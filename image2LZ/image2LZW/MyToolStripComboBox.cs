using System.Drawing;
using System.Windows.Forms;

namespace LZWConverter
{
    internal class MyToolStripComboBox : ToolStripControlHost
    {
        public MyToolStripComboBox() : base(new MyComboBox()) { }

        public MyComboBox ComboBox
        {
            get
            {
                return Control as MyComboBox;
            }
        }
    }

    public class MyComboBox : ComboBox
    {
        public MyComboBox()
        {
            this.DrawMode = DrawMode.OwnerDrawFixed;
        }

        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            base.OnDrawItem(e);
            e.DrawBackground();
            MyComboBoxItem item = (MyComboBoxItem)this.Items[e.Index];
            Brush brush = new SolidBrush(item.BackColor);
            e.Graphics.FillRectangle(brush, e.Bounds);
            Brush fontBrush = item.BackColor.GetBrightness() > 0.5f ? Brushes.Black : Brushes.White;
            e.Graphics.DrawString(item.Text, this.Font, fontBrush, e.Bounds.X, e.Bounds.Y);
        }
    }

    public class MyComboBoxItem
    {
        public string Text { get; set; }
        public object Value { get; set; }
        public Color BackColor { get; set; }

        public MyComboBoxItem() { }

        public MyComboBoxItem(string pText, object pValue)
        {
            Text = pText; Value = pValue;
        }

        public MyComboBoxItem(string pText, object pValue, Color pColor)
        {
            Text = pText; Value = pValue; BackColor = pColor;
        }

        public override string ToString()
        {
            return Text;
        }
    }
}
