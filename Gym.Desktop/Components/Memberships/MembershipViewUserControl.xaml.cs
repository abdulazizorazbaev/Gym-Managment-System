using Gym.Desktop.Entities.Clients;
using Gym.Desktop.Entities.Instructors;
using Gym.Desktop.Entities.Memberships;
using Gym.Desktop.Repositories.Clients;
using Gym.Desktop.Repositories.Instructors;
using Gym.Desktop.Windows.Members;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Input;

namespace Gym.Desktop.Components.Memberships
{
    /// <summary>
    /// Interaction logic for MembershipViewUserControl.xaml
    /// </summary>
    public partial class MembershipViewUserControl : UserControl
    {
        private readonly ClientRepository _clientRepository;
        private readonly InstructorRepository _instructorRepository;

        public Membership Membership { get; set; }

        public MembershipViewUserControl()
        {
            InitializeComponent();
            _clientRepository = new ClientRepository();
            _instructorRepository = new InstructorRepository();
            Membership = new Membership();
        }

        public async void SetData(Membership membership)
        {
            lbId.Content = membership.Id;

            List<Client> clients = await _clientRepository.GetAllClientsAsync();
            foreach (Client client in clients)
            {
                if (membership.ClientId == client.Id)
                    lbClient.Content = client.FirstName;
            }

            List<Instructor> instructors = await _instructorRepository.GetAllInstructorsAsync();
            foreach (Instructor instructor in instructors)
            {
                if (membership.InstructorId == instructor.Id)
                    lbInstructor.Content = instructor.FirstName;
            }

            lbStartDate.Content = membership.StartDate;
            lbEndDate.Content = membership.EndDate;
            lbStatus.Content = membership.MembershipStatus;
            lbPaymentDate.Content = membership.PaymentDate;
            Membership = membership;
        }

        private void deleteIcon_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MemberUpdateWindow memberUpdateWindow = new MemberUpdateWindow();
            memberUpdateWindow.DeleteAsync(Membership);
        }

        private void updateIcon_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MemberUpdateWindow memberUpdateWindow = new MemberUpdateWindow();
            memberUpdateWindow.SetData(Membership);
            memberUpdateWindow.ShowDialog();
        }
    }
}