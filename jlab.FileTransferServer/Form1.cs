using System;
using System.Drawing;
using System.Threading;
using System.Diagnostics;
using System.Windows.Forms;
using jlab.FileTransferServer.libs;

namespace jlab.FileTransferServer
{
    public partial class FTForm : Form, IPrintMessage
    {
        private const int THUMB_PRESENTATION_SIZE = 200;
        private const string APP_NAME = "jlab.FileTransferServer";
        private HttpServer Server { get; set; }
        public bool Running { get; set; }
        private int CountAllTrace { get; set; }
        private int CountDirTrace { get; set; }
        private int CountIndexTrace { get; set; }
        private int CountFileTrace { get; set; }
        private int CountThumbTrace { get; set; }
        private int CountThumbIndex { get; set; }
        private int CountThumbIndexSpeed { get; set; }
        private int CountThumbIndexLength { get; set; }
        private bool SettingFormShow { get; set; }
        private Config Config = new Config();
        private Settings SettingForm { get; set; }
        private Thread ServerTask { get; set; }
        private long[] AllDownloadSpeeds { get; set; } = new long[LengthSpeedArrays];
        private long[] DirDownloadSpeeds { get; set; } = new long[LengthSpeedArrays];
        private long[] FileDownloadSpeeds { get; set; } = new long[LengthSpeedArrays];
        private long[] ThumbDownloadSpeeds { get; set; } = new long[LengthSpeedArrays];
        private long[] ThumbIndexSpeeds { get; set; } = new long[LengthSpeedArrays];
        private byte CountSpeeds { get; set; }
        private const int LengthSpeedArrays = 5;
        private FileThumbnailerChangeListener fileThumbChangeListener;
        private bool tpIndexVisible = false;
        private Semaphore printMutex = new Semaphore(1, 1);
        public Index index { get; set; }

        private bool PlayingThumb { get; set; }

        public FTForm()
        {
            CountAllTrace = CountDirTrace = CountFileTrace = CountThumbTrace = CountIndexTrace = 0;
            InitializeComponent();
            Server = new HttpServer();
            CheckForIllegalCrossThreadCalls = false;
            Config = Config.Load();
            index = new Index(Config);
            index.OutputMesages = this;
            llDirectory.Text = Config.DirectoryPath;
            tcTraces.SelectedTab = tpDirTraces;
            tcTraces.SelectedTab = tpFileTraces;
            tcTraces.SelectedTab = tpThumbTraces;
            tcTraces.SelectedTab = tpAllTraces;
            tcTraces.TabPages.Remove(tpIndexTraces);
            if (Config.DirectoryPath != null)
            {
                try
                {
                    fileThumbChangeListener = new FileThumbnailerChangeListener(Config);
                    fileThumbChangeListener.OutputMesages = this;
                }
                catch { }
            }
            new Thread(putFirewallRule).Start();
        }

        private void putFirewallRule()
        {
            FirewallHelper.AddPort(Config.Port, APP_NAME, NetFwTypeLib.NET_FW_IP_PROTOCOL_.NET_FW_IP_PROTOCOL_TCP);
        }

        private void tsLoadDir_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.Description = Properties.Settings.Default.DirectoryToShare;
            fbd.ShowNewFolderButton = false;
            var dialogRes = fbd.ShowDialog();
            if (dialogRes == DialogResult.OK)
            {
                Config.DirectoryPath = fbd.SelectedPath;
                if (fileThumbChangeListener == null)
                {
                    fileThumbChangeListener = new FileThumbnailerChangeListener(Config);
                    fileThumbChangeListener.OutputMesages = this;
                }
                else
                    fileThumbChangeListener.SetConfig(Config);
                if (Running)
                {
                    tsStartOrStop_Click(null, null);
                    tsStartOrStop_Click(null, null);
                }
                createIndex(fbd.SelectedPath, true);
            }
            llDirectory.Text = Config.DirectoryPath;
        }

        private void SetStateRunningOrNot(string str1, string str2, Bitmap bitmap)
        {
            this.tsStartOrStop.ToolTipText = str1;
            this.tsmStartOrStopServer.Text = str2;
            tsStartOrStop.Image = this.tsmStartOrStopServer.Image = bitmap;
        }

