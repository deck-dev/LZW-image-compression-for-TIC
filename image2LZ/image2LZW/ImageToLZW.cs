using System;
using System.Drawing;
using System.Text;

namespace LZWConverter
{
    public class ImageToLZW
    {
        private const int DICT_SIZE = 0xFFF + 1;  // number of F determines format size
        private const int FORMAT_SIZE = 3;  // change here accordly to dictionary size
        private const String FORMAT = "{0:X3}";  // change here accordly to dictionary size

        // alpha color
        private Color alpha;

        // TIC palette (DB16)
        private Color black = Color.FromArgb(20, 12, 18);
        private Color plum = Color.FromArgb(68, 36, 52);
        private Color midnight = Color.FromArgb(48, 52, 109);
        private Color iron = Color.FromArgb(78, 74, 78);
        private Color earth = Color.FromArgb(133, 76, 48);
        private Color moss = Color.FromArgb(52, 101, 36);
        private Color berry = Color.FromArgb(208, 70, 72);
        private Color olive = Color.FromArgb(117, 113, 97);
        private Color cornflower = Color.FromArgb(89, 125, 206);
        private Color ocher = Color.FromArgb(210, 125, 44);
        private Color slate = Color.FromArgb(133, 149, 161);
        private Color leaf = Color.FromArgb(109, 170, 44);
        private Color peach = Color.FromArgb(210, 170, 153);
        private Color sky = Color.FromArgb(109, 194, 202);
        private Color maize = Color.FromArgb(218, 212, 94);
        private Color peppermint = Color.FromArgb(222, 238, 214);

        public Bitmap OriginalImage { get; set; }
        public Bitmap DecompressedImage { get; set; }

        public String OriginalText { get; set; }
        public String CompressedText { get; set; }

        public delegate void LogEventHandler(object sender, EventArgs e);
        public event LogEventHandler LogEvent;

        public static Color[] Palette { get; private set; }

        public ImageToLZW()
        {
            // init palette array
            Palette = new Color[] {
                black,
                plum,
                midnight,
                iron,
                earth,
                moss,
                berry,
                olive,
                cornflower,
                ocher,
                slate,
                leaf,
                peach,
                sky,
                maize,
                peppermint
            };

            // init alpha color
            alpha = black;
        }

        public void Process(Image img)
        {
            #region adapt image palette and convert to text
            // create image
            Log("start");
            OriginalImage = new Bitmap(img);
            Bitmap convertedImg = new Bitmap(img);

            // convert image accordly to tic palette            
            for (int i = 0; i < convertedImg.Width * convertedImg.Height; i++)
            {
                int x = i % convertedImg.Width;
                int y = i / convertedImg.Width;
                convertedImg.SetPixel(x, y, ConverToPalette(convertedImg.GetPixel(x, y)));
                int percentage = (int)(100f * i / (convertedImg.Width * convertedImg.Height));

                // notify process
                if (i % 1000 == 0) Log("convert to palette... " + percentage + " %");
            }

            // convert image to text
            OriginalText = "";
            StringBuilder buffer = new StringBuilder(10000);
            for (int i = 0; i < convertedImg.Width * convertedImg.Height; i++)
            {
                int x = i % convertedImg.Width;
                int y = i / convertedImg.Width;
                buffer.Append(ColorToString(convertedImg.GetPixel(x, y)));

                // need to break the text in blocks otherwise the process run slowly
                if (i % 1000 == 0)
                {
                    OriginalText += buffer;
                    buffer = new StringBuilder(10000);
                }

                // notify process
                int percentage = (int)(100f * i / (convertedImg.Width * convertedImg.Height));
                if (i % 10000 == 0) Log("transform image to data... " + percentage + " %");
            }
            OriginalText += buffer;
            #endregion

            #region compression
            // COMPRESSION
            //first data is width and height of the image, 3 digits
            CompressedText = "";
            CompressedText += String.Format(FORMAT, convertedImg.Width);
            CompressedText += String.Format(FORMAT, convertedImg.Height);

            // compress image and convert in char array
            CompressedText += LZWCompress(OriginalText);
            #endregion

            #region decompression
            // DECOMPRESSION
            // decompress from code to image
            //first data is width and height of the image, 3 digits
            int w = Convert.ToInt32(CompressedText.Substring(0, 3), 16);
            int h = Convert.ToInt32(CompressedText.Substring(3, 3), 16);

            String decompress = LZWDecompress(CompressedText);
            #endregion

            #region reconstruct image
            DecompressedImage = new Bitmap(w, h);
            for (int i = 0; i < DecompressedImage.Width * DecompressedImage.Height; i++)
            {
                int x = i % convertedImg.Width;
                int y = i / convertedImg.Width;
                DecompressedImage.SetPixel(x, y, StringToColor(OriginalText[i].ToString()));

                // notify process
                int percentage = (int)(100f * i / (DecompressedImage.Width * DecompressedImage.Height));
                if (i % 1000 == 0) Log("reconstruct image... " + percentage + " %");
            }
            #endregion

            // notify process
            Log("end - compression ratio: " + (int)(100 - 1f * CompressedText.Length / OriginalText.Length * 100) + " %, size " + CompressedText.Length + " chars ");
        }

        public void UpdateAlpha(String c)
        {
            alpha = StringToColor(c);
        }

        private void Log(String txt)
        {
            if (LogEvent != null)
            {
                LogEvent(txt, EventArgs.Empty);
            }
        }

