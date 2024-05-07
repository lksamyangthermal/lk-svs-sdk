using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using KIRSharp;
using WpfSample.Model;
using WpfSample.Model.Handler;

namespace WpfSample.ViewModel
{
    public partial class PreviewViewModel : ObservableObject
    {
        [ObservableProperty]
        private DeviceHandler _deviceHandler = DeviceHandler.Instance;

        [ObservableProperty]
        private List<PseudoColor.Type> _pseudoColors = Enum.GetValues(typeof(PseudoColor.Type)).Cast<PseudoColor.Type>().ToList();

        public PreviewViewModel() { }

        [RelayCommand]
        private async Task Disconnect()
        {
            await DeviceHandler.SelectedDevice.DisposeAsync();
        }

        [RelayCommand]
        private async Task ActiveOnceShutter()
        {
            try
            {
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
