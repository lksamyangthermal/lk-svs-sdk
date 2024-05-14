using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using KIRSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfSample.Model.Handler;
using WpfSample.Model;

namespace WpfSample.ViewModel
{
    public partial class ControlViewModel : ObservableObject
    {
        [ObservableProperty]
        private DeviceHandler _deviceHandler = DeviceHandler.Instance;

        [ObservableProperty]
        private List<PseudoColor.Type> _pseudoColors = Enum.GetValues(typeof(PseudoColor.Type)).Cast<PseudoColor.Type>().ToList();

        public ControlViewModel() { }

        [RelayCommand]
        private async Task Disconnect()
        {
            if (DeviceHandler.SelectedDevice == null) return;

            await DeviceHandler.SelectedDevice.DisposeAsync();
            DeviceHandler.SelectedDevice = null;
        }

        [RelayCommand]
        private async Task ActiveOnceShutter()
        {
            try
            {
                if (DeviceHandler.SelectedDevice == null) return;

                var udpCamera = (UdpCamera)DeviceHandler.SelectedDevice.Camera;
                await udpCamera.RunShutterManually();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"ActiveOnceShutter: {ex.Message}");
            }
        }
    }
}
