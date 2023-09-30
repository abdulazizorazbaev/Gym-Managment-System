using Gym.Desktop.Constants;
using Gym.Desktop.Entities.Clients;
using Gym.Desktop.Entities.Instructors;
using Gym.Desktop.Helpers;
using Gym.Desktop.Interfaces.Clients;
using Gym.Desktop.Repositories.Clients;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Gym.Desktop.Windows.Clients
{
    /// <summary>
    /// Interaction logic for ClientAddWindow.xaml
    /// </summary>
    public partial class ClientAddWindow : Window
    {
        private readonly IClientRepository _clientRepository;
        public ClientAddWindow()
        {
            InitializeComponent();
            this._clientRepository = new ClientRepository();
        }

        private void brClientAddDrager_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (this.WindowState == WindowState.Maximized)
                this.WindowState = WindowState.Normal;
            this.DragMove();
        }

        private void btnClientAddExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void RichTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            ((RichTextBox)sender).Document = new FlowDocument();
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

        private async void btnSave_Click(object sender, RoutedEventArgs e)
        {
            Client client = new Client();

            client.FirstName = tbFirstName.Text;
            client.LastName = tbLastName.Text;
            client.Email = tbEmail.Text;
            client.Description = new TextRange(rbDescription.Document.ContentStart, rbDescription.Document.ContentEnd).Text;
            client.PhoneNumber = lbPhoneTemp.Content + tbPhoneNumber.Text;
            client.PassportSerialNumber = tbPassportSerialNumber.Text;

            // Image Path
            string imagePath = ImbBImage.ImageSource.ToString();
            if (!String.IsNullOrEmpty(imagePath))
                client.ImagePath = await CopyImageAsync(imagePath, ContentConstants.IMAGE_CONTENTS_PATH);

            // Date OF Birth
            if (dtpDateOfBirth.SelectedDate is not null)
                client.Date_of_birth = DateOnly.FromDateTime(dtpDateOfBirth.SelectedDate.Value);
            else client.Date_of_birth = DateOnly.FromDateTime(TimeHelper.GetDateTime());

            // Gender
            if (rbMale.IsChecked!.Value) client.IsMale = true;
            else client.IsMale = false;

            var result = await _clientRepository.CreateAsync(client);
            if (result > 0)
            {
                MessageBox.Show("Data has been successfully added!");
                this.Close();
            }
        }
    }
}
