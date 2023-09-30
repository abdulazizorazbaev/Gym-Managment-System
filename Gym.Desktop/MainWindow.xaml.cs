using Gym.Desktop.Pages;
using MaterialDesignThemes.Wpf;
using System.Windows;
using System.Windows.Input;

namespace Gym.Desktop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnMaximize_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Normal)
                this.WindowState = WindowState.Maximized;
            else this.WindowState = WindowState.Normal;
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void brDrager_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (this.WindowState == WindowState.Maximized)
                this.WindowState = WindowState.Normal;
            this.DragMove();
        }

        private void rbPackages_Click(object sender, RoutedEventArgs e)
        {
            PackagesPage packagesPage = new PackagesPage();
            PageNavigator.Content = packagesPage;
        }

        private void rbMembers_Click(object sender, RoutedEventArgs e)
        {
            MembersPage membersPage = new MembersPage();
            PageNavigator.Content = membersPage;
        }

        private void rbInstructors_Click(object sender, RoutedEventArgs e)
        {
            InstructorsPage instructorsPage = new InstructorsPage();
            PageNavigator.Content = instructorsPage;
        }

        private void rbClients_Click(object sender, RoutedEventArgs e)
        {
            ClientsPage clientsPage = new ClientsPage();
            PageNavigator.Content = clientsPage;
        }

        private void rbPayments_Click(object sender, RoutedEventArgs e)
        {
            PaymentsPage paymentsPage = new PaymentsPage();
            PageNavigator.Content = paymentsPage;
        }

        private void rbDashboard_Click(object sender, RoutedEventArgs e)
        {
            DashboardPage dashboardPage = new DashboardPage();
            PageNavigator.Content = dashboardPage;
        }

        private void rbAbout_Click(object sender, RoutedEventArgs e)
        {
            AboutPage aboutPage = new AboutPage();
            PageNavigator.Content = aboutPage;
        }

        private void rbFAQ_Click(object sender, RoutedEventArgs e)
        {
            FAQPage fAQPage = new FAQPage();
            PageNavigator.Content = fAQPage;
        }

        private void rbSessions_Click(object sender, RoutedEventArgs e)
        {
            SessionsPage sessionsPage = new SessionsPage();
            PageNavigator.Content = sessionsPage;
        }

        private void rbAttendance_Click(object sender, RoutedEventArgs e)
        {
            AttendancePage attendancePage = new AttendancePage();
            PageNavigator.Content = attendancePage;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DashboardPage dashboardPage1 = new DashboardPage();
            PageNavigator.Content = dashboardPage1;
        }
    }
}