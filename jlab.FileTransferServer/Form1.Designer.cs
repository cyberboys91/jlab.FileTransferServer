namespace jlab.FileTransferServer
{
    partial class FTForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            try
            {
                if (disposing && (components != null))
                {
                    components.Dispose();
                }
                base.Dispose(disposing);
            }
            catch { }
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FTForm));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this.msOptions = new System.Windows.Forms.MenuStrip();
            this.menuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmStartOrStopServer = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmLoadDir = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmNewIndex = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmLoadPresImage = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmSetting = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmClose = new System.Windows.Forms.ToolStripMenuItem();
            this.tcTraces = new System.Windows.Forms.TabControl();
            this.tpAllTraces = new System.Windows.Forms.TabPage();
            this.lbDownloadAllSpeed = new System.Windows.Forms.Label();
            this.lbDownloadAllLength = new System.Windows.Forms.Label();
            this.rtbAllTraces = new System.Windows.Forms.RichTextBox();
            this.tpDirTraces = new System.Windows.Forms.TabPage();
            this.lbDownloadDirSpeed = new System.Windows.Forms.Label();
            this.lbDownloadDirLength = new System.Windows.Forms.Label();
            this.rtbDirTraces = new System.Windows.Forms.RichTextBox();
            this.tpFileTraces = new System.Windows.Forms.TabPage();
            this.lbDownloadFileSpeed = new System.Windows.Forms.Label();
            this.lbDownloadFileLength = new System.Windows.Forms.Label();
            this.rtbFileTraces = new System.Windows.Forms.RichTextBox();
            this.tpThumbTraces = new System.Windows.Forms.TabPage();
            this.lbDownloadThumbSpeed = new System.Windows.Forms.Label();
            this.lbDownloadThumbLength = new System.Windows.Forms.Label();
            this.rtbThumbTraces = new System.Windows.Forms.RichTextBox();
            this.tpIndexTraces = new System.Windows.Forms.TabPage();
            this.btCancelThumb = new System.Windows.Forms.Button();
            this.btStartPauseThumb = new System.Windows.Forms.Button();
            this.lbThumbIndexSpeed = new System.Windows.Forms.Label();
            this.lbThumbIndexTotal = new System.Windows.Forms.Label();
            this.rtbIndexTraces = new System.Windows.Forms.RichTextBox();
            this.msIcons = new System.Windows.Forms.MenuStrip();
            this.tsStartOrStop = new System.Windows.Forms.ToolStripMenuItem();
            this.tsLoadDir = new System.Windows.Forms.ToolStripMenuItem();
            this.tsNewIndex = new System.Windows.Forms.ToolStripMenuItem();
            this.adadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.llDirectory = new System.Windows.Forms.LinkLabel();
            this.tmSpeed = new System.Windows.Forms.Timer(this.components);
            this.llInterfaces = new System.Windows.Forms.LinkLabel();
            this.gbInterfaces = new System.Windows.Forms.GroupBox();
            this.dgvIPv4 = new System.Windows.Forms.DataGridView();
            this.dgvcInterfaceName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvcIPv4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.tsRefresh = new System.Windows.Forms.ToolStripMenuItem();
            this.lbServerStatus = new System.Windows.Forms.Label();
            this.msOptions.SuspendLayout();
            this.tcTraces.SuspendLayout();
            this.tpAllTraces.SuspendLayout();
            this.tpDirTraces.SuspendLayout();
            this.tpFileTraces.SuspendLayout();
            this.tpThumbTraces.SuspendLayout();
            this.tpIndexTraces.SuspendLayout();
            this.msIcons.SuspendLayout();
            this.gbInterfaces.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvIPv4)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // msOptions
            // 
            this.msOptions.BackColor = System.Drawing.Color.White;
            this.msOptions.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.msOptions.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuToolStripMenuItem});
            resources.ApplyResources(this.msOptions, "msOptions");
            this.msOptions.Name = "msOptions";
            // 
            // menuToolStripMenuItem
            // 
            this.menuToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmStartOrStopServer,
            this.tsmLoadDir,
            this.tsmNewIndex,
            this.tsmLoadPresImage,
            this.tsmSetting,
            this.tsmClose});
            this.menuToolStripMenuItem.Name = "menuToolStripMenuItem";
            resources.ApplyResources(this.menuToolStripMenuItem, "menuToolStripMenuItem");
            this.menuToolStripMenuItem.Text = global::jlab.FileTransferServer.Properties.Settings.Default.Options;
            // 
            // tsmStartOrStopServer
            // 
            this.tsmStartOrStopServer.Image = global::jlab.FileTransferServer.Properties.Resources.start;
            this.tsmStartOrStopServer.Name = "tsmStartOrStopServer";
            resources.ApplyResources(this.tsmStartOrStopServer, "tsmStartOrStopServer");
            this.tsmStartOrStopServer.Text = global::jlab.FileTransferServer.Properties.Settings.Default.StartServer;
            this.tsmStartOrStopServer.Click += new System.EventHandler(this.tsStartOrStop_Click);
            // 
            // tsmLoadDir
            // 
            this.tsmLoadDir.Image = global::jlab.FileTransferServer.Properties.Resources.folder;
            this.tsmLoadDir.Name = "tsmLoadDir";
            resources.ApplyResources(this.tsmLoadDir, "tsmLoadDir");
            this.tsmLoadDir.Text = global::jlab.FileTransferServer.Properties.Settings.Default.LoadDir;
            this.tsmLoadDir.Click += new System.EventHandler(this.tsLoadDir_Click);
            // 
            // tsmNewIndex
            // 
            resources.ApplyResources(this.tsmNewIndex, "tsmNewIndex");
            this.tsmNewIndex.Name = "tsmNewIndex";
            this.tsmNewIndex.Click += new System.EventHandler(this.crearIndiceToolStripMenuItem_Click);
            // 
            // tsmLoadPresImage
            // 
            this.tsmLoadPresImage.Image = global::jlab.FileTransferServer.Properties.Resources.image;
            this.tsmLoadPresImage.Name = "tsmLoadPresImage";
            resources.ApplyResources(this.tsmLoadPresImage, "tsmLoadPresImage");
            this.tsmLoadPresImage.Click += new System.EventHandler(this.cargarImagenDePresentaciónToolStripMenuItem_Click);
            // 
            // tsmSetting
            // 
            this.tsmSetting.Image = global::jlab.FileTransferServer.Properties.Resources.config;
            this.tsmSetting.Name = "tsmSetting";
            resources.ApplyResources(this.tsmSetting, "tsmSetting");
            this.tsmSetting.Text = global::jlab.FileTransferServer.Properties.Settings.Default.Setting;
            this.tsmSetting.Click += new System.EventHandler(this.aToolStripMenuItem_Click);
            // 
            // tsmClose
            // 
            this.tsmClose.Image = global::jlab.FileTransferServer.Properties.Resources.close;
            this.tsmClose.Name = "tsmClose";
            resources.ApplyResources(this.tsmClose, "tsmClose");
            this.tsmClose.Text = global::jlab.FileTransferServer.Properties.Settings.Default.Close;
            this.tsmClose.Click += new System.EventHandler(this.salirToolStripMenuItem_Click);
            // 
            // tcTraces
            // 
            resources.ApplyResources(this.tcTraces, "tcTraces");
            this.tcTraces.Controls.Add(this.tpAllTraces);
            this.tcTraces.Controls.Add(this.tpDirTraces);
            this.tcTraces.Controls.Add(this.tpFileTraces);
            this.tcTraces.Controls.Add(this.tpThumbTraces);
            this.tcTraces.Controls.Add(this.tpIndexTraces);
            this.tcTraces.Name = "tcTraces";
            this.tcTraces.SelectedIndex = 0;
            // 
            // tpAllTraces
            // 
            this.tpAllTraces.Controls.Add(this.lbDownloadAllSpeed);
            this.tpAllTraces.Controls.Add(this.lbDownloadAllLength);
            this.tpAllTraces.Controls.Add(this.rtbAllTraces);
            resources.ApplyResources(this.tpAllTraces, "tpAllTraces");
            this.tpAllTraces.Name = "tpAllTraces";
            this.tpAllTraces.Text = global::jlab.FileTransferServer.Properties.Settings.Default.AllTrace;
            this.tpAllTraces.UseVisualStyleBackColor = true;
            // 
            // lbDownloadAllSpeed
            // 
            resources.ApplyResources(this.lbDownloadAllSpeed, "lbDownloadAllSpeed");
            this.lbDownloadAllSpeed.ForeColor = System.Drawing.Color.DarkGreen;
            this.lbDownloadAllSpeed.Name = "lbDownloadAllSpeed";
            // 
            // lbDownloadAllLength
            // 
            resources.ApplyResources(this.lbDownloadAllLength, "lbDownloadAllLength");
            this.lbDownloadAllLength.Name = "lbDownloadAllLength";
            // 
            // rtbAllTraces
            // 
            resources.ApplyResources(this.rtbAllTraces, "rtbAllTraces");
            this.rtbAllTraces.BackColor = System.Drawing.Color.White;
            this.rtbAllTraces.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtbAllTraces.ForeColor = System.Drawing.Color.Green;
            this.rtbAllTraces.Name = "rtbAllTraces";
            this.rtbAllTraces.ReadOnly = true;
            // 
            // tpDirTraces
            // 
            this.tpDirTraces.Controls.Add(this.lbDownloadDirSpeed);
            this.tpDirTraces.Controls.Add(this.lbDownloadDirLength);
            this.tpDirTraces.Controls.Add(this.rtbDirTraces);
            resources.ApplyResources(this.tpDirTraces, "tpDirTraces");
            this.tpDirTraces.Name = "tpDirTraces";
            this.tpDirTraces.Text = global::jlab.FileTransferServer.Properties.Settings.Default.Directory;
            this.tpDirTraces.UseVisualStyleBackColor = true;
            // 
            // lbDownloadDirSpeed
            // 
            resources.ApplyResources(this.lbDownloadDirSpeed, "lbDownloadDirSpeed");
            this.lbDownloadDirSpeed.ForeColor = System.Drawing.Color.DarkGreen;
            this.lbDownloadDirSpeed.Name = "lbDownloadDirSpeed";
            // 
            // lbDownloadDirLength
            // 
            resources.ApplyResources(this.lbDownloadDirLength, "lbDownloadDirLength");
            this.lbDownloadDirLength.Name = "lbDownloadDirLength";
            // 
            // rtbDirTraces
            // 
            resources.ApplyResources(this.rtbDirTraces, "rtbDirTraces");
            this.rtbDirTraces.BackColor = System.Drawing.Color.White;
            this.rtbDirTraces.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtbDirTraces.ForeColor = System.Drawing.Color.Green;
            this.rtbDirTraces.Name = "rtbDirTraces";
            this.rtbDirTraces.ReadOnly = true;
            // 
            // tpFileTraces
            // 
            this.tpFileTraces.Controls.Add(this.lbDownloadFileSpeed);
            this.tpFileTraces.Controls.Add(this.lbDownloadFileLength);
            this.tpFileTraces.Controls.Add(this.rtbFileTraces);
            resources.ApplyResources(this.tpFileTraces, "tpFileTraces");
            this.tpFileTraces.Name = "tpFileTraces";
            this.tpFileTraces.Text = global::jlab.FileTransferServer.Properties.Settings.Default.Files;
            this.tpFileTraces.UseVisualStyleBackColor = true;
            // 
            // lbDownloadFileSpeed
            // 
            resources.ApplyResources(this.lbDownloadFileSpeed, "lbDownloadFileSpeed");
            this.lbDownloadFileSpeed.ForeColor = System.Drawing.Color.DarkGreen;
            this.lbDownloadFileSpeed.Name = "lbDownloadFileSpeed";
            // 
            // lbDownloadFileLength
            // 
            resources.ApplyResources(this.lbDownloadFileLength, "lbDownloadFileLength");
            this.lbDownloadFileLength.Name = "lbDownloadFileLength";
            // 
            // rtbFileTraces
            // 
            resources.ApplyResources(this.rtbFileTraces, "rtbFileTraces");
            this.rtbFileTraces.BackColor = System.Drawing.Color.White;
            this.rtbFileTraces.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtbFileTraces.ForeColor = System.Drawing.Color.Blue;
            this.rtbFileTraces.Name = "rtbFileTraces";
            this.rtbFileTraces.ReadOnly = true;
            // 
            // tpThumbTraces
            // 
            this.tpThumbTraces.Controls.Add(this.lbDownloadThumbSpeed);
            this.tpThumbTraces.Controls.Add(this.lbDownloadThumbLength);
            this.tpThumbTraces.Controls.Add(this.rtbThumbTraces);
            resources.ApplyResources(this.tpThumbTraces, "tpThumbTraces");
            this.tpThumbTraces.Name = "tpThumbTraces";
            this.tpThumbTraces.Text = global::jlab.FileTransferServer.Properties.Settings.Default.Thumbnails;
            this.tpThumbTraces.UseVisualStyleBackColor = true;
            // 
            // lbDownloadThumbSpeed
            // 
            resources.ApplyResources(this.lbDownloadThumbSpeed, "lbDownloadThumbSpeed");
            this.lbDownloadThumbSpeed.ForeColor = System.Drawing.Color.DarkGreen;
            this.lbDownloadThumbSpeed.Name = "lbDownloadThumbSpeed";
            // 
            // lbDownloadThumbLength
            // 
            resources.ApplyResources(this.lbDownloadThumbLength, "lbDownloadThumbLength");
            this.lbDownloadThumbLength.Name = "lbDownloadThumbLength";
            // 
            // rtbThumbTraces
            // 
            resources.ApplyResources(this.rtbThumbTraces, "rtbThumbTraces");
            this.rtbThumbTraces.BackColor = System.Drawing.Color.White;
            this.rtbThumbTraces.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtbThumbTraces.ForeColor = System.Drawing.Color.Orange;
            this.rtbThumbTraces.Name = "rtbThumbTraces";
            this.rtbThumbTraces.ReadOnly = true;
            // 
            // tpIndexTraces
            // 
            this.tpIndexTraces.Controls.Add(this.btCancelThumb);
            this.tpIndexTraces.Controls.Add(this.btStartPauseThumb);
            this.tpIndexTraces.Controls.Add(this.lbThumbIndexSpeed);
            this.tpIndexTraces.Controls.Add(this.lbThumbIndexTotal);
            this.tpIndexTraces.Controls.Add(this.rtbIndexTraces);
            resources.ApplyResources(this.tpIndexTraces, "tpIndexTraces");
            this.tpIndexTraces.Name = "tpIndexTraces";
            this.tpIndexTraces.Text = global::jlab.FileTransferServer.Properties.Settings.Default.Index;
            this.tpIndexTraces.UseVisualStyleBackColor = true;
            // 
            // btCancelThumb
            // 
            resources.ApplyResources(this.btCancelThumb, "btCancelThumb");
            this.btCancelThumb.Name = "btCancelThumb";
            this.btCancelThumb.Text = global::jlab.FileTransferServer.Properties.Settings.Default.Cancel;
            this.btCancelThumb.UseVisualStyleBackColor = true;
            this.btCancelThumb.Click += new System.EventHandler(this.btCancelThumb_Click);
            // 
            // btStartPauseThumb
            // 
            resources.ApplyResources(this.btStartPauseThumb, "btStartPauseThumb");
            this.btStartPauseThumb.Name = "btStartPauseThumb";
            this.btStartPauseThumb.Text = global::jlab.FileTransferServer.Properties.Settings.Default.StopServer;
            this.btStartPauseThumb.UseVisualStyleBackColor = true;
            this.btStartPauseThumb.Click += new System.EventHandler(this.btStartPauseThumb_Click);
            // 
            // lbThumbIndexSpeed
            // 
            resources.ApplyResources(this.lbThumbIndexSpeed, "lbThumbIndexSpeed");
            this.lbThumbIndexSpeed.ForeColor = System.Drawing.Color.DarkGreen;
            this.lbThumbIndexSpeed.Name = "lbThumbIndexSpeed";
            // 
            // lbThumbIndexTotal
            // 
            resources.ApplyResources(this.lbThumbIndexTotal, "lbThumbIndexTotal");
            this.lbThumbIndexTotal.Name = "lbThumbIndexTotal";
            // 
            // rtbIndexTraces
            // 
            resources.ApplyResources(this.rtbIndexTraces, "rtbIndexTraces");
            this.rtbIndexTraces.BackColor = System.Drawing.Color.White;
            this.rtbIndexTraces.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtbIndexTraces.ForeColor = System.Drawing.Color.Teal;
            this.rtbIndexTraces.Name = "rtbIndexTraces";
            this.rtbIndexTraces.ReadOnly = true;
            // 
            // msIcons
            // 
            this.msIcons.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.msIcons.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsStartOrStop,
            this.tsLoadDir,
            this.tsNewIndex,
            this.adadToolStripMenuItem});
            resources.ApplyResources(this.msIcons, "msIcons");
            this.msIcons.Name = "msIcons";
            this.msIcons.ShowItemToolTips = true;
            // 
            // tsStartOrStop
            // 
            this.tsStartOrStop.Image = global::jlab.FileTransferServer.Properties.Resources.start;
            this.tsStartOrStop.Name = "tsStartOrStop";
            resources.ApplyResources(this.tsStartOrStop, "tsStartOrStop");
            this.tsStartOrStop.Click += new System.EventHandler(this.tsStartOrStop_Click);
            // 
            // tsLoadDir
            // 
            this.tsLoadDir.Image = global::jlab.FileTransferServer.Properties.Resources.folder;
            this.tsLoadDir.Name = "tsLoadDir";
            resources.ApplyResources(this.tsLoadDir, "tsLoadDir");
            this.tsLoadDir.Click += new System.EventHandler(this.tsLoadDir_Click);
            // 
            // tsNewIndex
            // 
            this.tsNewIndex.Image = global::jlab.FileTransferServer.Properties.Resources.index;
            this.tsNewIndex.Name = "tsNewIndex";
            resources.ApplyResources(this.tsNewIndex, "tsNewIndex");
            this.tsNewIndex.Click += new System.EventHandler(this.crearIndiceToolStripMenuItem_Click);
            // 
            // adadToolStripMenuItem
            // 
            this.adadToolStripMenuItem.Image = global::jlab.FileTransferServer.Properties.Resources.config;
            this.adadToolStripMenuItem.Name = "adadToolStripMenuItem";
            resources.ApplyResources(this.adadToolStripMenuItem, "adadToolStripMenuItem");
            this.adadToolStripMenuItem.Click += new System.EventHandler(this.aToolStripMenuItem_Click);
            // 
            // llDirectory
            // 
            resources.ApplyResources(this.llDirectory, "llDirectory");
            this.llDirectory.Name = "llDirectory";
            this.llDirectory.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llDirectory_LinkClicked);
            // 
            // tmSpeed
            // 
            this.tmSpeed.Enabled = true;
            this.tmSpeed.Interval = 1000;
            this.tmSpeed.Tick += new System.EventHandler(this.tmSpeed_Tick);
            // 
            // llInterfaces
            // 
            resources.ApplyResources(this.llInterfaces, "llInterfaces");
            this.llInterfaces.BackColor = System.Drawing.Color.Transparent;
            this.llInterfaces.DisabledLinkColor = System.Drawing.Color.Transparent;
            this.llInterfaces.Name = "llInterfaces";
            this.llInterfaces.TabStop = true;
            this.llInterfaces.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llInterfaces_LinkClicked);
            // 
            // gbInterfaces
            // 
            resources.ApplyResources(this.gbInterfaces, "gbInterfaces");
            this.gbInterfaces.Controls.Add(this.dgvIPv4);
            this.gbInterfaces.Controls.Add(this.menuStrip1);
            this.gbInterfaces.Name = "gbInterfaces";
            this.gbInterfaces.TabStop = false;
            // 
            // dgvIPv4
            // 
            this.dgvIPv4.AllowUserToAddRows = false;
            this.dgvIPv4.AllowUserToDeleteRows = false;
            this.dgvIPv4.AllowUserToResizeRows = false;
            resources.ApplyResources(this.dgvIPv4, "dgvIPv4");
            this.dgvIPv4.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvIPv4.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvIPv4.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvIPv4.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvIPv4.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgvcInterfaceName,
            this.dgvcIPv4});
            this.dgvIPv4.GridColor = System.Drawing.SystemColors.Control;
            this.dgvIPv4.Name = "dgvIPv4";
            this.dgvIPv4.ReadOnly = true;
            this.dgvIPv4.RowHeadersVisible = false;
            this.dgvIPv4.RowTemplate.Height = 24;
            // 
            // dgvcInterfaceName
            // 
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.White;
            this.dgvcInterfaceName.DefaultCellStyle = dataGridViewCellStyle7;
            resources.ApplyResources(this.dgvcInterfaceName, "dgvcInterfaceName");
            this.dgvcInterfaceName.Name = "dgvcInterfaceName";
            this.dgvcInterfaceName.ReadOnly = true;
            this.dgvcInterfaceName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dgvcIPv4
            // 
            dataGridViewCellStyle8.BackColor = System.Drawing.Color.White;
            this.dgvcIPv4.DefaultCellStyle = dataGridViewCellStyle8;
            resources.ApplyResources(this.dgvcIPv4, "dgvcIPv4");
            this.dgvcIPv4.Name = "dgvcIPv4";
            this.dgvcIPv4.ReadOnly = true;
            this.dgvcIPv4.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvcIPv4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // menuStrip1
            // 
            resources.ApplyResources(this.menuStrip1, "menuStrip1");
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsRefresh});
            this.menuStrip1.Name = "menuStrip1";
            // 
            // tsRefresh
            // 
            this.tsRefresh.Image = global::jlab.FileTransferServer.Properties.Resources.refresh;
            this.tsRefresh.Name = "tsRefresh";
            resources.ApplyResources(this.tsRefresh, "tsRefresh");
            this.tsRefresh.Click += new System.EventHandler(this.tsRefresh_Click);
            // 
            // lbServerStatus
            // 
            resources.ApplyResources(this.lbServerStatus, "lbServerStatus");
            this.lbServerStatus.Name = "lbServerStatus";
            // 
            // FTForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.lbServerStatus);
            this.Controls.Add(this.llInterfaces);
            this.Controls.Add(this.gbInterfaces);
            this.Controls.Add(this.tcTraces);
            this.Controls.Add(this.msIcons);
            this.Controls.Add(this.llDirectory);
            this.Controls.Add(this.msOptions);
            this.ForeColor = System.Drawing.Color.Black;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FTForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ucForm_FormClosing);
            this.msOptions.ResumeLayout(false);
            this.msOptions.PerformLayout();
            this.tcTraces.ResumeLayout(false);
            this.tpAllTraces.ResumeLayout(false);
            this.tpDirTraces.ResumeLayout(false);
            this.tpFileTraces.ResumeLayout(false);
            this.tpThumbTraces.ResumeLayout(false);
            this.tpIndexTraces.ResumeLayout(false);
            this.msIcons.ResumeLayout(false);
            this.msIcons.PerformLayout();
            this.gbInterfaces.ResumeLayout(false);
            this.gbInterfaces.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvIPv4)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip msOptions;
        private System.Windows.Forms.ToolStripMenuItem menuToolStripMenuItem;
        private System.Windows.Forms.MenuStrip msIcons;
        private System.Windows.Forms.ToolStripMenuItem tsStartOrStop;
        private System.Windows.Forms.ToolStripMenuItem tsmSetting;
        private System.Windows.Forms.ToolStripMenuItem tsLoadDir;
        private System.Windows.Forms.LinkLabel llDirectory;
        private System.Windows.Forms.TabControl tcTraces;
        private System.Windows.Forms.TabPage tpAllTraces;
        private System.Windows.Forms.TabPage tpDirTraces;
        private System.Windows.Forms.TabPage tpFileTraces;
        private System.Windows.Forms.TabPage tpThumbTraces;
        private System.Windows.Forms.RichTextBox rtbAllTraces;
        private System.Windows.Forms.RichTextBox rtbDirTraces;
        private System.Windows.Forms.RichTextBox rtbFileTraces;
        private System.Windows.Forms.RichTextBox rtbThumbTraces;
        private System.Windows.Forms.ToolStripMenuItem tsmNewIndex;
        private System.Windows.Forms.ToolStripMenuItem tsmClose;
        private System.Windows.Forms.RichTextBox rtbIndexTraces;
        private System.Windows.Forms.Timer tmSpeed;
        private System.Windows.Forms.Label lbDownloadAllSpeed;
        private System.Windows.Forms.Label lbDownloadAllLength;
        private System.Windows.Forms.Label lbDownloadDirSpeed;
        private System.Windows.Forms.Label lbDownloadDirLength;
        private System.Windows.Forms.Label lbDownloadFileSpeed;
        private System.Windows.Forms.Label lbDownloadFileLength;
        private System.Windows.Forms.Label lbDownloadThumbSpeed;
        private System.Windows.Forms.Label lbDownloadThumbLength;
        private System.Windows.Forms.Label lbThumbIndexSpeed;
        private System.Windows.Forms.Label lbThumbIndexTotal;
        private System.Windows.Forms.ToolStripMenuItem adadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsNewIndex;
        private System.Windows.Forms.ToolStripMenuItem tsmStartOrStopServer;
        private System.Windows.Forms.ToolStripMenuItem tsmLoadDir;
        private System.Windows.Forms.TabPage tpIndexTraces;
        private System.Windows.Forms.Button btStartPauseThumb;
        private System.Windows.Forms.Button btCancelThumb;
        private System.Windows.Forms.LinkLabel llInterfaces;
        private System.Windows.Forms.GroupBox gbInterfaces;
        private System.Windows.Forms.DataGridView dgvIPv4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvcInterfaceName;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvcIPv4;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tsRefresh;
        private System.Windows.Forms.Label lbServerStatus;
        private System.Windows.Forms.ToolStripMenuItem tsmLoadPresImage;
    }
}

