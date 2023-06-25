using Gym.Desktop.Components.Packages;
using Gym.Desktop.Interfaces.Packages;
using Gym.Desktop.Repositories.Packages;
using Gym.Desktop.Utilities;
using Gym.Desktop.Windows.Packages;
using System;
using System.Collections.Generic;
using System.Linq;
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

namespace Gym.Desktop.Pages
{
    /// <summary>
    /// Interaction logic for PackagesPage.xaml
    /// </summary>
    public partial class PackagesPage : Page
    {
        private readonly IPackageRepository _packageRepository;
        public PackagesPage()
        {
            InitializeComponent();
            this._packageRepository = new PackageRepository();
        }

        private async void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            PackageAddWindow packageAddWindow = new PackageAddWindow();
            packageAddWindow.ShowDialog();
            await RefreshAsync();
        }

        private async Task RefreshAsync()
        {
            wrpPackages.Children.Clear();
            PaginationParams paginationParams = new PaginationParams()
            {
                PageNumber = 1,
                PageSize = 30,
            };
            var packages = await _packageRepository.GetAllAsync(paginationParams);
            foreach (var package in packages)
            {
                PackageViewUserControl packageViewUserControl = new PackageViewUserControl();
                packageViewUserControl.SetData(package);
                wrpPackages.Children.Add(packageViewUserControl);
            }
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            await RefreshAsync();
        }
    }
}
