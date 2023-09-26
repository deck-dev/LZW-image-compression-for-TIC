using System;
using System.Drawing;
using System.Text;

namespace LZWConverter
{
    public class ImageToLZW
    {
        private const int DICT_SIZE = 0xFFF + 1;  // Number of Fs determines format size
        private const int FORMAT_SIZE = 3;  // Change here accordly to dictionary size
        private const string FORMAT = "{0:x3}";  // Change here accordly to dictionary size

        public Bitmap OriginalImage { get; set; }
        public Bitmap DecompressedImage { get; set; }
        public string OriginalText { get; set; }
        public string CompressedText { get; set; }
        public delegate void LogEventHandler(object sender, EventArgs e);
        public event LogEventHandler LogEvent;
        public Palette Palette { get; set; }

        // Alpha color
        private Color _alpha;

        // Image dimensions
        private int _width;
        private int _height;
        private string _decompressedText;

        private Bitmap _convertedImg;
        private LockBmp _lockConverted;

        public ImageToLZW()
        {
            // Init palette array
            Palette = new Palette("TIC")
            {
                Colors = new Color[] {
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
                }
            };

            // Init alpha color
            _alpha = Palette.Colors[0];
        }

        public unsafe void Process(Image img)
        {
            #region Adapt image palette and convert to text
            // Create image
            Log("start");
            _convertedImg = new Bitmap(img);
            OriginalImage = new Bitmap(img);

            _lockConverted = new LockBmp((Bitmap)_convertedImg);
            _lockConverted.LockBits();

            // Convert image accordly to tic palette            
            AdaptToPalette();

            // Convert image to text
            OriginalText = "";
            StringBuilder buffer = new StringBuilder(1000);

            for (int i = 0; i < _lockConverted.Width * _lockConverted.Height; i++)
            {
                int x = i % _lockConverted.Width;
                int y = i / _lockConverted.Width;
                buffer.Append(ColorTostring(_lockConverted.GetPixel(x, y)));

                // Need to break the text in blocks otherwise the process run slowly
                if (i % 1000 == 0)
                {
                    OriginalText += buffer;
                    buffer = new StringBuilder(1000);
                }

                // Notify process               
                if (i % 1000 == 0)
                {
                    int percentage = (int)(100f * i / (_lockConverted.Width * _lockConverted.Height));
                    Log("transform image to data... " + percentage + " %");
                }
            }
            OriginalText += buffer;
            #endregion

            #region Compression
            // COMPRESSION
            // First data is width and height of the image, 3 digits
            CompressedText = "";
            CompressedText += string.Format(FORMAT, _lockConverted.Width);
            CompressedText += string.Format(FORMAT, _lockConverted.Height);

            // Compress image and convert in char array
            CompressedText += LZWCompress(OriginalText);
            #endregion

            #region Decompression
            // DECOMPRESSION
            // Decompress from code to image
            // First data is width and height of the image, 3 digits
            _width = Convert.ToInt32(CompressedText.Substring(0, 3), 16);
            _height = Convert.ToInt32(CompressedText.Substring(3, 3), 16);
            _decompressedText = LZWDecompress(CompressedText);
            #endregion

            #region Reconstruct image
            // Render the image from decompressed data
            RenderDecompressedImage();

            // Unlock
            _lockConverted.UnlockBits();
            #endregion

            // Notify process
            Log("end - compression ratio: " + (int)(100 - 1f * CompressedText.Length / OriginalText.Length * 100) + " %, size " + CompressedText.Length + " chars ");
        }

        public void UpdateAlpha(string c)
        {
            _alpha = StringToColor(c);
        }

        public void AdaptToPalette()
        {
            for (int i = 0; i < _lockConverted.Width * _lockConverted.Height; i++)
            {
                int x = i % _lockConverted.Width;
                int y = i / _lockConverted.Width;
                _lockConverted.SetPixel(x, y, ConverToPalette(_lockConverted.GetPixel(x, y)));

                // Notify process                
                if (i % 1000 == 0)
                {
                    int percentage = (int)(100f * i / (_lockConverted.Width * _lockConverted.Height));
                    Log("convert to palette... " + percentage + " %");
                }
            }
        }

        public void RenderDecompressedImage()
        {
            DecompressedImage = new Bitmap(_width, _height);
            for (int i = 0; i < DecompressedImage.Width * DecompressedImage.Height; i++)
            {
                int x = i % _lockConverted.Width;
                int y = i / _lockConverted.Width;
                DecompressedImage.SetPixel(x, y, StringToColor(_decompressedText[i].ToString()));

                // Notify process               
                if (i % 1000 == 0)
                {
                    int percentage = (int)(100f * i / (DecompressedImage.Width * DecompressedImage.Height));
                    Log("reconstruct image... " + percentage + " %");
                }
            }
        }

        private void Log(string txt)
        {
            LogEvent?.Invoke(txt, EventArgs.Empty);
        }

        private string LZWCompress(string txt)
        {
            // Initialize array
            int indxDict = 15;
            string[] dict = new string[DICT_SIZE];
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

            // Process LZW
            StringBuilder output = new StringBuilder(txt.Length * 10);
            string s = "";

            for (int i = 0; i < txt.Length; i++)
            {
                char ch = txt[i];

                if (Findstring(dict, s + ch) >= 0)
                {
                    s += ch;
                }
                else
                {
                    output.AppendFormat(FORMAT, Findstring(dict, s));
                    dict[++indxDict] = s + ch;
                    s = "" + ch;
                }

                if (indxDict >= DICT_SIZE - 2)
                {
                    // Flush dictionary
                    output.AppendFormat(FORMAT, Findstring(dict, s));
                    output.AppendFormat(FORMAT, DICT_SIZE - 1);
                    s = "";
                    indxDict = 15;
                    for (int j = 16; j < dict.Length; j++) dict[j] = null;
                }

                // Notify process
                if (i % 1000 == 0)
                {
                    int percentage = (int)(100f * i / txt.Length);
                    Log("compress data... " + percentage + " %");
                }
            }
            output.AppendFormat(FORMAT, Findstring(dict, s));

            return output.ToString();
        }

        private string LZWDecompress(string code)
        {
            if (code == null)
                return null;

            // Initialize dictionary
            int indxDict = 15;
            string[] dict = new string[DICT_SIZE];
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

            // Decompress data
            int indxCode = 6; // First 6 chars are width and height of image
            StringBuilder output = new StringBuilder(code.Length * 10);
            int prevCode = Convert.ToInt32(code.Substring(indxCode, FORMAT_SIZE), 16);
            indxCode += FORMAT_SIZE;
            output.Append(dict[prevCode]);

            while (indxCode < code.Length)
            {
                int currCode = Convert.ToInt32(code.Substring(indxCode, FORMAT_SIZE), 16);
                indxCode += FORMAT_SIZE;
                if (currCode == DICT_SIZE - 1)
                {
                    // Flush dictionary
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

                // Notify process                
                if (indxCode % 1000 == 0)
                {
                    int percentage = (int)(100f * indxCode / code.Length);
                    Log("decompress data... " + percentage + " %");
                }
            }
            return output.ToString();
        }

        private int Findstring(string[] dict, string txt)
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
                    return _alpha;
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

        private string ColorTostring(Color c)
        {
            for (int i = 0; i < Palette.Colors.Length; i++)
            {
                if (c == Palette.Colors[i])
                {
                    return string.Format("{0:x1}", i);
                }
            }
            return "0";
        }

        private Color StringToColor(string c)
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