        public void tsStartOrStop_Click(object sender, EventArgs e)
        {
            if (Running)
            {
                SetStateRunningOrNot(Properties.Settings.Default.StartServer + " (F5)", Properties.Settings.Default.StartServer, Properties.Resources.start);
                lbServerStatus.Text = Properties.Settings.Default.Stoped;
                ucForm_FormClosing(null, null);
            }
            else
            {
                SetStateRunningOrNot(Properties.Settings.Default.StopServer + " (F5)", Properties.Settings.Default.StopServer, Properties.Resources.stop);
                lbServerStatus.Text = Properties.Settings.Default.Running;
                try
                {
                    Server.Create("*", Config);
                    Server.OutputMesages = this;
                    ServerTask = new Thread(() =>
                    {
                        try
                        {
                            Server.Start();
                        }
                        catch (MException excp)
                        {
                            Print(string.Format("[{0}] - - {1}", DateTime.Now, excp.Message), Config.ERROR_TAG);
                            SetStateRunningOrNot(Properties.Settings.Default.StartServer + " (F5)", Properties.Settings.Default.StartServer, Properties.Resources.start);
                            lbServerStatus.Text = Properties.Settings.Default.Stoped;
                            return;
                        }
                    });
                    ServerTask.Start();
                }
                catch (MException excp)
                {
                    Print(string.Format("[{0}] - - {1}", DateTime.Now, excp.Message), Config.ERROR_TAG);
                    SetStateRunningOrNot(Properties.Settings.Default.StartServer + " (F5)", Properties.Settings.Default.StartServer, Properties.Resources.start);
                    lbServerStatus.Text = Properties.Settings.Default.Stoped;
                    return;
                }
            }
            Running = !Running;
        }

