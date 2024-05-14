using System.Configuration;
using System.Data;
using System.Windows;
using WpfSample.Model.Handler;

namespace WpfSample
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        /// <summary>
        /// Gets the current <see cref="App"/> instance in use
        /// </summary>
        public new static App Current => (App)Application.Current;

        public App()
        {
            this.Exit += new ExitEventHandler(App_Exit);
        }

        void App_Exit(object sender, ExitEventArgs e)
        {
            Task.Run(async () =>
            {
                if (DeviceHandler.Instance.SelectedDevice != null)
                {
                    await DeviceHandler.Instance.SelectedDevice.DisposeAsync();
                }

            }).Wait();

        }
    }

}
