using Gym.Desktop.Constants;
using Gym.Desktop.Entities.Instructors;
using Gym.Desktop.Entities.Memberships;
using Gym.Desktop.Helpers;
using Gym.Desktop.Interfaces.Instructors;
using Gym.Desktop.Interfaces.Packages;
using Gym.Desktop.Repositories.Instructors;
using Gym.Desktop.Repositories.Packages;
using Microsoft.Win32;
using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace Gym.Desktop.Windows.Instructors
{
    /// <summary>
    /// Interaction logic for InstructorAddWindow.xaml
    /// </summary>
    public partial class InstructorAddWindow : Window
    {
        private readonly IInstructorRepository _instructorRepository;
        private readonly IPackageRepository _packageRepository;

        public InstructorAddWindow()
        {
            InitializeComponent();
            this._instructorRepository = new InstructorRepository();
            this._packageRepository = new PackageRepository();
        }

        private void btnInstructorAddExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void brInstructorAddDrager_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (this.WindowState == WindowState.Maximized)
                this.WindowState = WindowState.Normal;
            this.DragMove();
        }

        private void RichTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            ((RichTextBox)sender).Document = new FlowDocument();
        }

        private async void btnSave_Click(object sender, RoutedEventArgs e)
        {
            Instructor instructor = new Instructor();

            instructor.PackageId = (long)cmbPackageList.SelectedValue;
            instructor.FirstName = tbFirstName.Text;
            instructor.LastName = tbLastName.Text;
            instructor.Email = tbEmail.Text;
            instructor.Description = new TextRange(rbDescription.Document.ContentStart, rbDescription.Document.ContentEnd).Text;
            instructor.Title = tbTitle.Text;
            instructor.PhoneNumber = lbPhoneTemp.Content + tbPhoneNumber.Text;
            instructor.Salary = float.Parse(tbSalary.Text);

            // Image Path
            string imagePath = ImbBImage.ImageSource.ToString();
            if (!String.IsNullOrEmpty(imagePath))
                instructor.ImagePath = await CopyImageAsync(imagePath, ContentConstants.IMAGE_CONTENTS_PATH);

            // Date OF Birth
            if (dtpDateOfBirth.SelectedDate is not null)
                instructor.Date_of_birth = DateOnly.FromDateTime(dtpDateOfBirth.SelectedDate.Value);
            else instructor.Date_of_birth = DateOnly.FromDateTime(TimeHelper.GetDateTime());

            // Gender
            if (rbMale.IsChecked!.Value) instructor.IsMale = true;
            else instructor.IsMale = false;

            var result = await _instructorRepository.CreateAsync(instructor);
            if (result > 0)
            {
                MessageBox.Show("Data has been successfully added!");
                this.Close();
            }
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var packages = await _packageRepository.GetAllAsync(new Utilities.PaginationParams()
            {
                PageNumber = 1,
                PageSize = 100
            });
            cmbPackageList.ItemsSource = packages;
        }

        private void btnImageSelector_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "JPG Files (*.jpg)|*.jpg| JPEG Files (*.jpeg)|*.jpeg| PNG Files (*.png)|*.png| GIF Files (*.gif)|*.gif";
            if (openFileDialog.ShowDialog() == true)
            {
                string imgPath = openFileDialog.FileName;
                ImbBImage.ImageSource = new BitmapImage(new Uri(imgPath, UriKind.Relative));
            }
        }

        private void tbPhoneNumber_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
            
        }

        private void tbSalary_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private async Task<string> CopyImageAsync(string imgPath, string destinationDirectory)
        {
            if (!Directory.Exists(destinationDirectory))
                Directory.CreateDirectory(destinationDirectory);

            var imageName = ContentNameMaker.GetImageName(imgPath);

            string path = System.IO.Path.Combine(destinationDirectory, imageName);

            byte[] image = await File.ReadAllBytesAsync(imgPath);

            await File.WriteAllBytesAsync(path, image);

            return path;
        }
    }
}