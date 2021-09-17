#define USE_FILE_BASED_CERTIFICATE

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;

namespace Sample2
{
    internal class Program
    {
#if (USE_FILE_BASED_CERTIFICATE)
        private static X509Certificate certFile = null;
#else
        static X509Certificate2 certFile = null;
#endif

        //
        // Server code

        public static void StartAsServer()
        {
#if (USE_FILE_BASED_CERTIFICATE)
            string cfile = "localhost" + ".cer";
            string certFilePath = @"C:\Projects\Certificates\" + cfile;
            if (!File.Exists(certFilePath))
                certFilePath = cfile;
            certFile = new X509Certificate(certFilePath);
#else
            X509Store store = new X509Store(StoreName.My,
              StoreLocation.LocalMachine);
            store.Open(OpenFlags.ReadOnly);
            X509Certificate2Collection certs =
              store.Certificates.Find(X509FindType.FindBySubjectName,
              Environment.MachineName, false);
            certFile = certs[0];
            store.Close();
#endif
            Console.WriteLine("Certificate subject: {0}", certFile.Subject);
            TcpListener listener = new TcpListener(IPAddress.Any, 8088);
            listener.Start();
            System.Console.WriteLine(
                "Waiting for client on port 8088... (use Ctrl+C to stop)");
            TcpClient tcpClient = listener.AcceptTcpClient();
            ProcessClient(tcpClient);
        }

        public static void ProcessClient(TcpClient client)
        {
            SslStream ssls = new SslStream(client.GetStream(), false);
            try
            {
                ssls.AuthenticateAsServer(certFile, false,
                                          SslProtocols.Tls, true);
                ssls.ReadTimeout = 500000;
                ssls.WriteTimeout = 500000;
                StartReaderSenderThreads(ssls);
            }
            catch (AuthenticationException ex)
            {
                System.Console.WriteLine(ex.Message);
                System.Console.WriteLine(ex.InnerException.Message);
            }
            finally
            {
                ssls.Close();
                client.Close();
            }
        }

        //
        // Client code

        // The following method is invoked by the 
        //  RemoteCertificateValidationDelegate.
        public static bool ValidateServerCertificate(object sender,
                                                     X509Certificate certificate,
                                                     X509Chain chain,
                                                     SslPolicyErrors sslPolicyErrors)
        {
            if (sslPolicyErrors == SslPolicyErrors.None)
                return true;

            Console.WriteLine("Certificate error: {0}", sslPolicyErrors);

            // Do not allow this client to communicate with unauthenticated servers.
            return false;
        }

        public static void StartAsClient(String server, string serverName, int port)
        {
            TcpClient client = new TcpClient(server, port);
            Console.WriteLine("Client connected..");
            SslStream ssls = new SslStream(client.GetStream(), false,
                                           new RemoteCertificateValidationCallback(ValidateServerCertificate),
                                           null);
            try
            {
                ssls.AuthenticateAsClient(serverName);
                StartReaderSenderThreads(ssls);
            }
            catch (AuthenticationException e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.InnerException.Message);
            }
            finally
            {
                //ssls.Close();
                client.Close();
            }
        }

        //
        // Common methods

        private static Thread readerThread = null;
        private static Thread senderThread = null;

        public static void ReaderThread(Object str)
        {
            SslStream ssls = str as SslStream;
            while (true)
            {
                string message = ReadMessage(ssls);
                System.Console.WriteLine("Remote: " + message);
                if (message.ToLower().Equals("\\quit"))
                {
                    ssls.Write(Encoding.UTF8.GetBytes("\\quit<EOF>"));
                    ssls.Flush();
                    break;
                }
            }
        }

        public static void SenderThread(Object str)
        {
            SslStream ssls = str as SslStream;
            while (true)
            {
                string message = Console.ReadLine();
                if (readerThread.IsAlive)
                {
                    ssls.Write(Encoding.UTF8.GetBytes(message + "<EOF>"));
                    ssls.Flush();
                }
                if (message.ToLower().Equals("\\quit"))
                    break;
            }
        }

        public static void StartReaderSenderThreads(SslStream ssls)
        {
            ParameterizedThreadStart readerStarter =
                new ParameterizedThreadStart(ReaderThread);
            ParameterizedThreadStart senderStarter =
                new ParameterizedThreadStart(SenderThread);
            readerThread = new Thread(readerStarter);
            senderThread = new Thread(senderStarter);
            readerThread.Start(ssls);
            senderThread.Start(ssls);
            readerThread.Join();
            senderThread.Join();
        }

        public static string ReadMessage(SslStream stream)
        {
            byte[] buffer = new byte[2048];
            StringBuilder sb = new StringBuilder();
            int bytes = -1;
            do
            {
                bytes = stream.Read(buffer, 0, buffer.Length);
                Decoder decoder = Encoding.UTF8.GetDecoder();
                char[] chars = new char[bytes];
                decoder.GetChars(buffer, 0, bytes, chars, 0);
                sb.Append(chars);
                if (sb.ToString().IndexOf("<EOF>") != -1)
                {
                    sb.Replace("<EOF>", "");
                    break;
                }
            } while (bytes > 0);

            return sb.ToString();
        }

        private static void Main(string[] args)
        {
            if (args.Length != 4 && args.Length != 1 ||
                args[0].ToLower().Equals("-h"))
            {
                Console.WriteLine(
                    "Required format:\n" +
                    "  [-h|-c|-s] [ServerName] [ServerMachine] [ServerPort]\n" +
                    "  Use -h to display this help screen\n" +
                    "  Use -s with no arguments to start as a server\n" +
                    "  Use -c with ServerName, ServerMachine and ServerPort arguments\n" +
                    "   to start as a client\n\n" +
                    "  For example: \n\n" +
                    "  C:\\SSLChat.exe -s\n" +
                    "    will start a server\n\n" +
                    "  C:\\SSLChat.exe -c localhost MachineName 8088\n" +
                    "    will start a client, and connect to server\n" +
                    "    located on localhost whose name is MachineName and\n" +
                    "    which is listening on port 8088\n\n" +
                    "  Use \\quit on command line to exit session\n");
                Environment.Exit(-1);
            }
            if (args[0].ToLower().Equals("-c"))
                StartAsClient(args[1], args[2], Convert.ToInt32(args[3]));
            if (args[0].ToLower().Equals("-s"))
                StartAsServer();
        }
    }
}
