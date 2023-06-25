using Gym.Desktop.Components.Instructors;
using Gym.Desktop.Components.Packages;
using Gym.Desktop.Interfaces.Instructors;
using Gym.Desktop.Interfaces.Packages;
using Gym.Desktop.Repositories.Instructors;
using Gym.Desktop.Repositories.Packages;
using Gym.Desktop.Windows.Instructors;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Gym.Desktop.Pages
{
    /// <summary>
    /// Interaction logic for InstructorsPage.xaml
    /// </summary>
    public partial class InstructorsPage : Page
    {
        private readonly IPackageRepository _packageRepository;
        private readonly IInstructorRepository _instructorRepository;

        public InstructorsPage()
        {
            InitializeComponent();
            this._packageRepository = new PackageRepository();
            this._instructorRepository = new InstructorRepository();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            InstructorAddWindow instructorAddWindow = new InstructorAddWindow();
            instructorAddWindow.ShowDialog();
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            stpPackageChips.Children.Clear();
            var packages = await _packageRepository.GetAllAsync(new Utilities.PaginationParams()
            {
                PageNumber = 1,
                PageSize = 30
            });
            foreach (var package in packages)
            {
                PackageChipUserControl packageChipUserControl = new PackageChipUserControl();
                packageChipUserControl.SetData(package);
                stpPackageChips.Children.Add(packageChipUserControl);
            }

            await RefreshAsync();
        }

        public async Task RefreshAsync()
        {
            wrpInstructors.Children.Clear();
            var instructors = await _instructorRepository.GetAllAsync(new Utilities.PaginationParams()
            {
                PageNumber = 1,
                PageSize = 30
            });
            foreach (var instructor in instructors)
            {
                InstructorViewUserControl instructorViewUserControl = new InstructorViewUserControl();
                instructorViewUserControl.SetData(instructor);
                wrpInstructors.Children.Add(instructorViewUserControl);
            }
        } 
    }
}