using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emgu.CV;
using Emgu.CV.Structure;
using OnBarcode.Barcode.BarcodeScanner;
using Emgu.CV.OCR;
using Emgu.CV.Util;
using Emgu.CV.CvEnum;
using IronOcr;
using Emgu.CV.UI;

namespace QRManager
{
    //public class QRCodeLocator
    //{
    //    public static void CropQR(string input)
    //    {
    //        Image<Gray, byte> image = OtsuBinarize(new Image<Gray, byte>(input));
    //        //------
    //        Image<Gray, byte> output = image.Clone().Not();

    //        output.Save("Test1.jpg");
            
    //        output = Dilate(output);
    //        output.Save("Test2.jpg");
    //        output = SuppressOCR(output,image.Convert<Bgr,byte>());
    //        output.Save("Test3.jpg");
    //        //------
    //        output.Save("Test.jpg");
    //    }
    //    internal static Image<Gray,byte> OtsuBinarize(Image<Gray,byte> input)
    //    {
    //        CvInvoke.Threshold(input, input, 100, 255, Emgu.CV.CvEnum.ThresholdType.Otsu);
    //        return input;
    //    }
    //    internal static Image<Gray, byte> CannyEdge(Image<Gray, byte> input)
    //    {
    //        return input.Canny(25, 10);
    //    }
    //    internal static Image<Gray,byte> SuppressOCR(Image<Gray,byte> input,Image<Bgr,byte> original)
    //    {
    //        double minAreaSize = 400; //v1 is 21x21 that is minimum
    //        Image<Bgr, byte> outputsave = input.Convert<Bgr, byte>();
    //        VectorOfVectorOfPoint contours = FindContour(input);
    //        int n = contours.Size;
    //        for (int i = 0; i < n; i++)
    //        {

    //            VectorOfPoint ctr = contours[i];
    //            Rectangle rect = CvInvoke.BoundingRectangle(ctr);
    //            //Expand rectangle
    //            if (rect.X > 5) rect.X = rect.X - 5;
    //            else rect.X = 0;
    //            if (rect.Y > 5) rect.Y = rect.Y - 5;
    //            else rect.Y = 0;
    //            if (rect.X + rect.Width + 5 < input.Width) rect.Width = rect.Width + 5;
    //            else rect.Width = input.Width - rect.X;
    //            if (rect.Y + rect.Height + 5 < input.Height) rect.Height = rect.Height + 5;
    //            else rect.Height = input.Height - rect.Y;

    //            double area = CvInvoke.ContourArea(ctr);
    //            if (area < minAreaSize)
    //            {
    //                outputsave = SuppressRectangle(outputsave, rect);
    //                continue;
    //            }

    //            Bitmap region = CropBitmap(original.Bitmap, rect);

    //            region.Save("Tmp.jpg");
    //            Console.WriteLine("Next");
    //            Console.ReadKey();
    //            string[] regionString = QRCodeDecoder.DecodeQR(region);
    //            CvInvoke.Rectangle(original, rect, new MCvScalar(0, 0, 255), 3);
    //            CvInvoke.Rectangle(outputsave, rect, new MCvScalar(0, 0, 255), 3);
    //            //ImageViewer viewer = new ImageViewer();
    //            //viewer.Image = original;
    //            //viewer.ShowDialog();
    //            if (regionString!=null && regionString.Length>0)
    //            {
    //                foreach(string d in regionString)  Console.WriteLine(d);
    //            }
    //            else
    //            {
    //                outputsave = SuppressRectangle(outputsave, rect);
    //            }
    //            outputsave.Save("Suppresed.jpg");
    //        }
            
    //        return input;
    //    }
    //    internal static string getOCR(Bitmap input)
    //    {
    //        AutoOcr Ocr = new AutoOcr();
    //        return Ocr.Read(input).Text;
    //    }
    //    internal static Bitmap CropBitmap(Bitmap input,Rectangle cropRect)
    //    {
    //        Bitmap target = new Bitmap(cropRect.Width, cropRect.Height);
    //        using (Graphics g = Graphics.FromImage(target))
    //        {
    //            g.DrawImage(input, new Rectangle(0, 0, target.Width, target.Height),cropRect,GraphicsUnit.Pixel);
    //        }
    //        return target;
    //    }
    //    internal static Image<Bgr,byte> SuppressRectangle(Image<Bgr,byte> input,Rectangle rect)
    //    {
    //        for (int i =rect.X;i<rect.Width+rect.X ;i++)
    //        {
    //            for (int j = rect.Y; j<rect.Height+rect.Y;j++)
    //            {
    //                input.Data[j, i, 0] = 0;
    //                input.Data[j, i, 1] = 0;
    //                input.Data[j, i, 2] = 0;
    //            }
    //        }
    //        return input;
    //    }
    //    internal static Image<Gray, byte> Dilate(Image<Gray, byte> input)
    //    {
    //        Mat kernel1 = CvInvoke.GetStructuringElement(Emgu.CV.CvEnum.ElementShape.Rectangle, new Size(7, 7), new Point(1, 1));
    //        return input.MorphologyEx(Emgu.CV.CvEnum.MorphOp.Dilate, kernel1, new Point(0, 0), 2, Emgu.CV.CvEnum.BorderType.Default, new MCvScalar());
    //    }
    //    internal byte average(Image<Gray,byte> input, Rectangle rect)
    //    {
    //        int countPixels = 0;
    //        int total = 0;
    //        for (int i = rect.X; i < rect.Width + rect.X; i++)
    //        {
    //            for (int j = rect.Y; j < rect.Height + rect.Y; j++)
    //            {
    //                total = total + (int)input.Data[j, i, 0];
    //                countPixels++;
    //            }
    //        }
    //        total = total / countPixels;
    //        return (byte)total;
    //    }
    //    internal static VectorOfVectorOfPoint FindContour(Image<Gray,byte> img, Mat hierarchy = null, Emgu.CV.CvEnum.RetrType type = Emgu.CV.CvEnum.RetrType.List, Emgu.CV.CvEnum.ChainApproxMethod method = Emgu.CV.CvEnum.ChainApproxMethod.ChainApproxSimple)
    //    {
    //        VectorOfVectorOfPoint contours = new VectorOfVectorOfPoint();
    //        CvInvoke.FindContours(img, contours, hierarchy, type, method);
    //        return contours;
    //    }
    //}

    public class QRCodeDecoder
    {
        public static string[] DecodeQR(string input)
        {
            string[] datas = BarcodeScanner.Scan(input, BarcodeType.QRCode);
            return datas;
        }
        public static string[] DecodeQR(Bitmap input)
        {
            string[] datas = BarcodeScanner.Scan(input, BarcodeType.QRCode);
            return datas;
        }
    }
}
