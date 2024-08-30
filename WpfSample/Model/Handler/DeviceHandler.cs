using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using KIRSharp.Camera;
using KIRSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static KIRSharp.Broadcast;
using System.Collections.ObjectModel;
using static KIRSharp.Broadcast.Info;

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

        private ICamera CreateCamera(Info info)
        {
            switch (info.Sens)
            {
                case SensorType.IR_SENSOR_80:
                case SensorType.IR_SENSOR_80_SHUTTER:
                case SensorType.IR_SENSOR_160:
                case SensorType.IR_SENSOR_256_IRAY:
                case SensorType.IR_SENSOR_256_HIK:
                case SensorType.IR_SENSOR_384:
                case SensorType.IR_SENSOR_384_IRAY:
                case SensorType.IR_SENSOR_384_I3:
                    return new UdpCamera(info, SelectedFpsThermal, SelectedFpsCmos);
                case SensorType.HIK_160:
                    return new HikCamera(info);
                case SensorType.Unknown:
                default:
                    return null;
            }
        }

        public void ConnectDevice(Info info)
        {
            SelectedDevice = new Device(CreateCamera(info));
        }
    }
}
