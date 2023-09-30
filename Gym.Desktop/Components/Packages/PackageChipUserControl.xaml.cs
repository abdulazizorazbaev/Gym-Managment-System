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
    /// Interaction logic for PackageChipUserControl.xaml
    /// </summary>
    public partial class PackageChipUserControl : UserControl
    {
        public Func<long, Task> Refresh { get; set; }

        private Package package = new Package();
        
        public PackageChipUserControl()
        {
            InitializeComponent();
        }

        public void SetData(Package package)
        {
            this.package = package;
            string[] strPackage = package.PackageName.Split(' ');
            lbPackage.Content = strPackage[0];
        }

        private async void UserControl_MouseDown(object sender, MouseButtonEventArgs e)
        {
            await Refresh(package.Id);
        }
    }
}
