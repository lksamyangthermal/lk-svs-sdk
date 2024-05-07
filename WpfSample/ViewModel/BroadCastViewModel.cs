using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using KIRSharp;
using KIRSharp.Camera;
using static KIRSharp.Broadcast;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Windows;
using WpfSample.Model.Handler;

namespace WpfSample.ViewModel
{
    public partial class BroadCastViewModel : ObservableObject
    {
        [ObservableProperty] 
        private DeviceHandler _deviceHandler = DeviceHandler.Instance;
        [ObservableProperty]
        private Broadcast.Info _selectedRegisteredDeviceInfo;
        [ObservableProperty]
        private Broadcast.Info _selectedDeviceInfo;
        [ObservableProperty]
        private Broadcast.Info _manualDeviceInfo = new();
        [ObservableProperty]
        private ObservableCollection<Broadcast.Info> _broadcastDeviceInfos = new ();
        [ObservableProperty]
        private ObservableCollection<Broadcast.Info> _registeredDeviceInfos = new ();

        public enum PortType
        {
            Normal160,
            Single160,
            Single384,
            Dummy
        }
        [ObservableProperty]
        private List<PortType> _portTypes = Enum.GetValues(typeof(PortType)).Cast<PortType>().ToList();
        [ObservableProperty]
        private PortType? _selectedPort = PortType.Normal160;
        partial void OnSelectedPortChanged(PortType? value)
        {
            if (value == PortType.Normal160)
            {
                ManualDeviceInfo.Opm = 0;
            }
            else
            {
                ManualDeviceInfo.Opm = 1;
            }
        }

        public BroadCastViewModel() 
        {
            _ = FindBroadcastCameraAsync();
        }

        [RelayCommand]
        private async Task AddDeviceManual()
        {
            switch (SelectedPort)
            {
                case PortType.Normal160:
                    ManualDeviceInfo.Name = CameraName.Kir160Dual;
                    break;
                case PortType.Single160:
                    ManualDeviceInfo.Name = CameraName.Kir160Dual;
                    break;
                case PortType.Single384:
                    ManualDeviceInfo.Name = CameraName.Kir384Dual;
                    ManualDeviceInfo.Sens = Info.SensorType.IR_SENSOR_384;
                    break;
                case PortType.Dummy:
                    ManualDeviceInfo.Name = CameraName.Dummy;
                    break;
            }
            await AddDevice(new Info(ManualDeviceInfo));
        }

        [RelayCommand]
        private async Task AddSelectedDevice()
        {
            if(SelectedDeviceInfo != null)
            {
                await AddDevice(SelectedDeviceInfo);
            }
        }

        private async Task AddDevice(Info info)
        {
            try
            {
                if (DeviceHandler.SelectedDevice != null) return;
                if (!IsValidIp(info.Ip)) return;
                if (IsRegisteredInfo(info)) return;

                try
                {
                    RegisteredDeviceInfos.Add(info);
                    DeviceHandler.AddDevice(info);
                    RemoveBroadcastInfo(info);
                    Debug.WriteLine("Save Device Info: " + info.Ip);

                }
                catch (Exception ex)
                {
                    Debug.WriteLine("AddDevice", $"Cannot add device: {ex.Message}");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("AddDevice: " + ex.Message);
            }
        }

        private async Task FindBroadcastCameraAsync()
        {
            try
            {
                await Task.Delay(5000);
                var broadcast = Broadcast.Instance;
                while (broadcast != null)
                {
                    var newInfo = await broadcast.FindCamera();
                    if (newInfo != null)
                    {
                        if (!IsBroadcastInfo(newInfo)) 
                        {
                            if(!IsRegisteredInfo(newInfo))
                            {
                                BroadcastDeviceInfos.Add(newInfo);
                            }
                        }
                        else
                        {
                            if (IsRegisteredInfo(newInfo))
                            {
                                RemoveBroadcastInfo(newInfo);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"FindBroadcastCameraAsync: {ex.Message}");
            }
        }

        private bool IsValidIp(string ip)
        {
            Regex validateIPv4Regex = new Regex("^(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\\.(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\\.(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\\.(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$");
            var isMatch =  validateIPv4Regex.IsMatch(ip);
            if(isMatch)
            {
                return true;
            }
            else
            {
                Debug.WriteLine("IsValidIp: Invalid IP");
                return false;
            }
        }

        private bool IsRegisteredInfo(Info info)
        {
            foreach (var registeredInfo in RegisteredDeviceInfos)
            {
                if (string.Compare(info.Ip, registeredInfo.Ip) == 0)
                {
                    //LogHandler.Instance.ShowDialogAndLog("Device info", "Duplicate IP");
                    return true;
                }
            }
            return false;
        }

        private bool IsBroadcastInfo(Info info)
        {
            foreach (var broadcastInfo in BroadcastDeviceInfos)
            {
                if (string.Compare(info.Ip, broadcastInfo.Ip) == 0)
                {
                    return true;
                }
            }
            return false;
        }

        private void RemoveBroadcastInfo(Info info)
        {
            foreach (var broadcastInfo in BroadcastDeviceInfos)
            {
                if (string.Compare(info.Ip, broadcastInfo.Ip) == 0)
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        BroadcastDeviceInfos.Remove(broadcastInfo);
                    });
                    break;
                }
            }
        }
    }
}
