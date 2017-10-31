using Microsoft.AspNet.SignalR.Client;
using System.Windows;

namespace SignalRClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private HubConnection _hubConnection;
        private IHubProxy _hubProxy;

        public MainWindow()
        {
            InitializeComponent();
            ConnectSignalRAsync();
        }

        private async void ConnectSignalRAsync()
        {
            try
            {
                string serverUrl = "http://localhost:17426/signalr";
                _hubConnection = new HubConnection(serverUrl);
                _hubProxy = _hubConnection.CreateHubProxy("NotificationHub");
                _hubProxy.On<string>("NotifyClients", (message) =>
                {
                    //message => mensaje opcional
                    MessageBox.Show("Cliente notificado");
                });
                await _hubConnection.Start();
            }
            catch (System.Exception ex)
            {

                MessageBox.Show($"Verificar la dirección del servidor {ex.ToString()}");
            }


        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _hubProxy.Invoke("NotifyServer");
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString());

            }

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                _hubProxy.Invoke("NotifyClients", "Mensaje opcional");
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString());

            }
        }
    }
}
