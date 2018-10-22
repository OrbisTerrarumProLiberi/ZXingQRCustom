using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using ZXing;
using ZXing.QrCode.Internal;
using ZXing.Rendering;

using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        private string imagePath = @"sss.jpg";
        private string url = @"Subject: Programming 1(IT102)
Section: BSIT-1M1
Instructor: Alfred J. Paldez
Date:10/19/2018
Session: 7:30AM – 10:30AM
";
        private int size = 250;

        public Form1()
        {
            InitializeComponent();
            pictureBox1.Image = GenerateQR(size, size, url);
            pictureBox1.Height = size;
            pictureBox1.Width = size;
            Console.WriteLine(checkQR(new Bitmap(pictureBox1.Image)));
        }

        public bool checkQR(Bitmap QrCode)
        {
            var reader = new BarcodeReader();
            var result = reader.Decode(QrCode);
            if (result == null)
                return false;
            return result.Text == url;
        }


        public Bitmap GenerateQR(int width, int height, string text)
        {
            var bw = new ZXing.BarcodeWriter();

            var encOptions = new ZXing.Common.EncodingOptions
            {
                Width = width,
                Height = height,
                Margin = 0,
                PureBarcode = false
            };

            encOptions.Hints.Add(EncodeHintType.ERROR_CORRECTION, ErrorCorrectionLevel.H);

            bw.Renderer = new BitmapRenderer();
            bw.Options = encOptions;
            bw.Format = ZXing.BarcodeFormat.QR_CODE;
            Bitmap bm = bw.Write(text);
            Bitmap overlay = new Bitmap(imagePath);

            int deltaHeigth = bm.Height - overlay.Height;
            int deltaWidth = bm.Width - overlay.Width;

            Graphics g = Graphics.FromImage(bm);
            g.DrawImage(overlay, new Point(deltaWidth / 2, deltaHeigth / 2));

            return bm;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
