﻿using CommunityToolkit.Mvvm.ComponentModel;
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
        private static DeviceHandler _deviceHandler = new DeviceHandler();
        public static DeviceHandler Instance { get => _deviceHandler; }

        [ObservableProperty]
        private ObservableCollection<Device> _devices = new();

        [ObservableProperty]
        private List<int> _fpsList = new List<int>() { 1, 2, 4, 8 };
        [ObservableProperty]
        private int _selectedFpsThermal = 2;
        [ObservableProperty]
        private int _selectedFpsCmos = 2;

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
                case CameraName.Dummy:
                default:
                    return new DummyCamera(info);
            }
        }

        public void AddDevice(Info info)
        {
            Device newDevice = new Device(CreateCamera(info));

            string[] ipParts = info.Ip.Split('.');
            if (string.IsNullOrEmpty(info.SiteName))
            {
                newDevice.Camera.Info.SiteName = ipParts[2];
            }
            if (string.IsNullOrEmpty(info.CustomName))
            {
                newDevice.Camera.Info.CustomName = ipParts[3];
            }
            Devices.Add(newDevice);
            SelectedDevice = newDevice;
        }
    }
}
