using Gym.Desktop.Components.Instructors;
using Gym.Desktop.Components.Packages;
using Gym.Desktop.Entities.Instructors;
using Gym.Desktop.Interfaces.Instructors;
using Gym.Desktop.Interfaces.Packages;
using Gym.Desktop.Repositories.Instructors;
using Gym.Desktop.Repositories.Packages;
using Gym.Desktop.Utilities;
using Gym.Desktop.Windows.Instructors;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Gym.Desktop.Pages
{
    /// <summary>
    /// Interaction logic for InstructorsPage.xaml
    /// </summary>
    public partial class InstructorsPage : Page
    {
        private readonly IPackageRepository _packageRepository;
        private readonly IInstructorRepository _instructorRepository;

        public long PId { get; set; }

        public InstructorsPage()
        {
            InitializeComponent();
            this._packageRepository = new PackageRepository();
            this._instructorRepository = new InstructorRepository();
        }

        private async void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            InstructorAddWindow instructorAddWindow = new InstructorAddWindow();
            instructorAddWindow.ShowDialog();
            await RefreshAsync(0);
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            stpPackageChips.Children.Clear();
            var packages = await _packageRepository.GetAllAsync(new Utilities.PaginationParams()
            {
                PageNumber = 1,
                PageSize = 30
            });
            // All categories
            PackageChipUserControl allpackages = new PackageChipUserControl();
            allpackages.SetData(new Entities.Packages.Package() { Id = 0, PackageName = "All" });
            allpackages.Refresh = RefreshAsync;
            stpPackageChips.Children.Add(allpackages);
            foreach (var package in packages)
            {
                PackageChipUserControl packageChipUserControl = new PackageChipUserControl();
                packageChipUserControl.Refresh = RefreshAsync;
                packageChipUserControl.SetData(package);
                stpPackageChips.Children.Add(packageChipUserControl);
            }

            await RefreshAsync(0);
        }

        public async Task RefreshAsync(long packageId)
        {
            PId = packageId;
            wrpInstructors.Children.Clear();

            IList<Instructor> instructors;
            if (packageId == 0)
            {
                instructors = await _instructorRepository.GetAllAsync(new Utilities.PaginationParams()
                {
                    PageNumber = 1,
                    PageSize = 30
                });
            }
            else
            {
                instructors = await _instructorRepository.GetAllByPackageIdAsync(packageId);
            }
            
            foreach (var instructor in instructors)
            {
                InstructorViewUserControl instructorViewUserControl = new InstructorViewUserControl();
                instructorViewUserControl.SetData(instructor);
                wrpInstructors.Children.Add(instructorViewUserControl);
            }
        }

        private async void OnKeyDownHandler(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Return && tbSearch.Text != "")
            {
                wrpInstructors.Children.Clear();
                IList<Instructor> instructors;
                if (PId == 0)
                {
                    instructors = await _instructorRepository.GetAllAsync(new Utilities.PaginationParams()
                    {
                        PageNumber = 1,
                        PageSize = 30
                    });
                }
                else
                {
                    instructors = await _instructorRepository.GetAllByPackageIdAsync(PId);
                }
                bool check = false;
                foreach (var instructor in instructors)
                {
                    if(instructor.FirstName.Contains(tbSearch.Text) || instructor.LastName.Contains(tbSearch.Text))
                    {
                        InstructorViewUserControl instructorViewUserControl = new InstructorViewUserControl();
                        instructorViewUserControl.SetData(instructor);
                        wrpInstructors.Children.Add(instructorViewUserControl);
                        check = true; 
                        break;
                    }
                }
                if (!check)
                {
                    MessageBoxResult result = MessageBox.Show("No Result!", "Informing message", MessageBoxButton.OK);
                    if (result == MessageBoxResult.OK)
                        await RefreshAsync(0);
                }
            }
            else if (e.Key == Key.Return && tbSearch.Text == "")
            {
                await RefreshAsync(0);
            }
        }
    }
}