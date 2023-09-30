using Gym.Desktop.Components.Clients;
using Gym.Desktop.Components.Memberships;
using Gym.Desktop.Entities.Memberships;
using Gym.Desktop.Interfaces.Memberships;
using Gym.Desktop.Repositories.Memberships;
using Gym.Desktop.Utilities;
using Gym.Desktop.Windows.Members;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Gym.Desktop.Pages
{
    /// <summary>
    /// Interaction logic for MembersPage.xaml
    /// </summary>
    public partial class MembersPage : Page
    {
        private readonly IMembershipRepository _membershipRepository;

        public MembersPage()
        {
            InitializeComponent();
            this._membershipRepository = new MembershipRepository();
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            await RefreshAsync();
        }

        private async void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            MemberAddWindow memberAddWindow = new MemberAddWindow();
            memberAddWindow.ShowDialog();
            await RefreshAsync();
        }

        private async Task RefreshAsync()
        {
            wrpMembers.Children.Clear();
            PaginationParams paginationParams = new PaginationParams()
            {
                PageNumber = 1,
                PageSize = 30
            };
            var memberships = await _membershipRepository.GetAllAsync(paginationParams);
            foreach (var membership in memberships)
            {
                MembershipViewUserControl membershipViewUserControl = new MembershipViewUserControl();
                membershipViewUserControl.SetData(membership);
                wrpMembers.Children.Add(membershipViewUserControl);
            }
        }

        private async void OnKeyDownHandler(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Return && tbSearch.Text != "")
            {
                wrpMembers.Children.Clear();
                PaginationParams paginationParams = new PaginationParams()
                {
                    PageNumber = 1,
                    PageSize = 30,
                };
                var memberships = await _membershipRepository.GetAllAsync(paginationParams);
                bool check = false;
                foreach (var membership in memberships)
                {
                    if (membership.MembershipStatus.Contains(tbSearch.Text))
                    {
                        MembershipViewUserControl membershipViewUserControl = new MembershipViewUserControl();
                        membershipViewUserControl.SetData(membership);
                        wrpMembers.Children.Add(membershipViewUserControl);
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