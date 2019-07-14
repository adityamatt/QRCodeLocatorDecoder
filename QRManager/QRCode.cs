using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emgu.CV;
using Emgu.CV.Structure;

namespace QRManager
{
    public class QRCodeLocator
    {
        Image<Gray, byte> InputImage;
        public QRCodeLocator(string location)
        {
            this.InputImage = new Image<Gray, byte>(location);
        }
        public void getQR()
        {
            Image<Gray,byte> output_ = this.InputImage.ThresholdBinary(new Gray(25), new Gray(255));
        }
    }
}
