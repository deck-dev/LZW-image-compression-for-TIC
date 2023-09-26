using image2LZW.Scanner;
using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace LZWConverter
{
    public partial class FormMain : Form
    {
        private const int MAX_ORIGINAL_TEXT_LENGTH = 999;

        private Image _img;
        private readonly ImageToLZW _img2LZW;
        private readonly MyComboBox _cbAlpha;
        private string _srcCode;

        public FormMain()
        {
            InitializeComponent();

            // Create the lzw manager
            _img2LZW = new ImageToLZW();

            // Used to notify current state of the compression/decompression process
            _img2LZW.LogEvent += new ImageToLZW.LogEventHandler(Img2LZW_LogEvent);

            // Add the custom combo box
            MyToolStripComboBox tscbAlpha = new MyToolStripComboBox();
            _cbAlpha = tscbAlpha.ComboBox;
            _cbAlpha.DropDownStyle = ComboBoxStyle.DropDownList;
            PopulateAlphaComboBox();
            _cbAlpha.ItemHeight = 18;
            _cbAlpha.SelectedIndexChanged += CbAlpha_SelectedIndexChanged;
            toolStrip1.Items.Add(tscbAlpha);

            // Read the configuration and extract the palettes
            try
            {
                TicConfigParser parser = new TicConfigParser(
                    Path.Combine(Directory.GetCurrentDirectory(), "config.lua"));
                tscbPalettes.Items.AddRange(parser.Palettes.ToArray());
                tscbPalettes.SelectedIndex = 0;
            }
            catch
            {
                // MessageBox.Show("Can't find the config.lua");
                UpdateStat("Can't find the config.lua");
            }
        }

        private void PopulateAlphaComboBox()
        {
            _cbAlpha.Items.Clear();
            _cbAlpha.Items.Add(new MyComboBoxItem("0", '0', _img2LZW.Palette.Colors[0]));
            _cbAlpha.Items.Add(new MyComboBoxItem("1", '1', _img2LZW.Palette.Colors[1]));
            _cbAlpha.Items.Add(new MyComboBoxItem("2", '2', _img2LZW.Palette.Colors[2]));
            _cbAlpha.Items.Add(new MyComboBoxItem("3", '3', _img2LZW.Palette.Colors[3]));
            _cbAlpha.Items.Add(new MyComboBoxItem("4", '4', _img2LZW.Palette.Colors[4]));
            _cbAlpha.Items.Add(new MyComboBoxItem("5", '5', _img2LZW.Palette.Colors[5]));
            _cbAlpha.Items.Add(new MyComboBoxItem("6", '6', _img2LZW.Palette.Colors[6]));
            _cbAlpha.Items.Add(new MyComboBoxItem("7", '7', _img2LZW.Palette.Colors[7]));
            _cbAlpha.Items.Add(new MyComboBoxItem("8", '8', _img2LZW.Palette.Colors[8]));
            _cbAlpha.Items.Add(new MyComboBoxItem("9", '9', _img2LZW.Palette.Colors[9]));
            _cbAlpha.Items.Add(new MyComboBoxItem("10", 'a', _img2LZW.Palette.Colors[10]));
            _cbAlpha.Items.Add(new MyComboBoxItem("11", 'b', _img2LZW.Palette.Colors[11]));
            _cbAlpha.Items.Add(new MyComboBoxItem("12", 'c', _img2LZW.Palette.Colors[12]));
            _cbAlpha.Items.Add(new MyComboBoxItem("13", 'd', _img2LZW.Palette.Colors[13]));
            _cbAlpha.Items.Add(new MyComboBoxItem("14", 'e', _img2LZW.Palette.Colors[14]));
            _cbAlpha.Items.Add(new MyComboBoxItem("15", 'f', _img2LZW.Palette.Colors[15]));

            _cbAlpha.SelectedIndex = 0;
        }

        private void UpdateStat(string val)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<string>(UpdateStat), new object[] { val });
                return;
            }
            tsslStatus.Text = val;
            statusStrip1.Refresh();
        }

        private void CopyToClipBoard(string text)
        {
            // Copy data
            try
            {
                Clipboard.SetText(text);
                UpdateStat("Exported to clipboard");
            }
            catch (Exception) { MessageBox.Show("Unable to copy to clipboard"); }
        }

        private void Img2LZW_LogEvent(object sender, EventArgs e)
        {
            // Get the state of the process and update the notifications
            string stat = sender as string;
            stat = stat ?? "";
            UpdateStat(stat);
        }

        #region Background compression / decompression
        private void BwCompress_DoWork(object sender, DoWorkEventArgs e)
        {
            // Process LZW
            _img2LZW.Process(_img);
        }

        private void BwCompress_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // Update images in form
            pbOriginal.Image = _img2LZW.OriginalImage;
            pbElaborated.Image = _img2LZW.DecompressedImage;

            // Load the uncompressed string if it is not too big
            if (_img2LZW.OriginalText.Length < MAX_ORIGINAL_TEXT_LENGTH)
            {
                tbOriginal.Text = _img2LZW.OriginalText;
                tbOriginal.Refresh();
            }
            else
            {
                tbOriginal.Text = _img2LZW.OriginalText.Substring(0, MAX_ORIGINAL_TEXT_LENGTH) + "...";
                tbOriginal.Refresh();
            }
            tbCompressed.Text = _img2LZW.CompressedText;

            // Enable the image export
            if (_img2LZW.DecompressedImage != null)
            {
                tsbExportImage.Enabled = true;
            }
            tsslLoadImages.Text = "";

            // Generate LZW demo for tic
            string imgstring = _img2LZW.CompressedText;
            _srcCode = TicCode.lwzdemoPre + string.Format(TicCode.lwzdemoimgData, imgstring) + TicCode.lwzdemoPost;

            // Cut the data string for a better visualization
            string imgCompressText;
            if (imgstring.Length > 50)
            {
                string compressedstring = imgstring.Substring(0, 10) + " ... " + imgstring.Substring(imgstring.Length - 10, 10);
                imgCompressText = string.Format(TicCode.lwzdemoimgData, compressedstring);
            }
            else
            {
                imgCompressText = string.Format(TicCode.lwzdemoimgData, imgstring);
            }

            // Load sample in the view
            rtbDemo.Text = TicCode.lwzdemoPre + imgCompressText + TicCode.lwzdemoPost;

            // Update status
            statusStrip1.Refresh();
        }
        #endregion

        #region User actions management
        private void SfdImage_FileOk(object sender, CancelEventArgs e)
        {
            // Export the decompressed image
            string fileName = sfdImage.FileName;
            _img2LZW.DecompressedImage.Save(fileName);
        }

        private void CbAlpha_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Update the alpha color
            if (_cbAlpha.SelectedItem is MyComboBoxItem cb)
            {
                string c = cb.Value.ToString();
                _img2LZW.UpdateAlpha(c);
            }
        }

        private void TsbOpenImage_Click(object sender, EventArgs e)
        {
            // Open a dialog to select the image to process
            OpenFileDialog dlg = new OpenFileDialog
            {
                Title = "Open Image",
                Filter = "image files (*.png;*.bmp;*.gif;*.jpg)|*.png;*.bmp;*.gif;*.jpg"
            };

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                _img = Image.FromFile(dlg.FileName);
                if (!bwCompress.IsBusy)
                {
                    bwCompress.RunWorkerAsync();
                }
            }
            dlg.Dispose();
        }

        private void TsbExportImage_Click(object sender, EventArgs e)
        {
            // Open dialog to export decompressed image
            sfdImage.ShowDialog();
        }

        private void BtnCopy_Click(object sender, EventArgs e)
        {
            // Copy data
            CopyToClipBoard(tbCompressed.Text);
        }

        private void TsbCopyCodeExample_Click(object sender, EventArgs e)
        {
            // Copy data
            CopyToClipBoard(_srcCode);
        }

        private void TscbPalettes_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Render again the decompress image but with the selected palette
            if (_img2LZW != null)
            {
                _img2LZW.Palette = (Palette)tscbPalettes.SelectedItem;
                PopulateAlphaComboBox();
                if (_img2LZW.DecompressedImage != null)
                {
                    // Reconstruct image with new palette
                    _img2LZW.AdaptToPalette();
                    _img2LZW.RenderDecompressedImage();
                    pbElaborated.Image = _img2LZW.DecompressedImage;
                }
            }
        }

        private void RtbDemo_Layout(object sender, LayoutEventArgs e)
        {
            // Update rendering of rich text box
            var scanner = new LuaScanner();
            var tokens = scanner.Scan(rtbDemo.Text);

            // Assign colors to code tokens
            foreach (var token in tokens)
            {
                rtbDemo.Select(token.Start, token.Length);
                rtbDemo.SelectionColor = TokenColor(token.Type);
            }
        }

        private Color TokenColor(LuaToken.TokenType type)
        {
            switch (type)
            {
                case LuaToken.TokenType.EQUAL:
                case LuaToken.TokenType.EQUAL_EQUAL:
                case LuaToken.TokenType.BANG:
                case LuaToken.TokenType.BANG_EQUAL:
                case LuaToken.TokenType.LESS_EQUAL:
                case LuaToken.TokenType.GREATER_EQUAL:
                case LuaToken.TokenType.LESS:
                case LuaToken.TokenType.GREATER:
                    return Color.LightGreen;
                case LuaToken.TokenType.COMMENT:
                    return Color.LightGray;
                case LuaToken.TokenType.KEYWORD:
                    return Color.LightSkyBlue;
                case LuaToken.TokenType.STRING:
                    return Color.Coral;
                case LuaToken.TokenType.NUMBER:
                    return Color.Yellow;
                case LuaToken.TokenType.CHAR:
                    return Color.GreenYellow;
            }
            return Color.White;
        }
        #endregion
    }
}
