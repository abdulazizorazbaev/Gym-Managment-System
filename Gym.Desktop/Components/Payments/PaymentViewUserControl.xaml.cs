using Gym.Desktop.Entities.Clients;
using Gym.Desktop.Entities.Instructors;
using Gym.Desktop.Entities.Memberships;
using Gym.Desktop.Entities.Payments;
using Gym.Desktop.Repositories.Clients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Gym.Desktop.Components.Payments
{
    /// <summary>
    /// Interaction logic for PaymentViewUserControl.xaml
    /// </summary>
    public partial class PaymentViewUserControl : UserControl
    {
        private readonly ClientRepository _clientRepository;

        public Payment Payment { get; set; }

        public PaymentViewUserControl()
        {
            InitializeComponent();
            _clientRepository = new ClientRepository();
            Payment = new Payment();
        }
        public async void SetData(Payment payment)
        {
            lbId.Content = payment.Id;

            List<Client> clients = await _clientRepository.GetAllClientsAsync();
            foreach (Client client in clients)
            {
                if (payment.ClientId == client.Id)
                    lbClient.Content = client.FirstName;
            }
            lbPaymentType.Content = payment.PaymentType;
            lbCreatedAt.Content = payment.CreatedAt;
            lbUpdatedAt.Content = payment.UpdatedAt;
            Payment = payment;
        }

        private void deleteIcon_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void updateIcon_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }
    }
}