using CommunityToolkit.Mvvm.ComponentModel;
using KIRSharp.Camera;
using KIRSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
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

        public Device() { }

        public Device(ICamera camera)
        {
            Camera = camera;
            Initialize();
        }
        private async void ThermalCamera_FrameEnqueued(object sender, FrameEventArgs e)
        {
            if (e.FrameType == FrameType.Thermal)
            {
                await ProcessThermalImage(e.Frame.Bytes);
                _ = Task.Run(() => ThermalFps = CalculateFps(_stopwatchThermal));
            }
        }

        private async void CmosCamera_FrameEnqueued(object sender, FrameEventArgs e)
        {
            if (e.FrameType == FrameType.Cmos)
            {
                await ProcessCmosFrame(e.Frame.Bytes);
                _ = Task.Run(() => CmosFps = CalculateFps(_stopwatchCmos));
            }
        }

        private async void UvfCount_FrameEnqueued(object sender, FrameEventArgs e)
        {
            if (e.FrameType == FrameType.Uvf)
            {
                UvCount = Camera.UvfCount;
            }
        }

        public void Initialize()
        {
            Camera.ThermalFrameEnqueued += ThermalCamera_FrameEnqueued;
            Camera.CmosFrameEnqueued += CmosCamera_FrameEnqueued;
            Camera.UvfCountEnqueued += UvfCount_FrameEnqueued;
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

        private async Task ProcessThermalImage(byte[] bytes)
        {
            try
            {
                double tempMin, tempMax, tempAvr;

                var shorts = Util.BytesToShorts(bytes);
                MatRaw = ImageProcess.BufferToMat(shorts, Camera.FrameBufferThermal.FrameWidth, Camera.FrameBufferThermal.FrameHeight);

                ImageProcess.GetTemperature(MatRaw, out tempMin, out tempMax, out tempAvr, Camera.Type);
                TempMin = tempMin;
                TempMax = tempMax;
                TempAvr = tempAvr;

                var pseudoColorTask = ApplyPseudoColorAsync(MatRaw);
                ThermalPreview = pseudoColorTask.Result;

            }
            catch (Exception ex)
            {
                Debug.WriteLine($"ProcessThermalImage: {ex.Message}");
            }
        }

        private Task<Mat> ApplyPseudoColorAsync(Mat matRaw)
        {
            Mat matNorm = ImageProcess.ConvertToRgb(matRaw);
            var matPseudoColor = PseudoColor.ApplyColorMap(matNorm, PseudoColorType);
            Mat resizedThermal = new Mat();

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

                if (Camera.FrameBufferCmos.FrameWidth > 0 && Camera.FrameBufferCmos.FrameHeight > 0)
                {
                    Cv2.Resize(rechannelThermal, resizedThermal, new Size(Camera.FrameBufferCmos.FrameWidth, Camera.FrameBufferCmos.FrameHeight));
                }
                else
                {
                    resizedThermal = rechannelThermal.Clone();
                }

            }
            catch (Exception ex)
            {
                string message = $"ApplyPseudoColorAsync: {ex.Message}";
                Debug.WriteLine(message);
            }

            return Task.FromResult(resizedThermal);
        }

        private async Task ProcessCmosFrame(byte[] bytes)
        {
            await Task.Run(() =>
            {
                try
                {
                    if (bytes != null && bytes.Length != 0)
                    {
                        var matCmos = Cv2.ImDecode(bytes, ImreadModes.Color);
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

