using Gym.Desktop.Components.Payments;
using Gym.Desktop.Repositories.Payments;
using Gym.Desktop.Utilities;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Gym.Desktop.Pages
{
    /// <summary>
    /// Interaction logic for PaymentsPage.xaml
    /// </summary>
    public partial class PaymentsPage : Page
    {
        private readonly PaymentRepository _paymentRepository;
        public PaymentsPage()
        {
            InitializeComponent();
            _paymentRepository = new PaymentRepository();
        }

        private async void OnKeyDownHandler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return && tbSearch.Text != "")
            {
                wrpPayments.Children.Clear();
                PaginationParams paginationParams = new PaginationParams()
                {
                    PageNumber = 1,
                    PageSize = 30,
                };
                var payments = await _paymentRepository.GetAllAsync(paginationParams);
                bool check = false;
                foreach (var payment in payments)
                {
                    if (payment.PaymentType.Contains(tbSearch.Text))
                    {
                        PaymentViewUserControl paymentViewUserControl = new PaymentViewUserControl();
                        paymentViewUserControl.SetData(payment);
                        wrpPayments.Children.Add(paymentViewUserControl);
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

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            await RefreshAsync();
        }

        private async Task RefreshAsync()
        {
            wrpPayments.Children.Clear();
            PaginationParams paginationParams = new PaginationParams()
            {
                PageNumber = 1,
                PageSize = 30
            };
            var payments = await _paymentRepository.GetAllAsync(paginationParams);
            foreach (var payment in payments)
            {
                PaymentViewUserControl paymentViewUserControl = new PaymentViewUserControl();
                paymentViewUserControl.SetData(payment);
                wrpPayments.Children.Add(paymentViewUserControl);
            }
        }
    }
}
