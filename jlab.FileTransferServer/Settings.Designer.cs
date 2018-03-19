namespace jlab.FileTransferServer
{
    partial class Settings
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
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lbPort = new System.Windows.Forms.Label();
            this.lbBufferSize = new System.Windows.Forms.Label();
            this.nudPort = new System.Windows.Forms.NumericUpDown();
            this.nudBufferSize = new System.Windows.Forms.NumericUpDown();
            this.btSaveSettings = new System.Windows.Forms.Button();
            this.nudCountConcurrentDir = new System.Windows.Forms.NumericUpDown();
            this.lbDir = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.nudCountConcurrentThumb = new System.Windows.Forms.NumericUpDown();
            this.lbThumb = new System.Windows.Forms.Label();
            this.nudCountConcurrentFile = new System.Windows.Forms.NumericUpDown();
            this.lbFile = new System.Windows.Forms.Label();
            this.nudCountTraces = new System.Windows.Forms.NumericUpDown();
            this.lbCountTraces = new System.Windows.Forms.Label();
            this.btCancel = new System.Windows.Forms.Button();
            this.rbKB = new System.Windows.Forms.RadioButton();
            this.rbMB = new System.Windows.Forms.RadioButton();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.nudPort)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudBufferSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCountConcurrentDir)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudCountConcurrentThumb)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCountConcurrentFile)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCountTraces)).BeginInit();
            this.SuspendLayout();
            // 
            // lbPort
            // 
            this.lbPort.AutoSize = true;
            this.lbPort.Enabled = false;
            this.lbPort.Location = new System.Drawing.Point(423, 30);
            this.lbPort.Name = "lbPort";
            this.lbPort.Size = new System.Drawing.Size(50, 17);
            this.lbPort.TabIndex = 0;
            this.lbPort.Text = "Puerto";
            // 
            // lbBufferSize
            // 
            this.lbBufferSize.AutoSize = true;
            this.lbBufferSize.Location = new System.Drawing.Point(41, 60);
            this.lbBufferSize.Name = "lbBufferSize";
            this.lbBufferSize.Size = new System.Drawing.Size(122, 17);
            this.lbBufferSize.TabIndex = 1;
            this.lbBufferSize.Text = global::jlab.FileTransferServer.Properties.Settings.Default.BufferSize;
            // 
            // nudPort
            // 
            this.nudPort.Enabled = false;
            this.nudPort.Location = new System.Drawing.Point(484, 28);
            this.nudPort.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.nudPort.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.nudPort.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudPort.Name = "nudPort";
            this.nudPort.Size = new System.Drawing.Size(76, 22);
            this.nudPort.TabIndex = 2;
            this.nudPort.Value = new decimal(new int[] {
            9101,
            0,
            0,
            0});
            this.nudPort.KeyDown += new System.Windows.Forms.KeyEventHandler(this.nudPort_KeyDown);
            // 
            // nudBufferSize
            // 
            this.nudBufferSize.Location = new System.Drawing.Point(174, 55);
            this.nudBufferSize.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.nudBufferSize.Maximum = new decimal(new int[] {
            1024,
            0,
            0,
            0});
            this.nudBufferSize.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudBufferSize.Name = "nudBufferSize";
            this.nudBufferSize.Size = new System.Drawing.Size(76, 22);
            this.nudBufferSize.TabIndex = 3;
            this.nudBufferSize.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudBufferSize.KeyDown += new System.Windows.Forms.KeyEventHandler(this.nudPort_KeyDown);
            // 
            // btSaveSettings
            // 
            this.btSaveSettings.Location = new System.Drawing.Point(88, 377);
            this.btSaveSettings.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btSaveSettings.Name = "btSaveSettings";
            this.btSaveSettings.Size = new System.Drawing.Size(87, 31);
            this.btSaveSettings.TabIndex = 5;
            this.btSaveSettings.Text = global::jlab.FileTransferServer.Properties.Settings.Default.Saving;
            this.btSaveSettings.UseVisualStyleBackColor = true;
            this.btSaveSettings.Click += new System.EventHandler(this.btSaveSettings_Click);
            // 
            // nudCountConcurrentDir
            // 
            this.nudCountConcurrentDir.Location = new System.Drawing.Point(161, 75);
            this.nudCountConcurrentDir.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.nudCountConcurrentDir.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nudCountConcurrentDir.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudCountConcurrentDir.Name = "nudCountConcurrentDir";
            this.nudCountConcurrentDir.Size = new System.Drawing.Size(76, 22);
            this.nudCountConcurrentDir.TabIndex = 7;
            this.nudCountConcurrentDir.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudCountConcurrentDir.KeyDown += new System.Windows.Forms.KeyEventHandler(this.nudPort_KeyDown);
            // 
            // lbDir
            // 
            this.lbDir.AutoSize = true;
            this.lbDir.Location = new System.Drawing.Point(75, 78);
            this.lbDir.Name = "lbDir";
            this.lbDir.Size = new System.Drawing.Size(76, 17);
            this.lbDir.TabIndex = 6;
            this.lbDir.Text = global::jlab.FileTransferServer.Properties.Settings.Default.Directory;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.nudCountConcurrentThumb);
            this.groupBox1.Controls.Add(this.lbThumb);
            this.groupBox1.Controls.Add(this.nudCountConcurrentFile);
            this.groupBox1.Controls.Add(this.lbFile);
            this.groupBox1.Controls.Add(this.nudCountConcurrentDir);
            this.groupBox1.Controls.Add(this.lbDir);
            this.groupBox1.Location = new System.Drawing.Point(13, 155);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Size = new System.Drawing.Size(332, 162);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = global::jlab.FileTransferServer.Properties.Settings.Default.Concurrence;
            // 
            // nudCountConcurrentThumb
            // 
            this.nudCountConcurrentThumb.Location = new System.Drawing.Point(161, 113);
            this.nudCountConcurrentThumb.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.nudCountConcurrentThumb.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nudCountConcurrentThumb.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudCountConcurrentThumb.Name = "nudCountConcurrentThumb";
            this.nudCountConcurrentThumb.Size = new System.Drawing.Size(76, 22);
            this.nudCountConcurrentThumb.TabIndex = 11;
            this.nudCountConcurrentThumb.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.nudCountConcurrentThumb.KeyDown += new System.Windows.Forms.KeyEventHandler(this.nudPort_KeyDown);
            // 
            // lbThumb
            // 
            this.lbThumb.AutoSize = true;
            this.lbThumb.Location = new System.Drawing.Point(77, 114);
            this.lbThumb.Name = "lbThumb";
            this.lbThumb.Size = new System.Drawing.Size(73, 17);
            this.lbThumb.TabIndex = 10;
            this.lbThumb.Text = global::jlab.FileTransferServer.Properties.Settings.Default.Thumbnails;
            // 
            // nudCountConcurrentFile
            // 
            this.nudCountConcurrentFile.Location = new System.Drawing.Point(161, 41);
            this.nudCountConcurrentFile.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.nudCountConcurrentFile.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudCountConcurrentFile.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudCountConcurrentFile.Name = "nudCountConcurrentFile";
            this.nudCountConcurrentFile.Size = new System.Drawing.Size(76, 22);
            this.nudCountConcurrentFile.TabIndex = 9;
            this.nudCountConcurrentFile.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.nudCountConcurrentFile.KeyDown += new System.Windows.Forms.KeyEventHandler(this.nudPort_KeyDown);
            // 
            // lbFile
            // 
            this.lbFile.AutoSize = true;
            this.lbFile.Location = new System.Drawing.Point(88, 43);
            this.lbFile.Name = "lbFile";
            this.lbFile.Size = new System.Drawing.Size(62, 17);
            this.lbFile.TabIndex = 8;
            this.lbFile.Text = global::jlab.FileTransferServer.Properties.Settings.Default.Files;
            // 
            // nudCountTraces
            // 
            this.nudCountTraces.Location = new System.Drawing.Point(174, 99);
            this.nudCountTraces.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.nudCountTraces.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.nudCountTraces.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.nudCountTraces.Name = "nudCountTraces";
            this.nudCountTraces.Size = new System.Drawing.Size(76, 22);
            this.nudCountTraces.TabIndex = 10;
            this.nudCountTraces.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudCountTraces.KeyDown += new System.Windows.Forms.KeyEventHandler(this.nudPort_KeyDown);
            // 
            // lbCountTraces
            // 
            this.lbCountTraces.AutoSize = true;
            this.lbCountTraces.Location = new System.Drawing.Point(23, 102);
            this.lbCountTraces.Name = "lbCountTraces";
            this.lbCountTraces.Size = new System.Drawing.Size(140, 17);
            this.lbCountTraces.TabIndex = 9;
            this.lbCountTraces.Text = global::jlab.FileTransferServer.Properties.Settings.Default.CountTraceList;
            // 
            // btCancel
            // 
            this.btCancel.Location = new System.Drawing.Point(185, 377);
            this.btCancel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(87, 31);
            this.btCancel.TabIndex = 11;
            this.btCancel.Text = global::jlab.FileTransferServer.Properties.Settings.Default.Cancel;
            this.btCancel.UseVisualStyleBackColor = true;
            this.btCancel.Click += new System.EventHandler(this.btCancel_Click);
            // 
            // rbKB
            // 
            this.rbKB.AutoSize = true;
            this.rbKB.Location = new System.Drawing.Point(257, 55);
            this.rbKB.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.rbKB.Name = "rbKB";
            this.rbKB.Size = new System.Drawing.Size(47, 21);
            this.rbKB.TabIndex = 13;
            this.rbKB.Text = "KB";
            this.rbKB.UseVisualStyleBackColor = true;
            this.rbKB.CheckedChanged += new System.EventHandler(this.rbKB_CheckedChanged);
            // 
            // rbMB
            // 
            this.rbMB.AutoSize = true;
            this.rbMB.Checked = true;
            this.rbMB.Location = new System.Drawing.Point(307, 55);
            this.rbMB.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.rbMB.Name = "rbMB";
            this.rbMB.Size = new System.Drawing.Size(49, 21);
            this.rbMB.TabIndex = 14;
            this.rbMB.TabStop = true;
            this.rbMB.Text = "MB";
            this.rbMB.UseVisualStyleBackColor = true;
            this.rbMB.CheckedChanged += new System.EventHandler(this.rbMB_CheckedChanged);
            // 
            // button1
            // 
            this.button1.Enabled = false;
            this.button1.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.button1.Location = new System.Drawing.Point(567, 27);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(72, 27);
            this.button1.TabIndex = 15;
            this.button1.Text = "Generar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(360, 420);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.rbMB);
            this.Controls.Add(this.rbKB);
            this.Controls.Add(this.btCancel);
            this.Controls.Add(this.nudCountTraces);
            this.Controls.Add(this.lbCountTraces);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btSaveSettings);
            this.Controls.Add(this.nudBufferSize);
            this.Controls.Add(this.nudPort);
            this.Controls.Add(this.lbBufferSize);
            this.Controls.Add(this.lbPort);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(378, 467);
            this.MinimumSize = new System.Drawing.Size(378, 467);
            this.Name = "Settings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = global::jlab.FileTransferServer.Properties.Settings.Default.Setting;
            ((System.ComponentModel.ISupportInitialize)(this.nudPort)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudBufferSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCountConcurrentDir)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudCountConcurrentThumb)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCountConcurrentFile)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCountTraces)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbPort;
        private System.Windows.Forms.Label lbBufferSize;
        private System.Windows.Forms.NumericUpDown nudPort;
        private System.Windows.Forms.NumericUpDown nudBufferSize;
        private System.Windows.Forms.Button btSaveSettings;
        private System.Windows.Forms.NumericUpDown nudCountConcurrentDir;
        private System.Windows.Forms.Label lbDir;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.NumericUpDown nudCountConcurrentThumb;
        private System.Windows.Forms.Label lbThumb;
        private System.Windows.Forms.NumericUpDown nudCountConcurrentFile;
        private System.Windows.Forms.Label lbFile;
        private System.Windows.Forms.NumericUpDown nudCountTraces;
        private System.Windows.Forms.Label lbCountTraces;
        private System.Windows.Forms.Button btCancel;
        private System.Windows.Forms.RadioButton rbKB;
        private System.Windows.Forms.RadioButton rbMB;
        private System.Windows.Forms.Button button1;
    }
}