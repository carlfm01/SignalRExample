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
                //cambiar puerto para pruebas locales
                //o dirección del servidor remoto
                string serverUrl = "http://localhost:17426/signalr";
                _hubConnection = new HubConnection(serverUrl);
                _hubProxy = _hubConnection.CreateHubProxy("NotificationHub");
                _hubProxy.On<string>("NotifyClients", (message) =>
                {
                    //message => mensaje opcional
                   
                    Dispatcher.Invoke(() => lbxMessages.Items.Add($"Cliente remoto - {message}"));
                   
                });
                _hubConnection.StateChanged += _hubConnection_StateChanged;

                await _hubConnection.Start();
                btnReconnect.Visibility = Visibility.Collapsed;
            }
            catch (System.Exception ex)
            {

                MessageBox.Show($"Verificar la dirección del servidor {ex.ToString()}");
            }


        }

        private void _hubConnection_StateChanged(StateChange obj)
        {
            switch (obj.NewState)
            {
                case ConnectionState.Connecting:
                    break;
                case ConnectionState.Connected:
                    MessageBox.Show("Conectado al servidor");
                    break;
                case ConnectionState.Reconnecting:
                    break;
                case ConnectionState.Disconnected:
                    break;
                default:
                    break;
            }
        }

        private void btnReconnect_Click(object sender, RoutedEventArgs e)
        {
            ConnectSignalRAsync();
        }

        private void btnClients_Click(object sender, RoutedEventArgs e)
        {
            //mensaje a enviar a los clientes
            string message = txtMessage.Text;
            try
            {
                if (string.IsNullOrWhiteSpace(message)) 
                    MessageBox.Show("No se pueden enviar mensajes en blanco");
                else
                {
                    _hubProxy.Invoke("NotifyClients", message);
                    lbxMessages.Items.Add($"Tú - {message}");
                    txtMessage.Text = string.Empty;
                }
                    
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString());

            }
        }

        private void btnServer_Click(object sender, RoutedEventArgs e)
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
    }
}
