namespace LZWConverter
{
    partial class FormMain
    {
        /// <summary>
        /// Variabile di progettazione necessaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Pulire le risorse in uso.
        /// </summary>
        /// <param name="disposing">ha valore true se le risorse gestite devono essere eliminate, false in caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Codice generato da Progettazione Windows Form

        /// <summary>
        /// Metodo necessario per il supporto della finestra di progettazione. Non modificare
        /// il contenuto del metodo con l'editor di codice.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.panel1 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.pbOriginal = new System.Windows.Forms.PictureBox();
            this.pbElaborated = new System.Windows.Forms.PictureBox();
            this.tbOriginal = new System.Windows.Forms.TextBox();
            this.tbCompressed = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tsslStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslLoadImages = new System.Windows.Forms.ToolStripStatusLabel();
            this.sfdImage = new System.Windows.Forms.SaveFileDialog();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbOpenImage = new System.Windows.Forms.ToolStripButton();
            this.tsbExportImage = new System.Windows.Forms.ToolStripButton();
            this.tsbCopyToClipboard = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.bwCompress = new System.ComponentModel.BackgroundWorker();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tpImage = new System.Windows.Forms.TabPage();
            this.tabCode = new System.Windows.Forms.TabPage();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbOriginal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbElaborated)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tpImage.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.toolStrip1);
            this.panel1.Controls.Add(this.tableLayoutPanel1);
            this.panel1.Controls.Add(this.statusStrip1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1001, 541);
            this.panel1.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.pbOriginal, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.pbElaborated, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.tbOriginal, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.tbCompressed, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label4, 1, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1001, 516);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // pbOriginal
            // 
            this.pbOriginal.BackColor = System.Drawing.SystemColors.ControlDark;
            this.pbOriginal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbOriginal.Location = new System.Drawing.Point(4, 29);
            this.pbOriginal.Margin = new System.Windows.Forms.Padding(4);
            this.pbOriginal.Name = "pbOriginal";
            this.pbOriginal.Size = new System.Drawing.Size(492, 364);
            this.pbOriginal.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbOriginal.TabIndex = 0;
            this.pbOriginal.TabStop = false;
            // 
            // pbElaborated
            // 
            this.pbElaborated.BackColor = System.Drawing.SystemColors.ControlDark;
            this.pbElaborated.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbElaborated.Location = new System.Drawing.Point(504, 29);
            this.pbElaborated.Margin = new System.Windows.Forms.Padding(4);
            this.pbElaborated.Name = "pbElaborated";
            this.pbElaborated.Size = new System.Drawing.Size(493, 364);
            this.pbElaborated.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbElaborated.TabIndex = 1;
            this.pbElaborated.TabStop = false;
            // 
            // tbOriginal
            // 
            this.tbOriginal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbOriginal.Location = new System.Drawing.Point(4, 426);
            this.tbOriginal.Margin = new System.Windows.Forms.Padding(4);
            this.tbOriginal.Multiline = true;
            this.tbOriginal.Name = "tbOriginal";
            this.tbOriginal.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbOriginal.Size = new System.Drawing.Size(492, 86);
            this.tbOriginal.TabIndex = 2;
            // 
            // tbCompressed
            // 
            this.tbCompressed.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbCompressed.Location = new System.Drawing.Point(504, 426);
            this.tbCompressed.Margin = new System.Windows.Forms.Padding(4);
            this.tbCompressed.Multiline = true;
            this.tbCompressed.Name = "tbCompressed";
            this.tbCompressed.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbCompressed.Size = new System.Drawing.Size(493, 86);
            this.tbCompressed.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(4, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(492, 25);
            this.label1.TabIndex = 4;
            this.label1.Text = "Original Image";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(504, 0);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(493, 25);
            this.label2.TabIndex = 5;
            this.label2.Text = "Reconstructed Image";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Location = new System.Drawing.Point(4, 397);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(492, 25);
            this.label3.TabIndex = 6;
            this.label3.Text = "Uncompressed data";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(504, 397);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(119, 17);
            this.label4.TabIndex = 7;
            this.label4.Text = "Compressed data";
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsslStatus,
            this.tsslLoadImages});
            this.statusStrip1.Location = new System.Drawing.Point(0, 516);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 19, 0);
            this.statusStrip1.Size = new System.Drawing.Size(1001, 25);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tsslStatus
            // 
            this.tsslStatus.Name = "tsslStatus";
            this.tsslStatus.Size = new System.Drawing.Size(21, 20);
            this.tsslStatus.Text = "\"\"";
            // 
            // tsslLoadImages
            // 
            this.tsslLoadImages.Name = "tsslLoadImages";
            this.tsslLoadImages.Size = new System.Drawing.Size(0, 20);
            // 
            // sfdImage
            // 
            this.sfdImage.DefaultExt = "png";
            this.sfdImage.FileName = "export.png";
            this.sfdImage.Filter = "image files (*.png;*.bmp;*.gif;*.jpg)|*.png;*.bmp;*.gif;*.jpg";
            this.sfdImage.FileOk += new System.ComponentModel.CancelEventHandler(this.sfdImage_FileOk);
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbOpenImage,
            this.tsbExportImage,
            this.tsbCopyToClipboard,
            this.toolStripSeparator1,
            this.toolStripLabel1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1001, 27);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbOpenImage
            // 
            this.tsbOpenImage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbOpenImage.Image = ((System.Drawing.Image)(resources.GetObject("tsbOpenImage.Image")));
            this.tsbOpenImage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbOpenImage.Name = "tsbOpenImage";
            this.tsbOpenImage.Size = new System.Drawing.Size(104, 24);
            this.tsbOpenImage.Text = "Open Image...";
            this.tsbOpenImage.Click += new System.EventHandler(this.tsbOpenImage_Click);
            // 
            // tsbExportImage
            // 
            this.tsbExportImage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbExportImage.Enabled = false;
            this.tsbExportImage.Image = ((System.Drawing.Image)(resources.GetObject("tsbExportImage.Image")));
            this.tsbExportImage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbExportImage.Name = "tsbExportImage";
            this.tsbExportImage.Size = new System.Drawing.Size(102, 24);
            this.tsbExportImage.Text = "Export Image";
            this.tsbExportImage.Click += new System.EventHandler(this.tsbExportImage_Click);
            // 
            // tsbCopyToClipboard
            // 
            this.tsbCopyToClipboard.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbCopyToClipboard.Image = ((System.Drawing.Image)(resources.GetObject("tsbCopyToClipboard.Image")));
            this.tsbCopyToClipboard.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbCopyToClipboard.Name = "tsbCopyToClipboard";
            this.tsbCopyToClipboard.Size = new System.Drawing.Size(137, 24);
            this.tsbCopyToClipboard.Text = "Copy To Clipboard";
            this.tsbCopyToClipboard.Click += new System.EventHandler(this.tsbCopyToClipboard_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 27);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(106, 24);
            this.toolStripLabel1.Text = "Alpha channel:";
            // 
            // bwCompress
            // 
            this.bwCompress.WorkerReportsProgress = true;
            this.bwCompress.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwCompress_DoWork);
            this.bwCompress.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bwCompress_ProgressChanged);
            this.bwCompress.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bwCompress_RunWorkerCompleted);
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tpImage);
            this.tabControl.Controls.Add(this.tabCode);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(1015, 576);
            this.tabControl.TabIndex = 3;
            // 
            // tpImage
            // 
            this.tpImage.Controls.Add(this.panel1);
            this.tpImage.Location = new System.Drawing.Point(4, 25);
            this.tpImage.Name = "tpImage";
            this.tpImage.Padding = new System.Windows.Forms.Padding(3);
            this.tpImage.Size = new System.Drawing.Size(1007, 547);
            this.tpImage.TabIndex = 0;
            this.tpImage.Text = "Image";
            this.tpImage.UseVisualStyleBackColor = true;
            // 
            // tabCode
            // 
            this.tabCode.Location = new System.Drawing.Point(4, 25);
            this.tabCode.Name = "tabCode";
            this.tabCode.Padding = new System.Windows.Forms.Padding(3);
            this.tabCode.Size = new System.Drawing.Size(1007, 547);
            this.tabCode.TabIndex = 1;
            this.tabCode.Text = "Code";
            this.tabCode.UseVisualStyleBackColor = true;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1015, 576);
            this.Controls.Add(this.tabControl);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Image to LZW";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbOriginal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbElaborated)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.tabControl.ResumeLayout(false);
            this.tpImage.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel tsslStatus;
        private System.Windows.Forms.PictureBox pbOriginal;
        private System.Windows.Forms.PictureBox pbElaborated;
        private System.Windows.Forms.TextBox tbOriginal;
        private System.Windows.Forms.TextBox tbCompressed;
        private System.Windows.Forms.ToolStripStatusLabel tsslLoadImages;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.SaveFileDialog sfdImage;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbOpenImage;
        private System.Windows.Forms.ToolStripButton tsbExportImage;
        private System.Windows.Forms.ToolStripButton tsbCopyToClipboard;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.ComponentModel.BackgroundWorker bwCompress;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tpImage;
        private System.Windows.Forms.TabPage tabCode;
    }
}

