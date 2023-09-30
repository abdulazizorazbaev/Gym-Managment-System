using Gym.Desktop.Entities.Clients;
using Gym.Desktop.Utilities;
using Gym.Desktop.Windows.Clients;
using System.Windows.Controls;
using System.Windows.Input;

namespace Gym.Desktop.Components.Clients
{
    /// <summary>
    /// Interaction logic for ClientViewUserControl.xaml
    /// </summary>
    public partial class ClientViewUserControl : UserControl
    {
        public Client Client { get; set; }

        public ClientViewUserControl()
        {
            InitializeComponent();
            Client = new Client();
        }

        public void SetData(Client client)
        {
            lbId.Content = client.Id;
            lbName.Content = client.FirstName;
            lbSurname.Content = client.LastName;
            lbDob.Content = client.Date_of_birth;
            lbGender.Content = client.IsMale;
            lbPhoneNumber.Content = client.PhoneNumber;
            lbPSN.Content = client.PassportSerialNumber;
            Client = client;
        }

        private void deleteIcon_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ClientUpdateWindow clientUpdateWindow = new ClientUpdateWindow();
            clientUpdateWindow.DeleteAsync(Client);
        }

        private void updateIcon_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ClientUpdateWindow clientUpdateWindow = new ClientUpdateWindow();
            clientUpdateWindow.SetData(Client);
            clientUpdateWindow.ShowDialog();
        }
    }
}