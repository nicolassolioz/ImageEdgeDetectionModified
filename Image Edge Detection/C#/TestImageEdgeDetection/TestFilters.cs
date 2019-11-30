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

            Bitmap bmp = new Bitmap(@"C:\Users\Bornatch\Pictures\0000000.jpg");
            Bitmap img = bmp;
            Bitmap result;

            result = Filters.RainbowFilter(img);
            Assert.AreEqual(55803581, result.GetHashCode());

        }
    }
}



