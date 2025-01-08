using KIRSharp;
using System.Collections.Specialized;
using System.ComponentModel;
using OpenCvSharp.Extensions;
using WpfSample.Model;
using WpfSample.Model.Handler;
using WpfSample.ViewModel;
using System.Diagnostics;

namespace WinFormsSample
{
    public partial class MainForm : Form
    {
        private BroadCastViewModel _broadcastViewModel;
        private Info _selectedDeviceInfo;

        private ControlViewModel _controlViewModel;

        public MainForm()
        {
            InitializeComponent();
            Process();
        }

        private void Process()
        {
            _broadcastViewModel = new BroadCastViewModel();
            _broadcastViewModel.BroadcastDeviceInfos.CollectionChanged += BroadcastDeviceInfos_CollectionChanged;

            _controlViewModel = new ControlViewModel();
            comboBox_Control_PseudoColor.DataSource = _controlViewModel.PseudoColors;
            comboBox_Control_PseudoColor.SelectedIndexChanged += ComboBox_Control_PseudoColor_SelectedIndexChanged;
        }

        private void BroadcastDeviceInfos_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (Info newItem in e.NewItems)
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
                foreach (Info oldItem in e.OldItems)
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

            label_Broadcast_Count.Text = listView_Broadcast.Items.Count.ToString();
        }

        private void listView_Broadcast_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (e.IsSelected)
            {
                _selectedDeviceInfo = e.Item.Tag as Info;
                label_SelectedDevice_Version.Text = _selectedDeviceInfo.Version;
                label_SelectedDevice_Ip.Text = _selectedDeviceInfo.Ip;
                label_SelectedDevice_Netmask.Text = _selectedDeviceInfo.NetMask;
                label_SelectedDevice_Port.Text = _selectedDeviceInfo.Port;
                label_SelectedDevice_Gateway.Text = _selectedDeviceInfo.Gateway;
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
                if (sender is Device device && device.ThermalPreview != null)
                {
                    pictureBox_ThermalPreview.Invoke((Action)(() =>
                    {
                        pictureBox_ThermalPreview.Image = device.ThermalPreview.ToBitmap();
                        label_Preview_Max.Text = device.TempMax.ToString();
                        label_Preview_Avrg.Text = device.TempAvr.ToString();
                        label_Preview_Min.Text = device.TempMin.ToString();
                    }));
                }
            }
            else if (e.PropertyName == nameof(Device.CmosPreview))
            {
                if (sender is Device device && device.CmosPreview != null)
                {
                    pictureBox_CmosPreview.Invoke((Action)(() =>
                    {
                        pictureBox_CmosPreview.Image = device.CmosPreview.ToBitmap();
                    }));
                }
            }
        }


        private void ComboBox_Control_PseudoColor_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (comboBox_Control_PseudoColor.SelectedItem is PseudoColor.Type selectedColor)
            {
                if (DeviceHandler.Instance.SelectedDevice != null)
                {
                    DeviceHandler.Instance.SelectedDevice.PseudoColorType = selectedColor;
                }
            }
        }

        private void button_Control_Offset_Get_Click(object sender, EventArgs e)
        {
            if (DeviceHandler.Instance.SelectedDevice == null) return;

            Task.Run(async () =>
            {
                var camera = DeviceHandler.Instance.SelectedDevice.Camera as UdpCamera;
                if (camera == null) return;

                var offset = await camera.GetOffset();
                if (offset == null) return;

                textBox_Control_Offset.Text = offset.ToString();
                DeviceHandler.Instance.SelectedDevice.UserOffset = (double)offset;
            });
        }

        private void button_Control_Offset_Set_Click(object sender, EventArgs e)
        {
            if (DeviceHandler.Instance.SelectedDevice == null) return;

            Task.Run(async () =>
            {
                var camera = DeviceHandler.Instance.SelectedDevice.Camera as UdpCamera;
                if (camera == null) return;

                var offset = await camera.SetOffset(Convert.ToDouble(textBox_Control_Offset.Text));
                if (offset != null)
                {
                    DeviceHandler.Instance.SelectedDevice.UserOffset = (double)offset;
                };
            });
        }

        private void button_Control_ActiveOnceShutter_Click(object sender, EventArgs e)
        {
            try
            {
                if (DeviceHandler.Instance.SelectedDevice == null) return;

                Task.Run(async () =>
                {
                    var udpCamera = (UdpCamera)DeviceHandler.Instance.SelectedDevice.Camera;
                    await udpCamera.RunShutterManually();
                });
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"ActiveOnceShutter: {ex.Message}");
            }
        }
    }
}
