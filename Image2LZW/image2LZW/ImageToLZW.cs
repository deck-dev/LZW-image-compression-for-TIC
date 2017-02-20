using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;
using System.Threading;

namespace LZWConverter
{
    public class ImageToLZW
    {
        private const int DICT_SIZE = 0xFFF + 1;  // number of F determines format size
        private const int FORMAT_SIZE = 3;  // change here accordly to dictionary size
        private const String FORMAT = "{0:x3}";  // change here accordly to dictionary size

        // alpha color
        private Color alpha;

        // image dimensions
        private int w;
        private int h;

        private Bitmap convertedImg;

        public Bitmap OriginalImage { get; set; }
        public Bitmap DecompressedImage { get; set; }

        LockBmp lockConverted;

        public String OriginalText { get; set; }
        public String CompressedText { get; set; }

        public delegate void LogEventHandler(object sender, EventArgs e);
        public event LogEventHandler LogEvent;

        public Palette Palette { get; set; }

        public ImageToLZW()
        {
            // init palette array
            Palette = new Palette("TIC");
            Palette.Colors = new Color[] {
                Color.FromArgb(20, 12, 18),
                Color.FromArgb(68, 36, 52),
                Color.FromArgb(48, 52, 109),
                Color.FromArgb(78, 74, 78),
                Color.FromArgb(133, 76, 48),
                Color.FromArgb(52, 101, 36),
                Color.FromArgb(208, 70, 72),
                Color.FromArgb(117, 113, 97),
                Color.FromArgb(89, 125, 206),
                Color.FromArgb(210, 125, 44),
                Color.FromArgb(133, 149, 161),
                Color.FromArgb(109, 170, 44),
                Color.FromArgb(210, 170, 153),
                Color.FromArgb(109, 194, 202),
                Color.FromArgb(218, 212, 94),
                Color.FromArgb(222, 238, 214)
            };

            // init alpha color
            alpha = Palette.Colors[0];
        }

        public unsafe void Process(Image img)
        {
            #region adapt image palette and convert to text
            // create image
            Log("start");
            convertedImg = new Bitmap(img);
            OriginalImage = new Bitmap(img);

            lockConverted = new LockBmp((Bitmap)convertedImg);
            lockConverted.LockBits();

            // convert image accordly to tic palette            
            AdaptToPalette();

            // convert image to text
            OriginalText = "";
            StringBuilder buffer = new StringBuilder(1000);

            for (int i = 0; i < lockConverted.Width * lockConverted.Height; i++)
            {
                int x = i % lockConverted.Width;
                int y = i / lockConverted.Width;
                buffer.Append(ColorToString(lockConverted.GetPixel(x, y)));

                // need to break the text in blocks otherwise the process run slowly
                if (i % 1000 == 0)
                {
                    OriginalText += buffer;
                    buffer = new StringBuilder(1000);
                }

                // notify process               
                if (i % 1000 == 0)
                {
                    int percentage = (int)(100f * i / (lockConverted.Width * lockConverted.Height));
                    Log("transform image to data... " + percentage + " %");
                }
            }
            OriginalText += buffer;
            #endregion

            #region compression
            // COMPRESSION
            //first data is width and height of the image, 3 digits
            CompressedText = "";
            CompressedText += String.Format(FORMAT, lockConverted.Width);
            CompressedText += String.Format(FORMAT, lockConverted.Height);

            // compress image and convert in char array
            CompressedText += LZWCompress(OriginalText);
            #endregion

            #region decompression
            // DECOMPRESSION
            // decompress from code to image
            //first data is width and height of the image, 3 digits
            w = Convert.ToInt32(CompressedText.Substring(0, 3), 16);
            h = Convert.ToInt32(CompressedText.Substring(3, 3), 16);

            String decompress = LZWDecompress(CompressedText);
            #endregion

            #region reconstruct image
            // render the image from decompressed data
            RenderDecompressedImage();

            //Unlock
            lockConverted.UnlockBits();
            #endregion

            // notify process
            Log("end - compression ratio: " + (int)(100 - 1f * CompressedText.Length / OriginalText.Length * 100) + " %, size " + CompressedText.Length + " chars ");
        }

