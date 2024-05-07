using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using WpfSample.Model.Handler;

namespace WpfSample.ViewModel
{
    public partial class PreviewViewModel : ObservableObject
    {
        [ObservableProperty]
        private DeviceHandler _deviceHandler = DeviceHandler.Instance;

        public PreviewViewModel() { }
    }
}
