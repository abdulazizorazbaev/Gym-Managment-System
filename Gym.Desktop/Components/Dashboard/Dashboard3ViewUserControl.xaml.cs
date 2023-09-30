using Gym.Desktop.Repositories.Memberships;
using Gym.Desktop.Repositories.Packages;
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

namespace Gym.Desktop.Components.Dashboard
{
    /// <summary>
    /// Interaction logic for Dashboard3ViewUserControl.xaml
    /// </summary>
    public partial class Dashboard3ViewUserControl : UserControl
    {
        private readonly MembershipRepository _membershipRepository;
        private readonly PackageRepository _packageRepository;
        
        public Dashboard3ViewUserControl()
        {
            InitializeComponent();
            _membershipRepository = new MembershipRepository();
            _packageRepository = new PackageRepository();
        }

        public async void SetData()
        {
            var paidMembersList = await _membershipRepository.CountRevenueAsync();
            var revenueList = await _packageRepository.GetAllPackagesAsync();
            float summ = 0;
            foreach (var revenue in revenueList)
            {
                summ += revenue.Price;
            }
            lbDollars.Content = "$" + (summ*2);
            lbCompletedCount.Content = "$" + summ;
        }
    }
}
