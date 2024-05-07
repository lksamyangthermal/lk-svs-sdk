using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfSample.Model
{
    public class PseudoColor
    {
        public enum Type
        {
            Gray,
            Rainbow,
            Inferno,
            Virdis,
        }

        public static Mat? ApplyColorMap(Mat? mat, Type type)
        {
            ColormapTypes colormap = ColormapTypes.Jet;
            if (mat != null)
            {
                switch (type)
                {
                    case Type.Gray:
                        return mat;
                    case Type.Rainbow:
                        colormap = ColormapTypes.Jet;
                        break;
                    case Type.Inferno:
                        colormap = ColormapTypes.Inferno;
                        break;
                    case Type.Virdis:
                        colormap = ColormapTypes.Viridis;
                        break;
                }
                Mat result = new Mat();
                Cv2.ApplyColorMap(mat, result, colormap);
                return result;
            }
            else return null;
        }
    }
}
