using Gym.Desktop.Entities.Packages;
using Gym.Desktop.Windows.Packages;
using System;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace Gym.Desktop.Components.Packages
{
    /// <summary>
    /// Interaction logic for PackageViewUserControl.xaml
    /// </summary>
    public partial class PackageViewUserControl : UserControl
    {
        private Package Package { get; set; }
        
        public PackageViewUserControl()
        {
            InitializeComponent();
            Package = new Package();
        }

        public void SetData(Package package)
        {
            ImgB.ImageSource = new BitmapImage( new System.Uri(package.ImagePath, UriKind.Relative));
            lbPackagePrice.Content = package.Price + " USD/package";
            lbPackageName.Content = package.PackageName;
            tbDescription.Text = package.Description;
            lbPackageTotalDays.Content = " - "+ package.Days + " sessions";
            Package = package;
        }

        private void deleteIcon_MouseDown(object sender, MouseButtonEventArgs e)
        {
            PackageUpdateWindow packageUpdateWindow = new PackageUpdateWindow();
            packageUpdateWindow.DeleteAsync(Package);
        }

        private void updateIcon_MouseDown(object sender, MouseButtonEventArgs e)
        {
            PackageUpdateWindow packageUpdateWindow = new PackageUpdateWindow();
            packageUpdateWindow.SetData(Package);
            packageUpdateWindow.ShowDialog();
        }
    }
}