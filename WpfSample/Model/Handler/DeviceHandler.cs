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
            switch (info.Name)
            {
                case CameraName.Kir80Single:
                case CameraName.Kir160Dual:
                case CameraName.Kir384Dual:
                    return new UdpCamera(info, SelectedFpsThermal, SelectedFpsCmos);
                case CameraName.Hik160Dual:
                    return new HikCamera(info);
                case CameraName.Dummy:
                default:
                    return new DummyCamera(info);
            }
        }

        public void ConnectDevice(Info info)
        {
            SelectedDevice = new Device(CreateCamera(info));
        }
    }
}
