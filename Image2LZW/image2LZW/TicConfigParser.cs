using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace LZWConverter
{
    class TicConfigParser
    {
        private readonly string filePath;

        public List<Palette> Palettes { get; set; }

        public TicConfigParser(string filePath)
        {
            this.filePath = filePath;
            Palettes = new List<Palette>();
            Process();
        }

        private void Process()
        {
            string[] lines = File.ReadAllLines(filePath);
            int i = 0;
            while (i < lines.Length - 2)
            {
                // is a variable, maybe a palette, save name
                if (lines[i].Contains("local"))
                {
                    // if we have two rows with 8 datas, it should be a palette
                    if (lines[i + 1].Split(new string[] { ".." }, StringSplitOptions.RemoveEmptyEntries).Length == 8 &&
                        lines[i + 2].Split(new string[] { ".." }, StringSplitOptions.RemoveEmptyEntries).Length == 8)
                    {
                        Palette palette = new Palette(lines[i].Split(' ')[1].Replace('=', ' ').Trim());
                        ParseColors(lines[i + 1], 0, palette);
                        ParseColors(lines[i + 2], 8, palette);
                        Palettes.Add(palette);
                        i += 2;
                    }
                }
                i++;
            }
        }

        private void ParseColors(string line, int offset, Palette palette)
        {
            string[] colors;
            colors = line.Split(new string[] { ".." }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < colors.Length; i++)
            {
                string color = colors[i].Trim();
                int r = Convert.ToInt32(color.Substring(1, 2), 16);
                int g = Convert.ToInt32(color.Substring(3, 2), 16);
                int b = Convert.ToInt32(color.Substring(5, 2), 16);
                palette.Colors[i + offset] = Color.FromArgb(r, g, b);
            }
        }
    }
}
