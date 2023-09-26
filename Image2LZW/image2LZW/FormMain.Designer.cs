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
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbOpenImage = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbExportImage = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.tscbPalettes = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.pbOriginal = new System.Windows.Forms.PictureBox();
            this.pbElaborated = new System.Windows.Forms.PictureBox();
            this.tbOriginal = new System.Windows.Forms.TextBox();
            this.tbCompressed = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnCopy = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tsslStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslLoadImages = new System.Windows.Forms.ToolStripStatusLabel();
            this.sfdImage = new System.Windows.Forms.SaveFileDialog();
            this.bwCompress = new System.ComponentModel.BackgroundWorker();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tpImage = new System.Windows.Forms.TabPage();
            this.tabCode = new System.Windows.Forms.TabPage();
            this.panel2 = new System.Windows.Forms.Panel();
            this.rtbDemo = new System.Windows.Forms.RichTextBox();
            this.tsbCopyToClipboardCode = new System.Windows.Forms.ToolStrip();
            this.tsbCopyCodeExample = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.panel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbOriginal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbElaborated)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tpImage.SuspendLayout();
            this.tabCode.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tsbCopyToClipboardCode.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.toolStrip1);
            this.panel1.Controls.Add(this.tableLayoutPanel1);
            this.panel1.Controls.Add(this.statusStrip1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(2, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(749, 438);
            this.panel1.TabIndex = 0;
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbOpenImage,
            this.toolStripSeparator3,
            this.tsbExportImage,
            this.toolStripSeparator4,
            this.toolStripLabel2,
            this.tscbPalettes,
            this.toolStripSeparator5,
            this.toolStripLabel1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(749, 25);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbOpenImage
            // 
            this.tsbOpenImage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbOpenImage.Image = ((System.Drawing.Image)(resources.GetObject("tsbOpenImage.Image")));
            this.tsbOpenImage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbOpenImage.Name = "tsbOpenImage";
            this.tsbOpenImage.Size = new System.Drawing.Size(85, 22);
            this.tsbOpenImage.Text = "Open Image...";
            this.tsbOpenImage.Click += new System.EventHandler(this.TsbOpenImage_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbExportImage
            // 
            this.tsbExportImage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbExportImage.Enabled = false;
            this.tsbExportImage.Image = ((System.Drawing.Image)(resources.GetObject("tsbExportImage.Image")));
            this.tsbExportImage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbExportImage.Name = "tsbExportImage";
            this.tsbExportImage.Size = new System.Drawing.Size(80, 22);
            this.tsbExportImage.Text = "Export Image";
            this.tsbExportImage.Click += new System.EventHandler(this.TsbExportImage_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(43, 22);
            this.toolStripLabel2.Text = "Palette";
            // 
            // tscbPalettes
            // 
            this.tscbPalettes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tscbPalettes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.tscbPalettes.Name = "tscbPalettes";
            this.tscbPalettes.Size = new System.Drawing.Size(121, 25);
            this.tscbPalettes.SelectedIndexChanged += new System.EventHandler(this.TscbPalettes_SelectedIndexChanged);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(85, 22);
            this.toolStripLabel1.Text = "Alpha Channel";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 51F));
            this.tableLayoutPanel1.Controls.Add(this.pbOriginal, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.pbElaborated, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.tbOriginal, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.tbCompressed, 2, 3);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label4, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.btnCopy, 3, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(749, 416);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // pbOriginal
            // 
            this.pbOriginal.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.tableLayoutPanel1.SetColumnSpan(this.pbOriginal, 2);
            this.pbOriginal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbOriginal.Location = new System.Drawing.Point(3, 23);
            this.pbOriginal.Name = "pbOriginal";
            this.pbOriginal.Size = new System.Drawing.Size(368, 289);
            this.pbOriginal.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbOriginal.TabIndex = 0;
            this.pbOriginal.TabStop = false;
            // 
            // pbElaborated
            // 
            this.pbElaborated.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.tableLayoutPanel1.SetColumnSpan(this.pbElaborated, 2);
            this.pbElaborated.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbElaborated.Location = new System.Drawing.Point(377, 23);
            this.pbElaborated.Name = "pbElaborated";
            this.pbElaborated.Size = new System.Drawing.Size(369, 289);
            this.pbElaborated.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbElaborated.TabIndex = 1;
            this.pbElaborated.TabStop = false;
            // 
            // tbOriginal
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.tbOriginal, 2);
            this.tbOriginal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbOriginal.Location = new System.Drawing.Point(3, 345);
            this.tbOriginal.Multiline = true;
            this.tbOriginal.Name = "tbOriginal";
            this.tbOriginal.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbOriginal.Size = new System.Drawing.Size(368, 68);
            this.tbOriginal.TabIndex = 2;
            // 
            // tbCompressed
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.tbCompressed, 2);
            this.tbCompressed.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbCompressed.Location = new System.Drawing.Point(377, 345);
            this.tbCompressed.Multiline = true;
            this.tbCompressed.Name = "tbCompressed";
            this.tbCompressed.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbCompressed.Size = new System.Drawing.Size(369, 68);
            this.tbCompressed.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(318, 20);
            this.label1.TabIndex = 4;
            this.label1.Text = "Original Image";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(327, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 20);
            this.label2.TabIndex = 5;
            this.label2.Text = "Reconstructed Image";
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 322);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(102, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Uncompressed data";
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(377, 322);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Compressed data";
            // 
            // btnCopy
            // 
            this.btnCopy.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnCopy.Location = new System.Drawing.Point(701, 318);
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(45, 21);
            this.btnCopy.TabIndex = 8;
            this.btnCopy.Text = "Copy";
            this.btnCopy.UseVisualStyleBackColor = true;
            this.btnCopy.Click += new System.EventHandler(this.BtnCopy_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsslStatus,
            this.tsslLoadImages});
            this.statusStrip1.Location = new System.Drawing.Point(0, 416);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(749, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tsslStatus
            // 
            this.tsslStatus.Name = "tsslStatus";
            this.tsslStatus.Size = new System.Drawing.Size(39, 17);
            this.tsslStatus.Text = "fermo";
            // 
            // tsslLoadImages
            // 
            this.tsslLoadImages.Name = "tsslLoadImages";
            this.tsslLoadImages.Size = new System.Drawing.Size(0, 17);
            // 
            // sfdImage
            // 
            this.sfdImage.DefaultExt = "png";
            this.sfdImage.FileName = "export.png";
            this.sfdImage.Filter = "image files (*.png;*.bmp;*.gif;*.jpg)|*.png;*.bmp;*.gif;*.jpg";
            this.sfdImage.FileOk += new System.ComponentModel.CancelEventHandler(this.SfdImage_FileOk);
            // 
            // bwCompress
            // 
            this.bwCompress.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BwCompress_DoWork);
            this.bwCompress.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BwCompress_RunWorkerCompleted);
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tpImage);
            this.tabControl.Controls.Add(this.tabCode);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Margin = new System.Windows.Forms.Padding(2);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(761, 468);
            this.tabControl.TabIndex = 3;
            // 
            // tpImage
            // 
            this.tpImage.Controls.Add(this.panel1);
            this.tpImage.Location = new System.Drawing.Point(4, 22);
            this.tpImage.Margin = new System.Windows.Forms.Padding(2);
            this.tpImage.Name = "tpImage";
            this.tpImage.Padding = new System.Windows.Forms.Padding(2);
            this.tpImage.Size = new System.Drawing.Size(753, 442);
            this.tpImage.TabIndex = 0;
            this.tpImage.Text = "Image";
            this.tpImage.UseVisualStyleBackColor = true;
            // 
            // tabCode
            // 
            this.tabCode.Controls.Add(this.panel2);
            this.tabCode.Location = new System.Drawing.Point(4, 22);
            this.tabCode.Margin = new System.Windows.Forms.Padding(2);
            this.tabCode.Name = "tabCode";
            this.tabCode.Padding = new System.Windows.Forms.Padding(2);
            this.tabCode.Size = new System.Drawing.Size(753, 442);
            this.tabCode.TabIndex = 1;
            this.tabCode.Text = "Code";
            this.tabCode.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.rtbDemo);
            this.panel2.Controls.Add(this.tsbCopyToClipboardCode);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(2, 2);
            this.panel2.Margin = new System.Windows.Forms.Padding(2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(749, 438);
            this.panel2.TabIndex = 1;
            // 
            // rtbDemo
            // 
            this.rtbDemo.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.rtbDemo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbDemo.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbDemo.ForeColor = System.Drawing.SystemColors.Window;
            this.rtbDemo.Location = new System.Drawing.Point(0, 25);
            this.rtbDemo.Margin = new System.Windows.Forms.Padding(2);
            this.rtbDemo.Name = "rtbDemo";
            this.rtbDemo.ReadOnly = true;
            this.rtbDemo.Size = new System.Drawing.Size(749, 413);
            this.rtbDemo.TabIndex = 4;
            this.rtbDemo.Text = "";
            this.rtbDemo.WordWrap = false;
            this.rtbDemo.Layout += new System.Windows.Forms.LayoutEventHandler(this.RtbDemo_Layout);
            // 
            // tsbCopyToClipboardCode
            // 
            this.tsbCopyToClipboardCode.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.tsbCopyToClipboardCode.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbCopyCodeExample,
            this.toolStripSeparator2});
            this.tsbCopyToClipboardCode.Location = new System.Drawing.Point(0, 0);
            this.tsbCopyToClipboardCode.Name = "tsbCopyToClipboardCode";
            this.tsbCopyToClipboardCode.Size = new System.Drawing.Size(749, 25);
            this.tsbCopyToClipboardCode.TabIndex = 3;
            this.tsbCopyToClipboardCode.Text = "toolStrip2";
            // 
            // tsbCopyCodeExample
            // 
            this.tsbCopyCodeExample.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbCopyCodeExample.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbCopyCodeExample.Name = "tsbCopyCodeExample";
            this.tsbCopyCodeExample.Size = new System.Drawing.Size(39, 22);
            this.tsbCopyCodeExample.Text = "Copy";
            this.tsbCopyCodeExample.Click += new System.EventHandler(this.TsbCopyCodeExample_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(761, 468);
            this.Controls.Add(this.tabControl);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Image to LZW";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbOriginal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbElaborated)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.tabControl.ResumeLayout(false);
            this.tpImage.ResumeLayout(false);
            this.tabCode.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.tsbCopyToClipboardCode.ResumeLayout(false);
            this.tsbCopyToClipboardCode.PerformLayout();
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
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.ComponentModel.BackgroundWorker bwCompress;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tpImage;
        private System.Windows.Forms.TabPage tabCode;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ToolStrip tsbCopyToClipboardCode;
        private System.Windows.Forms.ToolStripButton tsbCopyCodeExample;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.RichTextBox rtbDemo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripComboBox tscbPalettes;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.Button btnCopy;
    }
}

