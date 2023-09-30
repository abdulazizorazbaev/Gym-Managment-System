using Gym.Desktop.Components.Clients;
using Gym.Desktop.Interfaces.Clients;
using Gym.Desktop.Repositories.Clients;
using Gym.Desktop.Utilities;
using Gym.Desktop.Windows.Clients;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Gym.Desktop.Pages
{
    /// <summary>
    /// Interaction logic for ClientsPage.xaml
    /// </summary>
    public partial class ClientsPage : Page
    {
        private readonly IClientRepository _clientRepository;

        public ClientsPage()
        {
            InitializeComponent();
            this._clientRepository = new ClientRepository();
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            await RefreshAsync();
        }

        private async void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            ClientAddWindow clientAddWindow = new ClientAddWindow();
            clientAddWindow.ShowDialog();
            await RefreshAsync();
        }

        private async Task RefreshAsync()
        {
            wrpClients.Children.Clear();
            PaginationParams paginationParams = new PaginationParams()
            {
                PageNumber = 1,
                PageSize = 30,
            };
            var clients = await _clientRepository.GetAllAsync(paginationParams);
            foreach (var client in clients)
            {
                ClientViewUserControl clientViewUserControl = new ClientViewUserControl();
                clientViewUserControl.SetData(client);
                wrpClients.Children.Add(clientViewUserControl);
            }
        }

        private async void OnKeyDownHandler(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Return && tbSearch.Text != "")
            {
                wrpClients.Children.Clear();
                PaginationParams paginationParams = new PaginationParams()
                {
                    PageNumber = 1,
                    PageSize = 30,
                };
                var clients = await _clientRepository.GetAllAsync(paginationParams);
                bool check = false;
                foreach (var client in clients)
                {
                    if (client.FirstName.Contains(tbSearch.Text) || client.LastName.Contains(tbSearch.Text))
                    {
                        ClientViewUserControl clientViewUserControl = new ClientViewUserControl();
                        clientViewUserControl.SetData(client);
                        wrpClients.Children.Add(clientViewUserControl);
                        check = true;
                        break;
                    }
                }
                if (!check)
                {
                    MessageBoxResult result = MessageBox.Show("No Result!", "Informing message", MessageBoxButton.OK);
                    if (result == MessageBoxResult.OK)
                        await RefreshAsync();
                }
            }
            else if (e.Key == Key.Return && tbSearch.Text == "")
            {
                await RefreshAsync();
            }
        }
    }
}