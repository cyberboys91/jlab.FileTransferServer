using System;
using System.Windows.Forms;
using jlab.FileTransferServer.libs;

namespace jlab.FileTransferServer
{
    public partial class Settings : Form
    {
        private FTForm parentForm { get; set; }
        private Config Config { get; set; }
        public Settings(FTForm parentForm, Config config)
        {
            InitializeComponent();
            Config = config;
            this.parentForm = parentForm;
            if (Config.BUFFER_SIZE / 1024 >= 1024)
            {
                nudBufferSize.Value = (int)(Config.BUFFER_SIZE / 1048576);
                rbMB.Checked = true;
                nudBufferSize.Maximum = 20;
            }
            else
            {
                nudBufferSize.Value = (int)(Config.BUFFER_SIZE / 1024);
                rbKB.Checked = true;
                nudBufferSize.Maximum = 1024;
            }

            nudPort.Value = Config.Port;
            nudCountTraces.Value = Config.MaxCountTrace;
            nudCountConcurrentDir.Value = Config.MaxDirectoryDownConcurrent;
            nudCountConcurrentFile.Value = Config.MaxFileDownConcurrent;
            nudCountConcurrentThumb.Value = Config.MaxThumbnailDownConcurrent;
        }
        private int oldPort = 0;
        private void btSaveSettings_Click(object sender, EventArgs e)
        {
            oldPort = Config.Port;
            Config.Port = (int)nudPort.Value;

            Config.MaxCountTrace = (int)nudCountTraces.Value;
            Config.MaxDirectoryDownConcurrent = (int)nudCountConcurrentDir.Value;
            Config.MaxFileDownConcurrent = (int)nudCountConcurrentFile.Value;
            Config.MaxThumbnailDownConcurrent = (int)nudCountConcurrentThumb.Value;
            Config.BUFFER_SIZE = (int)nudBufferSize.Value * (rbKB.Checked ? 1024 : 1024 * 1024);

            if (parentForm.Running && oldPort != Config.Port)
            {
                var dialogRes = MessageBox.Show(this, Properties.Settings.Default.PortChange, Properties.Settings.Default.PortChange, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogRes == DialogResult.Yes)
                {
                    parentForm.tsStartOrStop_Click(null, null);
                    parentForm.tsStartOrStop_Click(null, null);
                }
                else
                {
                    nudPort.Value = oldPort;
                    Config.Port = oldPort;
                    return;
                }
            }
            Close();
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void nudPort_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btSaveSettings_Click(null, null);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            nudPort.Value = HttpServer.GetAnyPort();
        }

        private void rbMB_CheckedChanged(object sender, EventArgs e)
        {
            if (rbMB.Checked)
                nudBufferSize.Maximum = 20;
        }

        private void rbKB_CheckedChanged(object sender, EventArgs e)
        {
            if (rbKB.Checked)
                nudBufferSize.Maximum = 1024;
        }
    }
}