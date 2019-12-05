/*
 * The Following Code was developed by Dewald Esterhuizen
 * View Documentation at: http://softwarebydefault.com
 * Licensed under Ms-PL 
*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Imaging;

namespace ImageEdgeDetection
{
    public partial class MainForm : Form
    {
        private Bitmap originalBitmap = null;
        private Bitmap previewBitmap = null;
        private Bitmap resultBitmap = null;
      
        
        public MainForm()
        {
            InitializeComponent();

            cmbEdgeDetection.SelectedIndex = 0;

            ControlCmbEdgeDetection();

        }

        private void btnOpenOriginal_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            //add jpeg
            ofd.Title = "Select an image file.";
            ofd.Filter = "Png Images(*.png)|*.png|" +
                            "Jpeg Images(*.jpg)|*.jpg|" +
                            "Jpeg Images(*.jpeg)|*.jpeg|" +
                            "Bitmap Images(*.bmp)|*.bmp";


            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                StreamReader streamReader = new StreamReader(ofd.FileName);
                originalBitmap = (Bitmap)Bitmap.FromStream(streamReader.BaseStream);
                streamReader.Close();

                previewBitmap = originalBitmap.CopyToSquareCanvas(picPreview.Width);
                picPreview.Image = previewBitmap;

                ApplyFilter(true);
            }

            //disable checkbox when no picture load
            if (previewBitmap != null)
                enableCheckboxes();
        }


        private void btnSaveNewImage_Click(object sender, EventArgs e)
        {
            ApplyFilter(false);

            if (resultBitmap != null)
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Title = "Specify a file name and file path";
                sfd.Filter = "Png Images(*.png)|*.png|" +
                            "Jpeg Images(*.jpg)|*.jpg|" +
                            "Jpeg Images(*.jpeg)|*.jpeg|" +
                            "Bitmap Images(*.bmp)|*.bmp";
                if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    string fileExtension = Path.GetExtension(sfd.FileName).ToUpper();
                    ImageFormat imgFormat = ImageFormat.Png;

                    if (fileExtension == "BMP")
                    {
                        imgFormat = ImageFormat.Bmp;
                    }
                    else if (fileExtension == "JPG")
                    {
                        imgFormat = ImageFormat.Jpeg;
                    }

                    StreamWriter streamWriter = new StreamWriter(sfd.FileName, false);
                    resultBitmap.Save(streamWriter.BaseStream, imgFormat);
                    streamWriter.Flush();
                    streamWriter.Close();

                    resultBitmap = null;
                }
            }
        }

        private void ApplyFilter(bool preview)
        {
            if (previewBitmap == null || cmbEdgeDetection.SelectedIndex == -1)
            {
                return;
            }

            Bitmap selectedSource = null;
            Bitmap bitmapResult = null;

            if (preview == true)
            {
                selectedSource = previewBitmap;
            }
            else
            {
                selectedSource = originalBitmap;
            }

            if (selectedSource != null)
            {
                if (cmbEdgeDetection.SelectedItem.ToString() == "None")
                {
                    bitmapResult = selectedSource;
                }
                else if (cmbEdgeDetection.SelectedItem.ToString() == "Laplacian 3x3")
                {
                    bitmapResult = selectedSource.Laplacian3x3Filter(false);
                }
                else if (cmbEdgeDetection.SelectedItem.ToString() == "Laplacian 3x3 Grayscale")
                {
                    bitmapResult = selectedSource.Laplacian3x3Filter(true);
                }
                else if (cmbEdgeDetection.SelectedItem.ToString() == "Laplacian 5x5")
                {
                    bitmapResult = selectedSource.Laplacian5x5Filter(false);
                }
                else if (cmbEdgeDetection.SelectedItem.ToString() == "Laplacian 5x5 Grayscale")
                {
                    bitmapResult = selectedSource.Laplacian5x5Filter(true);
                }
                else if (cmbEdgeDetection.SelectedItem.ToString() == "Laplacian of Gaussian")
                {
                    bitmapResult = selectedSource.LaplacianOfGaussianFilter();
                }
                else if (cmbEdgeDetection.SelectedItem.ToString() == "Laplacian 3x3 of Gaussian 3x3")
                {
                    bitmapResult = selectedSource.Laplacian3x3OfGaussian3x3Filter();
                }
                else if (cmbEdgeDetection.SelectedItem.ToString() == "Laplacian 3x3 of Gaussian 5x5 - 1")
                {
                    bitmapResult = selectedSource.Laplacian3x3OfGaussian5x5Filter1();
                }
                else if (cmbEdgeDetection.SelectedItem.ToString() == "Laplacian 3x3 of Gaussian 5x5 - 2")
                {
                    bitmapResult = selectedSource.Laplacian3x3OfGaussian5x5Filter2();
                }
                else if (cmbEdgeDetection.SelectedItem.ToString() == "Laplacian 5x5 of Gaussian 3x3")
                {
                    bitmapResult = selectedSource.Laplacian5x5OfGaussian3x3Filter();
                }
                else if (cmbEdgeDetection.SelectedItem.ToString() == "Laplacian 5x5 of Gaussian 5x5 - 1")
                {
                    bitmapResult = selectedSource.Laplacian5x5OfGaussian5x5Filter1();
                }
                else if (cmbEdgeDetection.SelectedItem.ToString() == "Laplacian 5x5 of Gaussian 5x5 - 2")
                {
                    bitmapResult = selectedSource.Laplacian5x5OfGaussian5x5Filter2();
                }
                else if (cmbEdgeDetection.SelectedItem.ToString() == "Sobel 3x3")
                {
                    bitmapResult = selectedSource.Sobel3x3Filter(false);
                }
                else if (cmbEdgeDetection.SelectedItem.ToString() == "Sobel 3x3 Grayscale")
                {
                    bitmapResult = selectedSource.Sobel3x3Filter();
                }
                else if (cmbEdgeDetection.SelectedItem.ToString() == "Prewitt")
                {
                    bitmapResult = selectedSource.PrewittFilter(false);
                }
                else if (cmbEdgeDetection.SelectedItem.ToString() == "Prewitt Grayscale")
                {
                    bitmapResult = selectedSource.PrewittFilter();
                }
                else if (cmbEdgeDetection.SelectedItem.ToString() == "Kirsch")
                {
                    bitmapResult = selectedSource.KirschFilter(false);
                }
                else if (cmbEdgeDetection.SelectedItem.ToString() == "Kirsch Grayscale")
                {
                    bitmapResult = selectedSource.KirschFilter();
                }
            }

            if (bitmapResult != null)
            {
                if (preview == true)
                {
                    picPreview.Image = bitmapResult;
                }
                else
                {
                    resultBitmap = bitmapResult;
                }
            }

            //Control if first filter is selected or not
            ControlCmbEdgeDetection();
        }

        private void NeighbourCountValueChangedEventHandler(object sender, EventArgs e)
        {
            ApplyFilter(true);
        }

        private void checkBoxBlackAndWhiteFilter_CheckedChanged(object sender, EventArgs e)
        {

            applyFirstFilter();

        }

        private void checkBoxSwapFilter_CheckedChanged(object sender, EventArgs e)
        {

            applyFirstFilter();
            
        }

        private void checkBoxNoneFilter_CheckedChanged(object sender, EventArgs e)
        {

            applyFirstFilter();
        }

        //manage checkbox
        private void enableCheckboxes()
        {
            checkBoxNoneFilter.Enabled = true;
            checkBoxRainbowFilter.Enabled = true;
            checkBoxSwapFilter.Enabled = true;
        }

        private void ControlCmbEdgeDetection()
        {
            if(checkBoxNoneFilter.CheckState.ToString().Equals("Unchecked") && checkBoxRainbowFilter.CheckState.ToString().Equals("Unchecked") && checkBoxSwapFilter.CheckState.ToString().Equals("Unchecked"))
            {
                cmbEdgeDetection.Enabled = false;
            }
            else
            {
                cmbEdgeDetection.Enabled = true;
            }            
        }

        private void applyFirstFilter()
        {
            Bitmap toTreat = originalBitmap;

            //If Check box "none" is checked then unchecked et disabled others checkbox
            if(checkBoxNoneFilter.CheckState.ToString().Equals("Checked"))
            {
                checkBoxRainbowFilter.Checked = false ;
                checkBoxSwapFilter.Checked = false ;

                checkBoxRainbowFilter.Enabled = false;
                checkBoxSwapFilter.Enabled = false;
                
            }
            else
            {
                checkBoxRainbowFilter.Enabled = true;
                checkBoxSwapFilter.Enabled = true;

                if (checkBoxRainbowFilter.CheckState.ToString().Equals("Checked"))
                {
                    toTreat = Filters.RainbowFilter(toTreat);
                }

                if (checkBoxSwapFilter.CheckState.ToString().Equals("Checked"))
                {
                    toTreat = Filters.Swap(toTreat);
                }
            }    

            previewBitmap = toTreat;
            
            ApplyFilter(true);
        }

        private void picPreview_Click(object sender, EventArgs e)
        {

        }
    }
}
