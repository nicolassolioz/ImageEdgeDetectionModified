using System;
using ImageEdgeDetection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace TestImageEdgeDetection
{
    [TestClass]
    public class TestFilters
    {
        

        [TestMethod]
        public void TestSwap()
        {

            Bitmap bmp = new Bitmap(@"C:\Users\Bornatch\Pictures\0000000.jpg");
            Bitmap img = bmp;
            Bitmap result;

            result = Filters.Swap(img);
            Assert.AreEqual(65543281, result.GetHashCode());

        }

        [TestMethod]
        public void TestRainbowFilter()
        {
            Bitmap control = new Bitmap("../../res/treeRainbow.png");

            //Bitmap bmp = new Bitmap(@"C:\Users\Bornatch\Pictures\index.jpg");
            Bitmap bmp = new Bitmap(@"C:\Users\Bornatch\Pictures\0000000.jpg");
            Bitmap img = bmp;
            Bitmap result;

            result = Filters.RainbowFilter(img);

            //code from http://csharpexamples.com/c-fast-bitmap-compare/

            int bytes = result.Width * result.Height * (Image.GetPixelFormatSize(result.PixelFormat) / 8);

            byte[] b1bytes = new byte[bytes];
            byte[] b2bytes = new byte[bytes];

            BitmapData bitmapData1 = result.LockBits(new Rectangle(0, 0, result.Width, result.Height), ImageLockMode.ReadOnly, result.PixelFormat);
            BitmapData bitmapData2 = control.LockBits(new Rectangle(0, 0, control.Width, control.Height), ImageLockMode.ReadOnly, control.PixelFormat);

            Marshal.Copy(bitmapData1.Scan0, b1bytes, 0, bytes);
            Marshal.Copy(bitmapData2.Scan0, b2bytes, 0, bytes);


            //int de validation, modification bv
            int go = 0;
            int nogo = 0;
            for (int n = 0; n <= bytes - 1; n++)
            {
                if (b1bytes[n] != b2bytes[n])
                {
                    nogo--;
                }
                go++;
                nogo++;
            }

            Assert.AreEqual(go, nogo);

            result.UnlockBits(bitmapData1);
            control.UnlockBits(bitmapData2);

        }   
    }
}



