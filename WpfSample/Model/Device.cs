using CommunityToolkit.Mvvm.ComponentModel;
using KIRSharp.Camera;
using KIRSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KIRSharp.Utils;
using OpenCvSharp;

namespace WpfSample.Model
{
    public partial class Device : ObservableObject, IAsyncDisposable
    {
        public ICamera Camera { get; set; }
        
        [ObservableProperty]
        private Mat? _matRaw;
        [ObservableProperty]
        private Mat? _thermalPreview;
        [ObservableProperty]
        private Mat? _cmosPreview;

        [ObservableProperty]
        private short _uvCount;
        [ObservableProperty]
        private double _thermalFps = 0.0;
        [ObservableProperty]
        private double _cmosFps = 0.0;

        [ObservableProperty]
        double _tempMin, _tempMax, _tempAvr;

        Stopwatch _stopwatchThermal = Stopwatch.StartNew();
        Stopwatch _stopwatchCmos = Stopwatch.StartNew();


        [ObservableProperty]
        private PseudoColor.Type _pseudoColorType = PseudoColor.Type.Rainbow;

        [ObservableProperty] private double _userOffset = 0;

        public Device() { }

        public Device(ICamera camera)
        {
            Camera = camera;

            _ = GetUserOffset();

            Camera.ThermalFrameEnqueued += ThermalCamera_FrameEnqueued;
            Camera.CmosFrameEnqueued += CmosCamera_FrameEnqueued;
            Camera.UvfCountEnqueued += UvfCount_FrameEnqueued;
        }

        private async Task GetUserOffset()
        {
            var camera = Camera as UdpCamera;
            if (camera == null) return;

            var offset = await camera.GetOffset();
            if (offset == null) return;

            UserOffset = (double)offset;
        }

        private async void ThermalCamera_FrameEnqueued(object sender, FrameEventArgs e)
        {
            await ProcessThermalImage(e.Frame);
            _ = Task.Run(() => ThermalFps = CalculateFps(_stopwatchThermal));
        }

        private async void CmosCamera_FrameEnqueued(object sender, FrameEventArgs e)
        {
            if (e.Frame.Type == IFrame.FrameType.Cmos)
            {
                await ProcessCmosFrame(e.Frame);
                _ = Task.Run(() => CmosFps = CalculateFps(_stopwatchCmos));
            }
        }

        private async void UvfCount_FrameEnqueued(object sender, FrameEventArgs e)
        {
            if (e.Frame.Type == IFrame.FrameType.Thermal)
            {
                if (e.Frame is ThermalFrame thermalFrame)
                {
                    UvCount = thermalFrame.UvfCount;
                }
            }
        }

        public async ValueTask DisposeAsync()
        {
            if (Camera != null)
            {
                try
                {
                    await Camera.StopStreamingAsync();
                    Camera.Dispose();
                    Debug.WriteLine($"Camera {Camera.Info.Ip} is closed.");

                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"DisposeAsync : {ex.Message}");
                }
            }
        }

        private async Task ProcessThermalImage(IFrame frame)
        {
            try
            {
                if (frame.Type == IFrame.FrameType.Thermal)
                {
                    double tempMin, tempMax, tempAvr;

                    var shorts = Util.BytesToShorts(frame.Bytes);
                    MatRaw = ImageProcess.BufferToMat(shorts, frame.Width,frame.Height);

                    ImageProcess.GetTemperature(MatRaw, out tempMin, out tempMax, out tempAvr, Camera.Type);
                    TempMin = Math.Round(tempMin + UserOffset, 2);
                    TempMax = Math.Round(tempMax + UserOffset, 2);
                    TempAvr = Math.Round(tempAvr + UserOffset, 2);

                    var mat = ApplyPseudoColorAsync(MatRaw);
                    ThermalPreview = mat;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"ProcessThermalImage: {ex.Message}");
            }
        }

        private Mat? ApplyPseudoColorAsync(Mat matRaw)
        {
            Mat matNorm = ImageProcess.ConvertToRgb(matRaw);
            var matPseudoColor = PseudoColor.ApplyColorMap(matNorm, PseudoColorType);

            try
            {
                Mat rechannelThermal = new Mat();

                if (matPseudoColor.Channels() == 1)
                {
                    Cv2.CvtColor(matPseudoColor, rechannelThermal, ColorConversionCodes.GRAY2BGR);
                }
                else
                {
                    rechannelThermal = matPseudoColor;
                }

                return rechannelThermal;

            }
            catch (Exception ex)
            {
                string message = $"ApplyPseudoColorAsync: {ex.Message}";
                Debug.WriteLine(message);
                return null;
            }
        }

        private async Task ProcessCmosFrame(IFrame frame)
        {
            await Task.Run(() =>
            {
                try
                {
                    if (frame.Bytes != null && frame.Bytes.Length != 0)
                    {
                        var matCmos = Cv2.ImDecode(frame.Bytes, ImreadModes.Color);
                        if (matCmos != null)
                        {
                            CmosPreview = matCmos;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"ProcessCmosFrame: {ex.Message}");
                }
            });
        }

        private double CalculateFps(Stopwatch stopwatch)
        {
            double fps = 0;
            if (stopwatch.ElapsedMilliseconds > 0)
            {
                fps = 1000 / stopwatch.Elapsed.TotalMilliseconds;
            }
            stopwatch.Restart();

            return fps;
        }
    }
}

