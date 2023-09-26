using System.Drawing;

namespace LZWConverter
{
    public class Palette
    {
        public string Name { get; set; }
        public Color[] Colors { get; set; }

        public Palette(string name)
        {
            Name = name;
            Colors = new Color[16];
            for (int i = 0; i < Colors.Length; i++)
            {
                Colors[i] = Color.Black;
            }
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
