using System;
using ImageEdgeDetection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;

namespace TestImageEdgeDetection
{
    [TestClass]
    public class TestExtBitmap
    {

        [TestMethod]
        public void TestLaplacianOfGaussianFilter()
        {
            Bitmap bmp = new Bitmap(@"C:\Users\Bornatch\Pictures\0000000.jpg");
            Bitmap img = bmp;
            Bitmap result;

            result = ExtBitmap.LaplacianOfGaussianFilter(img);
            Assert.AreEqual(61858317, result.GetHashCode());

        }


        [TestMethod]
        public void TestLaplacian3x3OfGaussian3x3Filter()
        {
            Bitmap bmp = new Bitmap(@"C:\Users\Bornatch\Pictures\0000000.jpg");
            Bitmap img = bmp;
            Bitmap result;

            result = ExtBitmap.Laplacian3x3OfGaussian3x3Filter(img);
            Assert.AreEqual(28639838, result.GetHashCode());

        }


        [TestMethod]
        public void TestLaplacian3x3OfGaussian5x5Filter1()
        {
            Bitmap bmp = new Bitmap(@"C:\Users\Bornatch\Pictures\0000000.jpg");
            Bitmap img = bmp;
            Bitmap result;

            result = ExtBitmap.Laplacian3x3OfGaussian5x5Filter1(img);
            Assert.AreEqual(49467480, result.GetHashCode());

        }

    }
}
