using Gym.Desktop.Entities.Packages;
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

namespace Gym.Desktop.Components.Packages
{
    /// <summary>
    /// Interaction logic for PackageViewUserControl.xaml
    /// </summary>
    public partial class PackageViewUserControl : UserControl
    {
        public long Id { get; private set; }
        public PackageViewUserControl()
        {
            InitializeComponent();
        }

        public void SetData(Package package)
        {
            Id = package.Id;
            ImgB.ImageSource = new BitmapImage( new System.Uri(package.ImagePath, UriKind.Relative));
            lbPackagePrice.Content = package.Price + " USD/package";
            lbPackageName.Content = package.PackageName;
            tbDescription.Text = package.Description;
            lbPackageTotalDays.Content = " - "+ package.Days + " sessions";
        }

        private void grMain_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show(Id.ToString(), lbPackageName.Content.ToString(), MessageBoxButton.OK);
        }
    }
}
