﻿using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace LZWConverter
{
    public partial class FormMain : Form
    {
        private Image img;
        private ImageToLZW img2LZW;
        private MyComboBox cbAlpha;

        private string sourceCode;

        private Color keywordsColor;
        private Color commentsColor;

        public FormMain()
        {
            InitializeComponent();

            // create the lzw manager
            img2LZW = new ImageToLZW();
            // used to notify current state of the compression/decompression process
            img2LZW.LogEvent += new ImageToLZW.LogEventHandler(Img2LZW_LogEvent);

            // add the custom combo box
            MyToolStripComboBox tscbAlpha = new MyToolStripComboBox();
            cbAlpha = tscbAlpha.ComboBox;
            cbAlpha.DropDownStyle = ComboBoxStyle.DropDownList;
            PopulateAlphaComboBox();
            cbAlpha.ItemHeight = 18;
            cbAlpha.SelectedIndexChanged += cbAlpha_SelectedIndexChanged;
            toolStrip1.Items.Add(tscbAlpha);

            // set the lua keywords and comments colors
            keywordsColor = Color.DeepSkyBlue;
            commentsColor = Color.DarkGray;

            // read the configuration and extract the palettes
            try
            {
                TicConfigParser parser = new TicConfigParser(Directory.GetCurrentDirectory() + "\\config.lua");
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
            cbAlpha.Items.Clear();
            cbAlpha.Items.Add(new MyComboBoxItem("0", '0', img2LZW.Palette.Colors[0]));
            cbAlpha.Items.Add(new MyComboBoxItem("1", '1', img2LZW.Palette.Colors[1]));
            cbAlpha.Items.Add(new MyComboBoxItem("2", '2', img2LZW.Palette.Colors[2]));
            cbAlpha.Items.Add(new MyComboBoxItem("3", '3', img2LZW.Palette.Colors[3]));
            cbAlpha.Items.Add(new MyComboBoxItem("4", '4', img2LZW.Palette.Colors[4]));
            cbAlpha.Items.Add(new MyComboBoxItem("5", '5', img2LZW.Palette.Colors[5]));
            cbAlpha.Items.Add(new MyComboBoxItem("6", '6', img2LZW.Palette.Colors[6]));
            cbAlpha.Items.Add(new MyComboBoxItem("7", '7', img2LZW.Palette.Colors[7]));
            cbAlpha.Items.Add(new MyComboBoxItem("8", '8', img2LZW.Palette.Colors[8]));
            cbAlpha.Items.Add(new MyComboBoxItem("9", '9', img2LZW.Palette.Colors[9]));
            cbAlpha.Items.Add(new MyComboBoxItem("10", 'a', img2LZW.Palette.Colors[10]));
            cbAlpha.Items.Add(new MyComboBoxItem("11", 'b', img2LZW.Palette.Colors[11]));
            cbAlpha.Items.Add(new MyComboBoxItem("12", 'c', img2LZW.Palette.Colors[12]));
            cbAlpha.Items.Add(new MyComboBoxItem("13", 'd', img2LZW.Palette.Colors[13]));
            cbAlpha.Items.Add(new MyComboBoxItem("14", 'e', img2LZW.Palette.Colors[14]));
            cbAlpha.Items.Add(new MyComboBoxItem("15", 'f', img2LZW.Palette.Colors[15]));

            cbAlpha.SelectedIndex = 0;
        }

        private void UpdateStat(String val)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<string>(UpdateStat), new object[] { val });
                return;
            }
            tsslStatus.Text = val;
            statusStrip1.Refresh();
        }

        private void HighlightKeyword()
        {
            // these are the reserved strings of lua
            String keywords = "\\b(and|break|do|else|elseif|end|false|for|function|if|in|local|nil|not|or|repeat|return|then|true|until|while)\\b";

            Regex regExp = new Regex(keywords);

            foreach (Match match in regExp.Matches(rtbDemo.Text))
            {
                rtbDemo.Select(match.Index, match.Length);
                rtbDemo.SelectionFont = new Font(rtbDemo.Font, FontStyle.Bold);
                rtbDemo.SelectionColor = keywordsColor;
            }
        }

        private void HighlightComments()
        {
            for (int i = 0; i < rtbDemo.Lines.Length; i++)
            {
                String line = rtbDemo.Lines[i];
                int indx = line.IndexOf("--");
                if (indx >= 0)
                {
                    rtbDemo.Select(rtbDemo.GetFirstCharIndexFromLine(i) + indx, line.Length - indx);
                    rtbDemo.SelectionFont = new Font(rtbDemo.Font, FontStyle.Italic);
                    rtbDemo.SelectionColor = commentsColor;
                    rtbDemo.Select(rtbDemo.GetFirstCharIndexFromLine(i) + indx, 0);
                    rtbDemo.SelectionFont = new Font(rtbDemo.Font, FontStyle.Regular);
                    rtbDemo.SelectionColor = Color.Black;
                }
            }
        }

        private void HighlightSampleCode()
        {
            // highlights the reserved keywords of lua
            HighlightKeyword();
            // comments
            HighlightComments();
        }

        private void CopyToClipBoard(string text)
        {
            // copy data
            try
            {
                Clipboard.SetText(text);
                UpdateStat("Exported to clipboard");
            }
            catch (Exception) { MessageBox.Show("Unable to copy to clipboard"); }
        }

        private void Img2LZW_LogEvent(object sender, EventArgs e)
        {
            // get the state of the process and update the notifications
            String stat = sender as String;
            stat = stat != null ? stat : "";
            UpdateStat(stat);
        }

        #region background compression / decompression
        private void bwCompress_DoWork(object sender, DoWorkEventArgs e)
        {
            // process LZW
            img2LZW.Process(img);
        }

        private void bwCompress_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // update images in form
            pbOriginal.Image = (Bitmap)img2LZW.OriginalImage;
            pbElaborated.Image = (Bitmap)img2LZW.DecompressedImage;

            // load the uncompressed string if it is not too big
            if (img2LZW.OriginalText.Length < 1000000)
            {
                tbOriginal.Text = img2LZW.OriginalText;
                tbOriginal.Refresh();
            }
            else
            {
                tbOriginal.Text = "Too big to be displayed";
                tbOriginal.Refresh();
            }
            tbCompressed.Text = img2LZW.CompressedText;

            // enable the image export
            if (img2LZW.DecompressedImage != null)
            {
                tsbExportImage.Enabled = true;
            }
            tsslLoadImages.Text = "";

            // generate LZW demo for tic
            string imgString = img2LZW.CompressedText;
            sourceCode = TicCode.lwzdemoPre + String.Format(TicCode.lwzdemoimgData, imgString) + TicCode.lwzdemoPost;

            // cut the data string for a better visualization
            string imgCompressText = "";
            if (imgString.Length > 50)
            {
                string compressedString = imgString.Substring(0, 10) + " ... " + imgString.Substring(imgString.Length - 10, 10);
                imgCompressText = String.Format(TicCode.lwzdemoimgData, compressedString);
            }
            else
            {
                imgCompressText = String.Format(TicCode.lwzdemoimgData, imgString);
            }

            // load sample in the view
            rtbDemo.Text = TicCode.lwzdemoPre + imgCompressText + TicCode.lwzdemoPost;

            // update status
            statusStrip1.Refresh();
        }
        #endregion

        #region user actions management
        private void sfdImage_FileOk(object sender, CancelEventArgs e)
        {
            // export the decompressed image
            String fileName = sfdImage.FileName;
            img2LZW.DecompressedImage.Save(fileName);
        }

        private void cbAlpha_SelectedIndexChanged(object sender, EventArgs e)
        {
            // update the alpha color
            MyComboBoxItem cb = cbAlpha.SelectedItem as MyComboBoxItem;
            if (cb != null)
            {
                String c = cb.Value.ToString();
                img2LZW.UpdateAlpha(c);
            }
        }

        private void tsbOpenImage_Click(object sender, EventArgs e)
        {
            // open a dialog to select the image to process
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Title = "Open Image";
            dlg.Filter = "image files (*.png;*.bmp;*.gif;*.jpg)|*.png;*.bmp;*.gif;*.jpg";

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                img = Image.FromFile(dlg.FileName);
                if (!bwCompress.IsBusy)
                {
                    bwCompress.RunWorkerAsync();
                }
            }
            dlg.Dispose();
        }

        private void tsbExportImage_Click(object sender, EventArgs e)
        {
            // open dialog to export decompressed image
            sfdImage.ShowDialog();
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            // copy data
            CopyToClipBoard(tbCompressed.Text);
        }

        private void tsbCopyCodeExample_Click(object sender, EventArgs e)
        {
            // copy data
            CopyToClipBoard(sourceCode);
        }

        private void tscbPalettes_SelectedIndexChanged(object sender, EventArgs e)
        {
            // render again the decompress image but with the selected palette
            if (img2LZW != null)
            {
                img2LZW.Palette = (Palette)tscbPalettes.SelectedItem;
                PopulateAlphaComboBox();
                if (img2LZW.DecompressedImage != null)
                {
                    // reconstruct image with new palette
                    img2LZW.AdaptToPalette();
                    img2LZW.RenderDecompressedImage();
                    pbElaborated.Image = img2LZW.DecompressedImage;
                }
            }
        }

        private void rtbDemo_Layout(object sender, LayoutEventArgs e)
        {
            // update rendering of rich text box
            HighlightSampleCode();
        }
        #endregion
    }
}
