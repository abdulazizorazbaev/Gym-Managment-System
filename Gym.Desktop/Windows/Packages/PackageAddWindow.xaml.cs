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
    /// Interaction logic for PackageAddWindow.xaml
    /// </summary>
    public partial class PackageAddWindow : Window
    {
        private readonly IPackageRepository _packageRepository;

        public PackageAddWindow()
        {
            InitializeComponent();
            this._packageRepository = new PackageRepository();
        }

        private void brPackageAddDrager_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (this.WindowState == WindowState.Maximized)
                this.WindowState = WindowState.Normal;
            this.DragMove();
        }

        private void btnPackageAddExit_Click(object sender, RoutedEventArgs e)
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

        private async void btnSave_Click(object sender, RoutedEventArgs e)
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

            var result = await _packageRepository.CreateAsync(package);
            if (result > 0)
            {
                MessageBox.Show("Data has been successfully added!");
                this.Close();
            }
        }

        private async Task<string> CopyImageAsync(string imgPath, string destinationDirectory)
        {
            if (!Directory.Exists(destinationDirectory))
                Directory.CreateDirectory(destinationDirectory);

            var imageName = ContentNameMaker.GetImageName(imgPath);

            string path = Path.Combine(destinationDirectory, imageName);

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