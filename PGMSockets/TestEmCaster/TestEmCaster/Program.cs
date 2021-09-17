using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading;
using Common.Logging;
using Common.Logging.Simple;
using Emcaster.Sockets;
using Emcaster.Topics;

namespace EmCasterTest
{
    class Program
    {
        static void Main(string[] args)
        {
            PubPgmTest();
            //PubSubTest();
        }

        public static void PubPgmTest()
        {
            LogManager.Adapter = new ConsoleOutLoggerFactoryAdapter(new NameValueCollection());
            IList<ByteMessageParser> msgsReceived = new List<ByteMessageParser>();
            var msgParser = new MessageParserFactory();
            var reader = new PgmReader(msgParser);
            var receiveSocket = new PgmReceiver("224.0.0.23", 40001, reader);

            var topicSubscriber = new TopicSubscriber(".*", msgParser);
            topicSubscriber.Start();
            receiveSocket.Start();
            topicSubscriber.TopicMessageEvent +=
                parser => msgsReceived.Add(new ByteMessageParser(parser.Topic, parser.ParseBytes(), parser.EndPoint));


            var sendSocket = new PgmSource("224.0.0.23", 40001);
            sendSocket.Start();
            var asyncWriter = new BatchWriter(sendSocket, 1024 * 64);
            var topicPublisher = new TopicPublisher(asyncWriter);
            topicPublisher.Start();

            Thread.Sleep(1000);

            for (int i = 0; i < 10; i++)
                topicPublisher.PublishObject(i + "", i, 1000);

            Thread.Sleep(3000);

            sendSocket.Dispose();
            receiveSocket.Dispose();
            int ii = msgsReceived.Count;
            //Assert.AreEqual(10, msgsReceived.Count);
            //Assert.AreEqual(0, msgsReceived[0].ParseObject());
            //Assert.AreEqual(9, msgsReceived[9].ParseObject());
            //for (int i = 0; i < msgsReceived.Count; i++)
            //{
            //    Assert.AreEqual(i + "", msgsReceived[i].Topic);
            //}
        }

        public static void PubSubTest()
        {
            IList<object> msgsReceived = new List<object>();
            var factory = new MessageParserFactory();
            var parser = factory.Create();
            var receiveSocket = new UdpReceiver("224.0.0.23", 40001);
            receiveSocket.ReceiveEvent += parser.OnBytes;

            var topicSubscriber = new TopicSubscriber("MSFT", factory);
            topicSubscriber.Start();
            receiveSocket.Start();
            topicSubscriber.TopicMessageEvent += msgParser => msgsReceived.Add(msgParser.ParseObject());


            var sendSocket = new UdpSource("224.0.0.23", 40001);
            sendSocket.Start();
            var asyncWriter = new BatchWriter(sendSocket, 1500);
            var topicPublisher = new TopicPublisher(asyncWriter);
            topicPublisher.Start();

            Thread.Sleep(1000);

            for (int i = 0; i < 10; i++)
                topicPublisher.PublishObject("MSFT", i, 1000);

            Thread.Sleep(3000);

            sendSocket.Dispose();
            receiveSocket.Dispose();

            //Assert.AreEqual(10, msgsReceived.Count);
            //Assert.AreEqual(0, msgsReceived[0]);
            //Assert.AreEqual(9, msgsReceived[9]);
        }

        public static void TopicPublisher()
        {
            var sendSocket = new PgmSource("224.0.0.23", 7272);
            sendSocket.Start();

            var asyncWriter = new BatchWriter(sendSocket, 1024 * 128);

            var publisher = new TopicPublisher(asyncWriter);
            publisher.Start();

            const int sendTimeout = 1000;
            publisher.PublishObject("Stock-Quotes-AAPL", 123.3, sendTimeout);
        }

        public static void TopicSubscriber()
        {
            var msgParser = new MessageParserFactory();
            var reader = new PgmReader(msgParser);
            var receiveSocket = new PgmReceiver("224.0.0.23", 7272, reader);

            var topicSubscriber = new TopicSubscriber("Stock-Quotes-AAPL", msgParser);
            topicSubscriber.TopicMessageEvent += TopicSubscriberTopicMessageEvent;
            topicSubscriber.Start();
            receiveSocket.Start();
        }

        static void TopicSubscriberTopicMessageEvent(IMessageParser parser)
        {
            
        }
    }
}