        private void llDirectory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                Process.Start(llDirectory.Text);
            }
            catch
            {
                Print(string.Format("[{0}] - - {1}{2} {3}", DateTime.Now, Properties.Settings.Default.TheDirectory, (llDirectory.Text != null ? " " + llDirectory.Text : ""), Properties.Settings.Default.NotExist), Config.ERROR_TAG);
            }
        }

        private void ucForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (index.Creating && sender != null && e != null)
                    index.lastRenamedThread.Abort();
                if (fileThumbChangeListener.lastRenamedThread != null && fileThumbChangeListener.lastRenamedThread.IsAlive)
                    fileThumbChangeListener.lastRenamedThread.Abort();
                if (ServerTask != null)
                    ServerTask.Abort();
                if (Running && Server != null)
                    Server.Stop();
                Config.Save();
            }
            catch { }
        }

        private void aToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!SettingFormShow)
            {
                SettingForm = new Settings(this, Config);
                SettingForm.FormClosing += SettingForm_FormClosing;
                SettingFormShow = true;
                SettingForm.Show();
            }
            else
            {
                SettingForm.Focus();
                SettingForm.WindowState = FormWindowState.Normal;
            }
        }

        private void SettingForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            SettingFormShow = false;
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void setEnableButton(bool enabled)
        {
            tsNewIndex.Enabled = tsmNewIndex.Enabled = tsmLoadDir.Enabled = tsLoadDir.Enabled = enabled;
        }

        private void crearIndiceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            createIndex(null, false);
        }

        private void createIndex(string dirPath, bool question)
        {
            var dialogRes = DialogResult.OK;
            if (dirPath == null)
            {
                FolderBrowserDialog fbd = new FolderBrowserDialog();
                fbd.Description = Properties.Settings.Default.DirectoryToIndex;
                fbd.ShowNewFolderButton = false;
                dialogRes = fbd.ShowDialog();
                if (dialogRes == DialogResult.OK)
                    dirPath = fbd.SelectedPath;
            }
            if (dialogRes == DialogResult.OK)
            {
                if (Config.IndexAlreadyCreated(dirPath))
                {
                    dialogRes = MessageBox.Show(this, Properties.Settings.Default.UpdateThumbIndex, Properties.Settings.Default.Question, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dialogRes == DialogResult.No)
                        return;
                    question = false;
                }
                if (question)
                {
                    dialogRes = MessageBox.Show(this, Properties.Settings.Default.CreateThumbIndex, Properties.Settings.Default.Question, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dialogRes == DialogResult.No)
                        return;
                }
                index.onStartingCreating = () => StartingCreatingThumbnails();
                index.onFinishCreating = () => FinishCreatingThumbnails();
                index.Create(dirPath, () =>
                {
                    CountThumbIndexSpeed++;
                    CountThumbIndexLength++;
                }, () => PlayingThumb);
            }
        }

        private void StartingCreatingThumbnails()
        {
            setEnableButton(false);
            tcTraces.TabPages.Add(tpIndexTraces);
            tcTraces.SelectedTab = tpIndexTraces;
            tpIndexVisible = true;
            Refresh();
            PlayingThumb = true;
            btStartPauseThumb.Text = Properties.Settings.Default.Stop;
            CountThumbIndexLength = 0;
            lbThumbIndexTotal.Text = Properties.Settings.Default.Total + ": " + CountThumbIndexLength;
        }

        private void FinishCreatingThumbnails()
        {
            setEnableButton(true);
            tpIndexVisible = false;
            PlayingThumb = false;
            rtbIndexTraces.ResetText();
            tcTraces.TabPages.Remove(tpIndexTraces);
            Refresh();
        }

        private long AverageSpeed(long[] speeds)
        {
            long result = 0;
            for (int i = 0; i < LengthSpeedArrays; i++)
                result += speeds[i];
            return result / LengthSpeedArrays;
        }

        private void tmSpeed_Tick(object sender, EventArgs e)
        {
            lbDownloadAllLength.Text = string.Format(Properties.Settings.Default.Total + ": {0}", Utils.GetSize(Server.DownloadDirLength + Server.DownloadFileLength + Server.DownloadThumbLength));
            lbDownloadDirLength.Text = string.Format(Properties.Settings.Default.Total + ": {0}", Utils.GetSize(Server.DownloadDirLength));
            lbDownloadFileLength.Text = string.Format(Properties.Settings.Default.Total + ": {0}", Utils.GetSize(Server.DownloadFileLength));
            lbDownloadThumbLength.Text = string.Format(Properties.Settings.Default.Total + ": {0}", Utils.GetSize(Server.DownloadThumbLength));

            AllDownloadSpeeds[CountSpeeds] = Server.DownloadDirSpeed + Server.DownloadFileSpeed + Server.DownloadThumbSpeed;
            DirDownloadSpeeds[CountSpeeds] = Server.DownloadDirSpeed;
            FileDownloadSpeeds[CountSpeeds] = Server.DownloadFileSpeed;
            ThumbDownloadSpeeds[CountSpeeds] = Server.DownloadThumbSpeed;
            ThumbIndexSpeeds[CountSpeeds] = CountThumbIndexSpeed;
            CountSpeeds++;

            lbDownloadAllSpeed.Text = string.Format("{0}/s", Utils.GetSize(AverageSpeed(AllDownloadSpeeds)));
            lbDownloadDirSpeed.Text = string.Format("{0}/s", Utils.GetSize(AverageSpeed(DirDownloadSpeeds)));
            lbDownloadFileSpeed.Text = string.Format("{0}/s", Utils.GetSize(AverageSpeed(FileDownloadSpeeds)));
            lbDownloadThumbSpeed.Text = string.Format("{0}/s", Utils.GetSize(AverageSpeed(ThumbDownloadSpeeds)));
            lbThumbIndexSpeed.Text = string.Format("{0} thumb/s", AverageSpeed(ThumbIndexSpeeds));
            lbThumbIndexTotal.Text = Properties.Settings.Default.Total + ": " + CountThumbIndexLength;

            CountSpeeds %= LengthSpeedArrays;
            CountThumbIndexSpeed = 0;
            Server.DownloadDirSpeed = 0;
            Server.DownloadFileSpeed = 0;
            Server.DownloadThumbSpeed = 0;
        }

        private void btStartPauseThumb_Click(object sender, EventArgs e)
        {
            PlayingThumb = !PlayingThumb;
            if (PlayingThumb)
                btStartPauseThumb.Text = Properties.Settings.Default.Stop;
            else
                btStartPauseThumb.Text = Properties.Settings.Default.Continue;
        }

        private void btCancelThumb_Click(object sender, EventArgs e)
        {
            bool isPlayingThumb = PlayingThumb;
            if (isPlayingThumb)
                btStartPauseThumb_Click(null, null);
            if (index.Creating)
            {
                var dialogResult = MessageBox.Show(this, Properties.Settings.Default.StopIndexCreation, Properties.Settings.Default.Question, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                    index.lastRenamedThread.Abort();
            }
            if (isPlayingThumb)
                btStartPauseThumb_Click(null, null);
        }

        private void llInterfaces_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (!gbInterfaces.Visible)
            {
                llInterfaces.LinkColor = Color.Green;
                gbInterfaces.Visible = true;
                RefreshDataGridView();
                gbInterfaces.Show();
            }
            else
            {
                llInterfaces.LinkColor = Color.Blue;
                gbInterfaces.Visible = false;
                gbInterfaces.Hide();
            }
        }

        private void RefreshDataGridView()
        {
            dgvIPv4.Rows.Clear();
            foreach (Tuple<string, string> item in Utils.GetAllIpAddress())
                dgvIPv4.Rows.Add(item.Item1, item.Item2);
        }

        private void tsRefresh_Click(object sender, EventArgs e)
        {
            RefreshDataGridView();
        }

        private void cargarImagenDePresentaciónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var opfd = new OpenFileDialog();
            opfd.CheckFileExists = true;
            var dialoResult = opfd.ShowDialog();
            if (dialoResult == DialogResult.OK)
            {
                string ext = System.IO.Path.GetExtension(opfd.FileName);
                string mimetype = Utils.GetMimeType(ext);
                var classmimetype = Utils.GetClassMimeType(mimetype);
                bool error = classmimetype != ClassMimeType.IMAGE;
                if (classmimetype == ClassMimeType.IMAGE)
                {
                    try
                    {
                        var thumb = new Thumbnail(opfd.FileName, Config, THUMB_PRESENTATION_SIZE);
                        thumb.Path = System.IO.Path.Combine(Config.DATA_DIR_PATH, "p" + thumb._ext);
                        thumb.Save();
                        if (Config.ThumbnailPresentationPath != null && Config.ThumbnailPresentationPath != thumb.Path)
                        {
                            try
                            {
                                System.IO.File.Delete(Config.ThumbnailPresentationPath);
                            }
                            catch { }
                        }
                        Config.ThumbnailPresentationPath = thumb.Path;
                    }
                    catch
                    {
                        error = true;
                    }
                }
                if (error)
                    MessageBox.Show(this, Properties.Settings.Default.NotLoadPresImage, Properties.Settings.Default.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void Print(string trace, char resId)
        {
            try
            {
                printMutex.WaitOne();
                Color color = Color.Violet;
                if (resId == Config.ERROR_TAG)
                    color = Color.Red;
                else if (resId == Config.INDEX_TAG)
                {
                    if (tpIndexVisible)
                    {
                        if (CountIndexTrace >= Config.MaxCountTrace)
                        {
                            rtbIndexTraces.ResetText();
                            CountIndexTrace = 0;
                        }
                        rtbIndexTraces.AppendText(trace + "\n");
                        CountIndexTrace++;
                    }
                    color = Color.Teal;
                }
                else if (resId == Config.DIR_TAG)
                {
                    if (CountDirTrace >= Config.MaxCountTrace)
                    {
                        rtbDirTraces.ResetText();
                        CountDirTrace = 0;
                    }
                    rtbDirTraces.AppendText(trace + "\n");
                    CountDirTrace++;
                    color = Color.Green;
                }
                else if (resId == Config.FILE_TAG)
                {
                    if (CountFileTrace >= Config.MaxCountTrace)
                    {
                        rtbFileTraces.ResetText();
                        CountFileTrace = 0;
                    }
                    rtbFileTraces.AppendText(trace + "\n");
                    CountFileTrace++;
                    color = Color.Blue;
                }
                else if (resId == Config.THUMB_TAG)
                {
                    if (CountThumbTrace >= Config.MaxCountTrace)
                    {
                        rtbThumbTraces.ResetText();
                        CountThumbTrace = 0;
                    }
                    rtbThumbTraces.AppendText(trace + "\n");
                    CountThumbTrace++;
                    color = Color.Orange;
                }
                if (CountAllTrace > Config.MaxCountTrace)
                {
                    rtbAllTraces.ResetText();
                    CountAllTrace = 0;
                }
                lock (rtbAllTraces)
                {
                    int pos = rtbAllTraces.TextLength;
                    rtbAllTraces.AppendText(trace + "\n");
                    rtbAllTraces.Select(pos, rtbAllTraces.TextLength - pos);
                    rtbAllTraces.SelectionColor = color;
                    CountAllTrace++;
                }
            }
            catch { }
            finally
            {
                printMutex.Release();
            }
        }
    }
}