        private String LZWCompress(String txt)
        {
            // initialize array
            int indxDict = 15;
            String[] dict = new String[DICT_SIZE];
            dict[0] = "0";
            dict[1] = "1";
            dict[2] = "2";
            dict[3] = "3";
            dict[4] = "4";
            dict[5] = "5";
            dict[6] = "6";
            dict[7] = "7";
            dict[8] = "8";
            dict[9] = "9";
            dict[10] = "A";
            dict[11] = "B";
            dict[12] = "C";
            dict[13] = "D";
            dict[14] = "E";
            dict[15] = "F";

            // process lzw
            StringBuilder output = new StringBuilder(txt.Length * 10);
            char ch = ' ';
            String s = "";

            for (int i = 0; i < txt.Length; i++)
            {
                ch = txt[i];

                if (FindString(dict, s + ch) >= 0)
                {
                    s += ch;
                }
                else
                {
                    output.AppendFormat(FORMAT, FindString(dict, s));
                    dict[++indxDict] = s + ch;
                    s = "" + ch;
                }

                if (indxDict >= DICT_SIZE - 2)
                {
                    // flush dictionary
                    output.AppendFormat(FORMAT, FindString(dict, s));
                    output.AppendFormat(FORMAT, DICT_SIZE - 1);
                    s = "";
                    indxDict = 15;
                    for (int j = 16; j < dict.Length; j++) dict[j] = null;
                }

                // notify process
                int percentage = (int)(100f * i / txt.Length);
                if (i % 1000 == 0) Log("compress data... " + percentage + " %");
            }
            output.AppendFormat(FORMAT, FindString(dict, s));

            return output.ToString();
        }

        private String LZWDecompress(String code)
        {
            if (code == null)
                return null;

            // initialize dictionary
            int indxDict = 15;
            String[] dict = new String[DICT_SIZE];
            dict[0] = "0";
            dict[1] = "1";
            dict[2] = "2";
            dict[3] = "3";
            dict[4] = "4";
            dict[5] = "5";
            dict[6] = "6";
            dict[7] = "7";
            dict[8] = "8";
            dict[9] = "9";
            dict[10] = "A";
            dict[11] = "B";
            dict[12] = "C";
            dict[13] = "D";
            dict[14] = "E";
            dict[15] = "F";

            // decompress data
            int indxCode = 6; // first 6 chars are width and height of image
            StringBuilder output = new StringBuilder(code.Length * 10);
            int prevCode = -1;
            int currCode = -1;

            prevCode = Convert.ToInt32(code.Substring(indxCode, FORMAT_SIZE), 16);
            indxCode += FORMAT_SIZE;
            output.Append(dict[prevCode]);
            while (indxCode < code.Length)
            {
                currCode = Convert.ToInt32(code.Substring(indxCode, FORMAT_SIZE), 16);
                indxCode += FORMAT_SIZE;
                if (currCode == DICT_SIZE - 1)
                {
                    // flush dictionary
                    indxDict = 15;
                    for (int j = 16; j < dict.Length; j++) dict[j] = null;
                    prevCode = Convert.ToInt32(code.Substring(indxCode, FORMAT_SIZE), 16);
                    indxCode += FORMAT_SIZE;
                    output.Append(dict[prevCode]);
                }
                else if (dict[currCode] != null)
                {
                    output.Append(dict[currCode]);
                    dict[++indxDict] = dict[prevCode] + dict[currCode][0];
                    prevCode = currCode;
                }
                else
                {
                    dict[++indxDict] = dict[prevCode] + dict[prevCode][0];
                    output.Append(dict[currCode]);
                    prevCode = currCode;
                }

                // notify process
                int percentage = (int)(100f * indxCode / code.Length);
                if (indxCode % 1000 == 0) Log("decompress data... " + percentage + " %");
            }
            return output.ToString();
        }

        private int FindString(String[] dict, String txt)
        {
            for (int i = 0; i < dict.Length; i++)
            {
                if (dict[i] != null && dict[i].Equals(txt))
                {
                    return i;
                }
            }
            return -1;
        }

        private Color ConverToPalette(Color c)
        {
            int minIndx = 0;
            double minDst = double.MaxValue;
            for (int i = 0; i < Palette.Length; i++)
            {
                if (c.A != 255)
                {
                    return alpha;
                }
                else if (ColorDst(c, Palette[i]) < minDst)
                {
                    minDst = ColorDst(c, Palette[i]);
                    minIndx = i;
                }
            }
            return Palette[minIndx];
        }

        private double ColorDst(Color a, Color b)
        {
            int dr = Math.Abs(a.R - b.R);
            int dg = Math.Abs(a.G - b.G);
            int db = Math.Abs(a.B - b.B);
            return Math.Sqrt(dr * dr + dg * dg + db * db);
        }

        private String ColorToString(Color c)
        {
            for (int i = 0; i < Palette.Length; i++)
            {
                if (c == Palette[i])
                {
                    return String.Format("{0:X1}", i);
                }
            }
            return "0";
        }

        private Color StringToColor(String c)
        {
            switch (c)
            {
                case "0":
                    return Palette[0];
                case "1":
                    return Palette[1];
                case "2":
                    return Palette[2];
                case "3":
                    return Palette[3];
                case "4":
                    return Palette[4];
                case "5":
                    return Palette[5];
                case "6":
                    return Palette[6];
                case "7":
                    return Palette[7];
                case "8":
                    return Palette[8];
                case "9":
                    return Palette[9];
                case "A":
                    return Palette[10];
                case "B":
                    return Palette[11];
                case "C":
                    return Palette[12];
                case "D":
                    return Palette[13];
                case "E":
                    return Palette[14];
                case "F":
                    return Palette[15];
                default:
                    return Palette[0];
            }
        }
    }
}
