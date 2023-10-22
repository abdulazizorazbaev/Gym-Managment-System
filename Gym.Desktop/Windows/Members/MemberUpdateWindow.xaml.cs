using Gym.Desktop.Entities.Clients;
using Gym.Desktop.Entities.Instructors;
using Gym.Desktop.Entities.Memberships;
using Gym.Desktop.Entities.Packages;
using Gym.Desktop.Helpers;
using Gym.Desktop.Repositories.Clients;
using Gym.Desktop.Repositories.Instructors;
using Gym.Desktop.Repositories.Memberships;
using Gym.Desktop.Repositories.Packages;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;

namespace Gym.Desktop.Windows.Members
{
    /// <summary>
    /// Interaction logic for MemberUpdateWindow.xaml
    /// </summary>
    public partial class MemberUpdateWindow : Window
    {
        private readonly ClientRepository _clientRepository;
        private readonly InstructorRepository _instructorRepository;
        private readonly MembershipRepository _membershipRepository;
        private readonly PackageRepository _packageRepository;

        public long Id { get; set; }

        public MemberUpdateWindow()
        {
            InitializeComponent();
            _clientRepository = new ClientRepository();
            _instructorRepository = new InstructorRepository();
            _membershipRepository = new MembershipRepository();
            _packageRepository = new PackageRepository();
        }

        public void SetData(Membership membership)
        {
            //List<Client> clients = await _clientRepository.GetAllClientsAsync();
            //foreach (Client client in clients)
            //{
            //    if (membership.ClientId == client.Id)
            //    {
            //        cmbMembersList.Text = client.FirstName;
            //        break;
            //    }
            //}

            //List<Instructor> instructors = await _instructorRepository.GetAllInstructorsAsync();
            //foreach (Instructor instructor in instructors)
            //{
            //    if (membership.InstructorId == instructor.Id)
            //    {
            //        cmbInstructorsList.Text = instructor.FirstName;
            //        break;
            //    }  
            //}

            //List<Package> packages = await _packageRepository.GetAllPackagesAsync();
            //foreach (Package package in packages)
            //{
            //    if (membership.PackageId == package.Id)
            //    {
            //        cmbPackageList.Text = package.PackageName;
            //        break;
            //    }
            //}

            if (membership.IsPaid)
                rbPaid.IsChecked = true;
            else
                rbUnpaid.IsChecked = false;

            //cmbMembershipStatus.Text = membership.MembershipStatus;
            
            dtpStartDate.Text = membership.StartDate.ToString();
            dtpEndDate.Text = membership.EndDate.ToString();
            dtpPaymentDate.Text = membership.PaymentDate.ToString();
            rbDescription.Document.Blocks.Clear();
            rbDescription.Document.Blocks.Add(new Paragraph(new Run(membership.Description)));
            Id = membership.Id;
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var clients = await _clientRepository.GetAllAsync(new Utilities.PaginationParams()
            {
                PageNumber = 1,
                PageSize = 100
            });
            cmbMembersList.ItemsSource = clients;

            var instructors = await _instructorRepository.GetAllAsync(new Utilities.PaginationParams()
            {
                PageNumber = 1,
                PageSize = 100
            });
            cmbInstructorsList.ItemsSource = instructors;

            var packages = await _packageRepository.GetAllAsync(new Utilities.PaginationParams()
            {
                PageNumber = 1,
                PageSize = 100
            });
            cmbPackageList.ItemsSource = packages;
        }

        public async void DeleteAsync(Membership membership)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure to delete the membership?", "Warning message!", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                var res = await _membershipRepository.DeleteAsync(membership.Id);
                if (res != 0)
                {
                    MessageBox.Show("The membership has been successfully deleted!");
                }
            }
        }

        private void brMemberUpdateDrager_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (this.WindowState == WindowState.Maximized)
                this.WindowState = WindowState.Normal;
            this.DragMove();
        }

        private void btnMemberUpdateExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void RichTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            ((RichTextBox)sender).Document = new FlowDocument();
        }

        private async void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            Membership membership = new Membership();

            membership.PackageId = (long)cmbPackageList.SelectedValue;
            membership.ClientId = (long)cmbMembersList.SelectedValue;
            membership.InstructorId = (long)cmbInstructorsList.SelectedValue;
            
            membership.Description = new TextRange(rbDescription.Document.ContentStart, rbDescription.Document.ContentEnd).Text;

            var selectedValue = ((ComboBoxItem)cmbMembershipStatus.SelectedItem).Content.ToString();
            if (selectedValue is not null)
                membership.MembershipStatus = selectedValue;

            // StartDate
            if (dtpStartDate.SelectedDate is not null)
                membership.StartDate = DateOnly.FromDateTime(dtpStartDate.SelectedDate.Value);
            else membership.StartDate = DateOnly.FromDateTime(TimeHelper.GetDateTime());
            
            // EndDate
            if (dtpEndDate.SelectedDate is not null)
                membership.EndDate = DateOnly.FromDateTime(dtpEndDate.SelectedDate.Value);
            else membership.EndDate = DateOnly.FromDateTime(TimeHelper.GetDateTime());

            // PaymentDate
            if (dtpPaymentDate.SelectedDate is not null)
                membership.PaymentDate = DateTime.Now;
            else membership.PaymentDate = TimeHelper.GetDateTime();

            // PaymentStatus
            if (rbPaid.IsChecked!.Value) membership.IsPaid = true;
            else membership.IsPaid = false;

            membership.CreatedAt = membership.UpdatedAt = TimeHelper.GetDateTime();

            var result = await _membershipRepository.UpdateAsync(Id, membership);
            if (result > 0)
            {
                MessageBox.Show("Data has been successfully updated!", "Informing message", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("Something went wrong!", "Informing message", MessageBoxButton.OK);
                this.Close();
            }
        }
    }
}