using Gym.Desktop.Components.Packages;
using Gym.Desktop.Interfaces.Packages;
using Gym.Desktop.Repositories.Packages;
using Gym.Desktop.Utilities;
using Gym.Desktop.Windows.Packages;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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

        private async void OnKeyDownHandler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return && tbSearch.Text != "")
            {
                wrpPackages.Children.Clear();
                PaginationParams paginationParams = new PaginationParams()
                {
                    PageNumber = 1,
                    PageSize = 30,
                };
                var packages = await _packageRepository.GetAllAsync(paginationParams);
                bool check = false;
                foreach (var package in packages)
                {
                    if (package.PackageName.Contains(tbSearch.Text))
                    {
                        PackageViewUserControl packageViewUserControl = new PackageViewUserControl();
                        packageViewUserControl.SetData(package);
                        wrpPackages.Children.Add(packageViewUserControl);
                        check = true;
                    }
                }
                if (!check)
                {
                    MessageBoxResult result = MessageBox.Show("No Result!", "Informing message", MessageBoxButton.OK);
                    if(result == MessageBoxResult.OK)
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