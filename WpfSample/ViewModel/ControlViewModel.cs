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
        [ObservableProperty] private DeviceHandler _deviceHandler = DeviceHandler.Instance;
        [ObservableProperty] private List<PseudoColor.Type> _pseudoColors = Enum.GetValues(typeof(PseudoColor.Type)).Cast<PseudoColor.Type>().ToList();
        [ObservableProperty] private double _offset = 0;

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

        [RelayCommand]
        private async Task GetOffset()
        {
            if (DeviceHandler.SelectedDevice == null) return;

            var camera = DeviceHandler.SelectedDevice.Camera as UdpCamera;
            if (camera == null) return;

            var offset = await camera.GetOffset();
            if (offset == null) return;

            Offset = (double)offset;
            DeviceHandler.SelectedDevice.UserOffset = Offset;
        }

        [RelayCommand]
        private async Task SetOffset()
        {
            if (DeviceHandler.SelectedDevice == null) return;

            var camera = DeviceHandler.SelectedDevice.Camera as UdpCamera;
            if (camera == null) return;

            var offset = await camera.SetOffset(Offset);
            if (offset != null)
            {
                DeviceHandler.SelectedDevice.UserOffset = (double)offset;
            };
        }
    }
}
