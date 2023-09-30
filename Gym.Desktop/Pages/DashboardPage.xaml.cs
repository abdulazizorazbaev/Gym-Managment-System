using Gym.Desktop.Components.Dashboard;
using Gym.Desktop.Components.Packages;
using Gym.Desktop.Entities.Packages;
using Gym.Desktop.Repositories.Packages;
using Gym.Desktop.Utilities;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Gym.Desktop.Pages
{
    /// <summary>
    /// Interaction logic for DashboardPage.xaml
    /// </summary>
    public partial class DashboardPage : Page
    {
        private readonly PackageRepository _packageRepository;

        private Package Package { get; set; }

        public DashboardPage()
        {
            InitializeComponent();
            _packageRepository = new PackageRepository();
            Package = new Package();
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            await RefreshAsync();
        }
        private async Task RefreshAsync()
        {
            wrpDashboard.Children.Clear();
            DashboardViewUserControl dashboardViewUserControl = new DashboardViewUserControl();
            dashboardViewUserControl.SetData();
            Dashboard2ViewUserControl dashboard2ViewUserControl = new Dashboard2ViewUserControl();
            dashboard2ViewUserControl.SetData();
            Dashboard3ViewUserControl dashboard3ViewUserControl = new Dashboard3ViewUserControl();
            dashboard3ViewUserControl.SetData();
            wrpDashboard.Children.Add(dashboardViewUserControl);
            wrpDashboard.Children.Add(dashboard2ViewUserControl);
            wrpDashboard.Children.Add(dashboard3ViewUserControl);
        }
    }
}