        public void UpdateAlpha(String c)
        {
            alpha = StringToColor(c);
        }

        public void AdaptToPalette()
        {
            for (int i = 0; i < lockConverted.Width * lockConverted.Height; i++)
            {
                int x = i % lockConverted.Width;
                int y = i / lockConverted.Width;
                lockConverted.SetPixel(x, y, ConverToPalette(lockConverted.GetPixel(x, y)));

                // notify process                
                if (i % 1000 == 0)
                {
                    int percentage = (int)(100f * i / (lockConverted.Width * lockConverted.Height));
                    Log("convert to palette... " + percentage + " %");
                }
            }
        }

        public void RenderDecompressedImage()
        {
            DecompressedImage = new Bitmap(w, h);
            for (int i = 0; i < DecompressedImage.Width * DecompressedImage.Height; i++)
            {
                int x = i % lockConverted.Width;
                int y = i / lockConverted.Width;
                DecompressedImage.SetPixel(x, y, StringToColor(OriginalText[i].ToString()));

                // notify process               
                if (i % 1000 == 0)
                {
                    int percentage = (int)(100f * i / (DecompressedImage.Width * DecompressedImage.Height));
                    Log("reconstruct image... " + percentage + " %");
                }
            }
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
            dict[10] = "a";
            dict[11] = "b";
            dict[12] = "c";
            dict[13] = "d";
            dict[14] = "e";
            dict[15] = "f";

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
                if (i % 1000 == 0)
                {
                    int percentage = (int)(100f * i / txt.Length);
                    Log("compress data... " + percentage + " %");
                }
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
            dict[10] = "a";
            dict[11] = "b";
            dict[12] = "c";
            dict[13] = "d";
            dict[14] = "e";
            dict[15] = "f";

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
                if (indxCode % 1000 == 0)
                {
                    int percentage = (int)(100f * indxCode / code.Length);
                    Log("decompress data... " + percentage + " %");
                }
            }
            return output.ToString();
        }

        private int FindString(String[] dict, String txt)
        {
            for (int i = 0; i < dict.Length; i++)
            {
                if (dict[i] == null) { return -1; }

                if (dict[i].Equals(txt))
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
            double distance;
            for (int i = 0; i < Palette.Colors.Length; i++)
            {
                if (c.A != 255)
                {
                    return alpha;
                }
                else
                {
                    distance = ColorDst(c, Palette.Colors[i]);
                    if (distance < minDst)
                    {
                        minDst = distance;
                        minIndx = i;
                    }
                }
            }
            return Palette.Colors[minIndx];
        }

        private double ColorDst(Color a, Color b)
        {
            int dr = (a.R - b.R);
            int dg = (a.G - b.G);
            int db = (a.B - b.B);
            return (dr * dr + dg * dg + db * db);
        }

        private String ColorToString(Color c)
        {
            for (int i = 0; i < Palette.Colors.Length; i++)
            {
                if (c == Palette.Colors[i])
                {
                    return String.Format("{0:x1}", i);
                }
            }
            return "0";
        }

        private Color StringToColor(String c)
        {
            switch (c)
            {
                case "0":
                    return Palette.Colors[0];
                case "1":
                    return Palette.Colors[1];
                case "2":
                    return Palette.Colors[2];
                case "3":
                    return Palette.Colors[3];
                case "4":
                    return Palette.Colors[4];
                case "5":
                    return Palette.Colors[5];
                case "6":
                    return Palette.Colors[6];
                case "7":
                    return Palette.Colors[7];
                case "8":
                    return Palette.Colors[8];
                case "9":
                    return Palette.Colors[9];
                case "a":
                    return Palette.Colors[10];
                case "b":
                    return Palette.Colors[11];
                case "c":
                    return Palette.Colors[12];
                case "d":
                    return Palette.Colors[13];
                case "e":
                    return Palette.Colors[14];
                case "f":
                    return Palette.Colors[15];
                default:
                    return Palette.Colors[0];
            }
        }
    }
}
