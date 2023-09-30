using Gym.Desktop.Entities.Packages;
using Gym.Desktop.Repositories.Memberships;
using Gym.Desktop.Repositories.Packages;
using System.Windows.Controls;

namespace Gym.Desktop.Components.Dashboard
{
    /// <summary>
    /// Interaction logic for DashboardViewUserControl.xaml
    /// </summary>
    public partial class DashboardViewUserControl : UserControl
    {
        private readonly PackageRepository _packageRepository;

        public DashboardViewUserControl()
        {
            InitializeComponent();
            _packageRepository = new PackageRepository();
        }

        public async void SetData()
        {
            var packagesCount = await _packageRepository.GetAllPackagesAsync();
            lbTotalCount.Content = packagesCount.Count;
            lbActiveCount.Content = packagesCount.Count;
        }
    }
}