using KIRSharp;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using KIRSharp.Utils;

namespace WpfSample.Model
{
    public class ImageProcess
    {
        public static Mat? ThermalFrameToRgbMat(byte[] bytes, int width, int height)
        {
            try
            {
                var shorts = Util.BytesToShorts(bytes);
                Mat matRaw = BufferToMat(shorts, width, height);
                Mat matRgb = ConvertToRgb(matRaw);
                return matRgb;
            }
            catch (Exception ex)
            {
                Console.WriteLine("ThermalFrameToRgbMat: " + ex.Message);
                return null;
            }
        }

        public static Mat? BufferToMat(short[] buffer, int width, int height)
        {
            if (buffer.Length != width * height || buffer == null) return null;
            Mat mat = new Mat(height, width, MatType.CV_16UC1);
            var ptr = mat.Data;
            Marshal.Copy(buffer, 0, ptr, width * height);

            return mat;
        }

        public static Mat? ConvertToRgb(Mat mat)
        {
            if (mat == null) return null;
            Mat matNorm = new Mat();
            Cv2.Normalize(mat, matNorm, 0, 255, NormTypes.MinMax, MatType.CV_8U);

            return matNorm;
        }

        public static void GetTemperature(Mat? mat, out double minVal, out double maxVal, out double avrVal, ICamera.CameraType cameraType)
        {
            minVal = 0.0;
            maxVal = 0.0;
            avrVal = 0.0;
            if (mat != null)
            {

                OpenCvSharp.Point minLoc = new();
                OpenCvSharp.Point maxLoc = new();

                double minValRaw;
                double maxValRaw;
                mat.MinMaxLoc(out minValRaw, out maxValRaw, out minLoc, out maxLoc);

                // 마스크를 사용하여 NaN이 아닌 값에 대한 평균값 계산
                Scalar meanVal = Cv2.Mean(mat);
                avrVal = meanVal.Val0;

                // 센서 유형에 따라 온도 변환 및 반올림
                minVal = Math.Round(RawToCelsius(minValRaw, cameraType), 2);
                maxVal = Math.Round(RawToCelsius(maxValRaw, cameraType), 2);
                avrVal = Math.Round(RawToCelsius(avrVal, cameraType), 2);
            }
        }

        public static double RawToCelsius(double raw, ICamera.CameraType cameraType)
        {
            double kelvin = RawToKelvin(raw, cameraType);
            if (cameraType != ICamera.CameraType.KIR384_Kelvin)
                return KelvinToCelsius(kelvin);
            else
                return kelvin;
        }

        public static double RawToKelvin(double raw, ICamera.CameraType cameraType)
        {
            const double constR = 395653;
            const double constB = 1428;
            const double constF = 1;
            const double constO = 156;

            double wt1 = raw / 0.95;
            const double wt2 = 0;
            const double wt3 = 188.1204622;
            double wtScene = wt1 - wt2 - wt3;

            switch (cameraType)
            {
                case ICamera.CameraType.KIR160_Raw: // 2.0, 3.0
                    return constB / Math.Log((constR / (wtScene - constO)) + constF);
                case ICamera.CameraType.KIR160_Kelvin: // 2.5, 3.5
                    return raw * 0.01;
                case ICamera.CameraType.KIR384_Kelvin:
                    return raw * 0.1;
            }

            return -273.15;
        }

        public static double KelvinToCelsius(double kelvin)
        {
            return kelvin - 273.15;
        }

        public static double CelsiusToKelvin(double celsius)
        {
            return celsius + 273.15;
        }

        public static class ResolutionConverter
        {
            public static double EssenceToRatio(double val, double maxVal)
            {
                return val / maxVal;
            }

            public static double RatioToEssence(double val, double maxVal)
            {
                return maxVal * val;
            }
        }

        public static Mat ConcatenateImages(Mat img1, Mat img2)
        {
            // 두 이미지의 높이는 동일하다고 가정
            int height = img1.Height;
            int width = img1.Width + img2.Width;

            // 새 이미지 생성
            Mat result = new Mat(new OpenCvSharp.Size(width, height), img1.Type());

            // 첫 번째 이미지를 새 이미지의 왼쪽에 복사
            Mat roi1 = new Mat(result, new OpenCvSharp.Rect(0, 0, img1.Width, img1.Height));
            img1.CopyTo(roi1);

            // 두 번째 이미지를 새 이미지의 오른쪽에 복사
            Mat roi2 = new Mat(result, new OpenCvSharp.Rect(img1.Width, 0, img2.Width, img2.Height));
            img2.CopyTo(roi2);

            return result;
        }

        public static Mat YV12ToRgb(byte[] yv12Buffer, int width, int height)
        {
            // YV12 버퍼에서 Y, U, V 평면을 분리
            int ySize = width * height;
            int uvSize = ySize / 4;

            IntPtr yPlane = Marshal.AllocHGlobal(ySize);
            IntPtr uPlane = Marshal.AllocHGlobal(uvSize);
            IntPtr vPlane = Marshal.AllocHGlobal(uvSize);

            Marshal.Copy(yv12Buffer, 0, yPlane, ySize);
            Marshal.Copy(yv12Buffer, ySize, vPlane, uvSize); // V 평면이 먼저 있음
            Marshal.Copy(yv12Buffer, ySize + uvSize, uPlane, uvSize); // 그 다음에 U 평면이 있음

            Mat yMat = Mat.FromPixelData(height, width, MatType.CV_8UC1, yPlane);
            Mat uMat = Mat.FromPixelData(height / 2, width / 2, MatType.CV_8UC1, uPlane);
            Mat vMat = Mat.FromPixelData(height / 2, width / 2, MatType.CV_8UC1, vPlane);

            // U, V 평면을 업샘플링
            Mat uMatResized = new Mat();
            Mat vMatResized = new Mat();
            Cv2.Resize(uMat, uMatResized, new OpenCvSharp.Size(width, height));
            Cv2.Resize(vMat, vMatResized, new OpenCvSharp.Size(width, height));

            // YUV 평면을 병합
            Mat[] yuvChannels = { yMat, uMatResized, vMatResized };
            Mat yuvMat = new Mat();
            Cv2.Merge(yuvChannels, yuvMat);

            // YUV를 RGB로 변환
            Mat rgbMat = new Mat();
            Cv2.CvtColor(yuvMat, rgbMat, ColorConversionCodes.YUV2BGR);

            // 메모리 해제
            Marshal.FreeHGlobal(yPlane);
            Marshal.FreeHGlobal(uPlane);
            Marshal.FreeHGlobal(vPlane);

            return rgbMat;
        }
    }
}
