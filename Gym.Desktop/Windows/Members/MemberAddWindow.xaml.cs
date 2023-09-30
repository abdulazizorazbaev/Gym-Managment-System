using Gym.Desktop.Entities.Memberships;
using Gym.Desktop.Helpers;
using Gym.Desktop.Interfaces.Clients;
using Gym.Desktop.Interfaces.Instructors;
using Gym.Desktop.Interfaces.Memberships;
using Gym.Desktop.Interfaces.Packages;
using Gym.Desktop.Repositories.Clients;
using Gym.Desktop.Repositories.Instructors;
using Gym.Desktop.Repositories.Memberships;
using Gym.Desktop.Repositories.Packages;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;

namespace Gym.Desktop.Windows.Members
{
    /// <summary>
    /// Interaction logic for MemberAddWindow.xaml
    /// </summary>
    public partial class MemberAddWindow : Window
    {
        private readonly IPackageRepository _packageRepository;
        private readonly IMembershipRepository _membershipRepository;
        private readonly IInstructorRepository _instructorRepository;
        private readonly IClientRepository _clientRepository;
        
        public MemberAddWindow()
        {
            InitializeComponent();
            this._packageRepository = new PackageRepository();
            this._membershipRepository = new MembershipRepository();
            this._instructorRepository = new InstructorRepository();
            this._clientRepository = new ClientRepository();
        }

        private void brMemberAddDrager_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (this.WindowState == WindowState.Maximized)
                this.WindowState = WindowState.Normal;
            this.DragMove();
        }

        private void btnMemberAddExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void RichTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            ((RichTextBox)sender).Document = new FlowDocument();
        }

        private async void btnSave_Click(object sender, RoutedEventArgs e)
        {
            var membership = GetDateFromUI();
            var result = await _membershipRepository.CreateAsync(membership);
            if (result > 0)
            {
                MessageBox.Show("Data has been successfully added!");
                this.Close();
            }
        }

        private Membership GetDateFromUI()
        {
            Membership membership = new Membership();
            membership.PackageId = (long)cmbPackageList.SelectedValue;
            membership.ClientId = (long)cmbMembersList.SelectedValue;
            membership.InstructorId = (long)cmbInstructorsList.SelectedValue;
            membership.Description = new TextRange(rbDescription.Document.ContentStart, rbDescription.Document.ContentEnd).Text;        

            var selectedValue = ((ComboBoxItem)cmbMembershipStatus.SelectedItem).Content.ToString();
            if (selectedValue is not null)
                membership.MembershipStatus = selectedValue;

            if (dtpStartDate.SelectedDate is not null)
                membership.StartDate = DateOnly.FromDateTime(dtpStartDate.SelectedDate.Value);
            else membership.StartDate = DateOnly.FromDateTime(TimeHelper.GetDateTime());

            if (dtpEndDate.SelectedDate is not null)
                membership.EndDate = DateOnly.FromDateTime(dtpEndDate.SelectedDate.Value);
            else membership.EndDate = DateOnly.FromDateTime(TimeHelper.GetDateTime());

            if (dtpPaymentDate.SelectedDate is not null)
                membership.PaymentDate = DateTime.Now;
            else membership.PaymentDate = TimeHelper.GetDateTime();

            if (rbPaid.IsChecked!.Value) membership.IsPaid = true;
            else membership.IsPaid = false;

            membership.CreatedAt = membership.UpdatedAt = TimeHelper.GetDateTime();
            return membership;
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var packages = await _packageRepository.GetAllAsync(new Utilities.PaginationParams()
            {
                PageNumber = 1,
                PageSize = 100
            });
            cmbPackageList.ItemsSource = packages;

            var instructors = await _instructorRepository.GetAllAsync(new Utilities.PaginationParams()
            {
                PageNumber = 1,
                PageSize = 100
            });
            cmbInstructorsList.ItemsSource = instructors;

            var clients = await _clientRepository.GetAllAsync(new Utilities.PaginationParams()
            {
                PageNumber = 1,
                PageSize = 100
            });
            cmbMembersList.ItemsSource = clients;
        }
    }
}