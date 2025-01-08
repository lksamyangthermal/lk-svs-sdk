using CommunityToolkit.Mvvm.ComponentModel;
using KIRSharp;
using System.Collections.ObjectModel;
using System.Diagnostics;
using static KIRSharp.Info;

namespace WpfSample.Model.Handler
{
    public partial class DeviceHandler : ObservableObject
    {
        private static DeviceHandler _deviceHandler = new ();
        public static DeviceHandler Instance { get => _deviceHandler; }

        [ObservableProperty]
        private ObservableCollection<Device> _devices = new();

        [ObservableProperty]
        private List<int> _fpsList = new List<int>() { 1, 2, 4, 8 };
        [ObservableProperty]
        private int _selectedFpsThermal = 8;
        [ObservableProperty]
        private int _selectedFpsCmos = 8;

        [ObservableProperty]
        private Device? _selectedDevice = null;

        private DeviceHandler()
        {

        }

        private ICamera? CreateCamera(Info info, string rtspId, string rtspPassword)
        {
            switch (info.Sens)
            {
                case SensorType.IR_SENSOR_80:
                case SensorType.IR_SENSOR_80_SHUTTER:
                case SensorType.IR_SENSOR_160:
                    return new UdpCamera(info);

                case SensorType.IR_SENSOR_256_IRAY:
                case SensorType.IR_SENSOR_256_HIK:
                case SensorType.IR_SENSOR_384:
                case SensorType.IR_SENSOR_384_IRAY:
                case SensorType.IR_SENSOR_384_I3:
                    {
                        try
                        {
                            var camera = new UdpCamera(info, rtspId, rtspPassword);
                            return camera;
                        }
                        catch (UnauthorizedAccessException ex)
                        {
                            Debug.WriteLine("CreateCamera", $"Cannot add device, {ex.Message}");
                            return null;
                        }
                    }

                case SensorType.HIK_96:
                case SensorType.HIK_160:
                    {
                        try
                        {
                            var camera = new HikCamera(info, rtspId, rtspPassword);
                            return camera;
                        }
                        catch (UnauthorizedAccessException ex)
                        {
                            Debug.WriteLine("CreateCamera", $"Cannot add device {ex.Message}");
                            return null;
                        }
                    }
                case SensorType.Unknown:
                default:
                    return null;
            }
        }

        public void ConnectDevice(Info info, string? id = null, string? password = null)
        {
            SelectedDevice = new Device(CreateCamera(info, id, password));
        }
    }
}
