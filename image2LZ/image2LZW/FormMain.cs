using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace LZWConverter
{
    public partial class FormMain : Form
    {
        private Image img;
        private ImageToLZW img2LZW;
        private MyComboBox cbAlpha;

        public FormMain()
        {
            InitializeComponent();

            img2LZW = new ImageToLZW();
            // used to notify current state of the compression/decompression process
            img2LZW.LogEvent += new ImageToLZW.LogEventHandler(Img2LZW_LogEvent);

            // add the custom combo box
            MyToolStripComboBox tscbAlpha = new MyToolStripComboBox();
            cbAlpha = tscbAlpha.ComboBox;
            cbAlpha.DropDownStyle = ComboBoxStyle.DropDownList;
            cbAlpha.Items.Add(new MyComboBoxItem("black", '0', ImageToLZW.Palette[0]));
            cbAlpha.Items.Add(new MyComboBoxItem("plum", '1', ImageToLZW.Palette[1]));
            cbAlpha.Items.Add(new MyComboBoxItem("midnight", '2', ImageToLZW.Palette[2]));
            cbAlpha.Items.Add(new MyComboBoxItem("iron", '3', ImageToLZW.Palette[3]));
            cbAlpha.Items.Add(new MyComboBoxItem("earth", '4', ImageToLZW.Palette[4]));
            cbAlpha.Items.Add(new MyComboBoxItem("moss", '5', ImageToLZW.Palette[5]));
            cbAlpha.Items.Add(new MyComboBoxItem("berry", '6', ImageToLZW.Palette[6]));
            cbAlpha.Items.Add(new MyComboBoxItem("olive", '7', ImageToLZW.Palette[7]));
            cbAlpha.Items.Add(new MyComboBoxItem("cornflower", '8', ImageToLZW.Palette[8]));
            cbAlpha.Items.Add(new MyComboBoxItem("ocher", '9', ImageToLZW.Palette[9]));
            cbAlpha.Items.Add(new MyComboBoxItem("slate", 'A', ImageToLZW.Palette[10]));
            cbAlpha.Items.Add(new MyComboBoxItem("leaf", 'B', ImageToLZW.Palette[11]));
            cbAlpha.Items.Add(new MyComboBoxItem("peach", 'C', ImageToLZW.Palette[12]));
            cbAlpha.Items.Add(new MyComboBoxItem("sky", 'D', ImageToLZW.Palette[13]));
            cbAlpha.Items.Add(new MyComboBoxItem("maze", 'E', ImageToLZW.Palette[14]));
            cbAlpha.Items.Add(new MyComboBoxItem("peppermint", 'F', ImageToLZW.Palette[15]));
            cbAlpha.SelectedIndex = 0;
            cbAlpha.SelectedIndexChanged += cbAlpha_SelectedIndexChanged;
            toolStrip1.Items.Add(tscbAlpha);
        }


        private void Img2LZW_LogEvent(object sender, EventArgs e)
        {
            // get the state of the process and update the notifications
            String stat = sender as String;
            stat = stat != null ? stat : "";
            UpdateStat(stat);
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

        private void sfdImage_FileOk(object sender, CancelEventArgs e)
        {
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
            OpenFileDialog dlg = new OpenFileDialog();

            dlg.Title = "Open Image";
            dlg.Filter = "image files (*.png;*.bmp;*.gif;*.jpg)|*.png;*.bmp;*.gif;*.jpg";

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                // process LZW
                img = Image.FromFile(dlg.FileName);
                img2LZW.Process(img);

                // update panels contents
                tsslLoadImages.Text = "loading data 1 of 4";
                statusStrip1.Refresh();
                pbOriginal.Image = img2LZW.OriginalImage;

                tsslLoadImages.Text = "loading data 2 of 4";
                statusStrip1.Refresh();
                pbElaborated.Image = img2LZW.DecompressedImage;

                tsslLoadImages.Text = "loading data 3 of 4";
                statusStrip1.Refresh();
                // load the uncompressed string if it is not too big
                if (img2LZW.OriginalText.Length < 1000000)
                {
                    tbOriginal.Text = img2LZW.OriginalText;
                }
                else
                {
                    tbOriginal.Text = "Too big to be displayed";
                }

                tsslLoadImages.Text = "loading data 4 of 4";
                statusStrip1.Refresh();
                tbCompressed.Text = img2LZW.CompressedText;

                tsslLoadImages.Text = "";
                statusStrip1.Refresh();

                // enable the image export
                if (img2LZW.DecompressedImage != null)
                {
                    tsbExportImage.Enabled = true;
                }
            }
            dlg.Dispose();
        }

        private void tsbExportImage_Click(object sender, EventArgs e)
        {
            sfdImage.ShowDialog();
        }

        private void tsbCopyToClipboard_Click(object sender, EventArgs e)
        {
            try
            {
                Clipboard.SetText(tbCompressed.Text);
                UpdateStat("Exported to clipboard");
            }
            catch (Exception) { MessageBox.Show("Unable to copy to clipboard"); }
        }
    }
}
