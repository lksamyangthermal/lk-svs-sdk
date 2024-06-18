using KIRSharp;
using System.Collections.Specialized;
using WpfSample.ViewModel;

namespace WinFormsSample
{
    public partial class MainForm : Form
    {
        private BroadCastViewModel _broadcastViewModel;

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

        private void BroadcastDeviceInfos_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (Broadcast.Info newItem in e.NewItems)
                {
                    var info = new string[] { newItem.Version, newItem.Ip };
                    var item = new ListViewItem(info);
                    item.Tag = newItem;
                    listView_Broadcast.Items.Add(item);
                    //listView_Broadcast.Items[0].SubItems.Add(newItem.Version);
                    //listView_Broadcast.Items[1].SubItems.Add(newItem.Ip);
                }
            }
            else if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                //foreach (Broadcast.Info oldItem in e.OldItems)
                //{
                //}
            }
        }
    }
}
