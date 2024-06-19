using KIRSharp;
using System.Collections.Specialized;
using System.ComponentModel;
using OpenCvSharp.Extensions;
using WpfSample.Model;
using WpfSample.Model.Handler;
using WpfSample.ViewModel;

namespace WinFormsSample
{
    public partial class MainForm : Form
    {
        private BroadCastViewModel _broadcastViewModel;
        private Broadcast.Info _selectedDeviceInfo;

        public MainForm()
        {
            InitializeComponent();
            Process();
        }

        private void Process()
        {
            _broadcastViewModel = new BroadCastViewModel();
            _broadcastViewModel.BroadcastDeviceInfos.CollectionChanged += BroadcastDeviceInfos_CollectionChanged;
        }

        private void BroadcastDeviceInfos_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (Broadcast.Info newItem in e.NewItems)
                {
                    var info = new string[] { newItem.Version, newItem.Ip };
                    var item = new ListViewItem(info)
                    {
                        Tag = newItem
                    };
                    listView_Broadcast.Items.Add(item);
                }
            }
            else if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach (Broadcast.Info oldItem in e.OldItems)
                {
                    foreach (ListViewItem item in listView_Broadcast.Items)
                    {
                        if (item.Tag == oldItem)
                        {
                            listView_Broadcast.Items.Remove(item);
                            break;
                        }
                    }
                }
            }
        }

        private void listView_Broadcast_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (e.IsSelected)
            {
                _selectedDeviceInfo = e.Item.Tag as Broadcast.Info;
            }
        }

        private void button_SelectedDevice_Connect_Click(object sender, EventArgs e)
        {
            if (_selectedDeviceInfo == null || DeviceHandler.Instance.SelectedDevice != null) return;

            Task.Run(async () =>
            {
                await _broadcastViewModel.ConnectDevice(_selectedDeviceInfo);
                DeviceHandler.Instance.SelectedDevice.PropertyChanged += SelectedDevice_PropertyChanged;
            });
        }

        private void button_Control_Disconnect_Click(object sender, EventArgs e)
        {
            if (DeviceHandler.Instance.SelectedDevice == null) return;

            Task.Run(async () =>
            {
                await DeviceHandler.Instance.SelectedDevice.DisposeAsync();
                DeviceHandler.Instance.SelectedDevice.PropertyChanged -= SelectedDevice_PropertyChanged;
                DeviceHandler.Instance.SelectedDevice = null;
            });
        }

        private void SelectedDevice_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Device.ThermalPreview))
            {
                // UI 스레드에서 PictureBox 업데이트
                if (sender is Device device && device.ThermalPreview != null)
                {
                    pictureBox_ThermalPreview.Invoke((Action)(() =>
                    {
                        pictureBox_ThermalPreview.Image = device.ThermalPreview.ToBitmap();
                    }));
                }
            }
            else if (e.PropertyName == nameof(Device.CmosPreview))
            {
                // UI 스레드에서 PictureBox 업데이트
                if (sender is Device device && device.CmosPreview != null)
                {
                    pictureBox_CmosPreview.Invoke((Action)(() =>
                    {
                        pictureBox_CmosPreview.Image = device.CmosPreview.ToBitmap();
                    }));
                }
            }
        }
    }
}
