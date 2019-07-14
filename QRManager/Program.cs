using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QRManager
{
    class Program
    {
        static string qr_path1 = "res/psnU.png";
        static string qr_path2 = "res/psn1.png";
        static void Main(string[] args)
        {
            QRCodeLocator qr = new QRCodeLocator(qr_path2);
            qr.getQR();
        }
    }
}
