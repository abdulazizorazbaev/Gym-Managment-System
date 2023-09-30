using Gym.Desktop.Repositories.Memberships;
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
    /// Interaction logic for Dashboard2ViewUserControl.xaml
    /// </summary>
    public partial class Dashboard2ViewUserControl : UserControl
    {
        private readonly MembershipRepository _membershipRepository;
        public Dashboard2ViewUserControl()
        {
            InitializeComponent();
            _membershipRepository = new MembershipRepository();
        }

        public async void SetData()
        {
            lbActiveCount.Content = await _membershipRepository.CountAsync();
            lbCompletedCount.Content = await _membershipRepository.CountActiveAsync();
        }
    }
}