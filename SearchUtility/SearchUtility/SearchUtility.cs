#region Namespaces
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.Text.RegularExpressions;
using System.Globalization; 
#endregion

#region TechieNotes Namespace
namespace TechieNotes
{
    public partial class SearchUtility : Form
    {
        private static Queue<FileInfo> _dataQueue;
        private static int _fileCounter;
        private static int _fileTotalCounter;
        private static ManualResetEvent _resetEvent;
        private static object _lockObject;
        private static string _fileType;
        private static string _directory;
        private static string _searchText;
        private static bool _recursive;
        private static Regex _searchPattern;
        private static bool _closing;
        public SearchUtility()
        {
            InitializeComponent();
            _lockObject = new object();
            _dataQueue = new Queue<FileInfo>();
            _resetEvent = new ManualResetEvent(false);
            ThreadPool.QueueUserWorkItem(ProcessQueue);
        }

        private void BtnSearchClick(object sender, EventArgs e)
        {
            rtFile.Clear();
            _fileTotalCounter = _fileCounter = 0;
            groupBox1.Enabled = false;
            _dataQueue.Clear();
            lstSearchFile.Items.Clear();
            _fileType = txtFileType.Text;
            _directory = txtFileName.Text;
            _searchText = txtSearch.Text;
            _recursive = chkRecursive.Checked;
            _searchPattern = new Regex(_searchText, RegexOptions.IgnoreCase | RegexOptions.Multiline);
            ThreadPool.QueueUserWorkItem(ProcessDirectory, new DirectoryInfo(_directory));
        }

        private static void ProcessDirectory(object directoryinfo)
        {
            try
            {
                var diSearchDirectory = directoryinfo as DirectoryInfo;
                if(diSearchDirectory == null)
                    return;
                var fiFiles = diSearchDirectory.GetFiles(_fileType, _recursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly);
                if (fiFiles.Count() > 0)
                {
                    _fileTotalCounter = fiFiles.Count();
                    foreach (FileInfo fiInfo in fiFiles)
                        ThreadPool.QueueUserWorkItem(o =>
                        {
                            var fileinfo = o as FileInfo;
                            if (ProcessFile(fileinfo))
                            {
                                lock (_lockObject)
                                {
                                    _dataQueue.Enqueue(fileinfo);
                                    _resetEvent.Set();
                                }
                            }
                        }, fiInfo);
                }
                else
                {
                    _fileTotalCounter = _fileCounter = 0;
                    _resetEvent.Set();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private static bool ProcessFile(FileInfo file)
        {
            _fileCounter++;
            if (_fileCounter == _fileTotalCounter)
                _resetEvent.Set();
            string sContent = File.ReadAllText(file.FullName);
            if (_searchPattern.Match(sContent, 0).Success)
                return true;
            return false;
        }
        private void ProcessQueue(object objState)
        {
            for (; ; )
            {
                try
                {
                    _resetEvent.WaitOne();
                    if (_closing)
                        break;
                    if (_dataQueue.Count == 0)
                        _resetEvent.Reset();
                    lock (_lockObject)
                    {
                        int iDataCount = _dataQueue.Count;
                        for (int iFileInfoCtr = 0; iFileInfoCtr < iDataCount; iFileInfoCtr++)
                        {
                            FileInfo fileInfo = _dataQueue.Dequeue();
                            Invoke((Action<string>)(
                                o =>
                                {
                                    lstSearchFile.SuspendLayout();
                                    lstSearchFile.Items.Add(o);
                                    lblFilesFound.Text = lstSearchFile.Items.Count.ToString(CultureInfo.CurrentCulture);
                                    lstSearchFile.ResumeLayout();
                                }), new object[] { fileInfo.FullName });
                        }
                        if (_fileTotalCounter == _fileCounter)
                            Invoke((Action)(() =>
                            {
                                groupBox1.Enabled = true;
                            }));
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this,ex.Message,@"Error",MessageBoxButtons.OK,MessageBoxIcon.Error,MessageBoxDefaultButton.Button1,MessageBoxOptions.RightAlign);
                }
            }
        }

        private void SelectSearch()
        {
            MatchCollection matches = _searchPattern.Matches(rtFile.Text);
            foreach(Match match in matches)
                SelectMatch(match);
        }

        private void SelectMatch(Match match)
        {
            rtFile.Select(match.Index, match.Length);
            rtFile.SelectionBackColor = Color.LightGreen;
        }

        private static void SearchUtilityFormClosing(object sender, FormClosingEventArgs e)
        {
            _closing = true;
            _resetEvent.Set();
        }

        private void LstSearchFileClick(object sender, EventArgs e)
        {
            if (lstSearchFile.SelectedIndex != -1)
            {
                rtFile.Text = File.ReadAllText(lstSearchFile.SelectedItem.ToString());
                SelectSearch();
            }
        }
    }
} 
#endregion
