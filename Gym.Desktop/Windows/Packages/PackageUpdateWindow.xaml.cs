using Gym.Desktop.Constants;
using Gym.Desktop.Entities.Packages;
using Gym.Desktop.Helpers;
using Gym.Desktop.Interfaces.Packages;
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

namespace Gym.Desktop.Windows.Packages
{
    /// <summary>
    /// Interaction logic for PackageUpdateWindow.xaml
    /// </summary>
    public partial class PackageUpdateWindow : Window
    {
        private readonly IPackageRepository _packageRepository;
        public long Id { get; set; }

        public PackageUpdateWindow()
        {
            InitializeComponent();
            this._packageRepository = new PackageRepository();
        }

        public void SetData(Package package)
        {
            tbName.Text = package.PackageName;
            string imagePath = package.ImagePath;
            ImbBImage.ImageSource = new BitmapImage(new Uri(imagePath, UriKind.Relative));
            tbPrice.Text = package.Price.ToString();
            tbDays.Text = package.Days.ToString();
            rbDescription.Document.Blocks.Clear();
            cmbDuration.Text = package.Duration.ToString();
            rbDescription.Document.Blocks.Add(new Paragraph(new Run(package.Description)));
            Id = package.Id;
        }

        public async void DeleteAsync(Package package)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure to delete the package?", "Warning message!", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                var res = await _packageRepository.DeleteAsync(package.Id);
                if (res != 0)
                {
                    MessageBox.Show("The package has been successfully deleted!");
                }
            }
        }

        private void brPackageUpdateDrager_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (this.WindowState == WindowState.Maximized)
                this.WindowState = WindowState.Normal;
            this.DragMove();
        }

        private void btnPackageUpdateExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
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

        private async void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            Package package = new Package();
            // Name
            package.PackageName = tbName.Text;

            // Image Path
            string imagePath = ImbBImage.ImageSource.ToString();
            if (!String.IsNullOrEmpty(imagePath))
                package.ImagePath = await CopyImageAsync(imagePath, ContentConstants.IMAGE_CONTENTS_PATH);

            // Description
            package.Description = new TextRange(rbDescription.Document.ContentStart, rbDescription.Document.ContentEnd).Text;

            // Package Duration
            package.Duration = cmbDuration.Text;

            // Price
            package.Price = float.Parse(tbPrice.Text);

            // Days
            package.Days = long.Parse(tbDays.Text);

            // CreatedAt and UpdatedAt
            package.CreatedAt = package.UpdatedAt = TimeHelper.GetDateTime();

            var result = await _packageRepository.UpdateAsync(Id, package);
            if (result > 0)
            {
                MessageBox.Show("Data has been successfully updated!", "Informing message", MessageBoxButton.OK);
                this.Close();
            }
            else
            {
                MessageBox.Show("Something went wrong!", "Informing message", MessageBoxButton.OK);
                this.Close();
            }
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

        private void RichTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            ((RichTextBox)sender).Document = new FlowDocument();
        }

        private void tbPrice_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void tbDays_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}