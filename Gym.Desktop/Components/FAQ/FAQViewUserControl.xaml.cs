using System.Windows.Controls;

namespace Gym.Desktop.Components.FAQ
{
    /// <summary>
    /// Interaction logic for FAQViewUserControl.xaml
    /// </summary>
    public partial class FAQViewUserControl : UserControl
    {
        public FAQViewUserControl()
        {
            InitializeComponent();
        }

        public void SetData(string question, string answer)
        {
            lbQuestion.Content = question;
            textbAnswer.Text = answer;
        }

        private void hideShowBtn1_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            borderAnswer.Height = 0;
            first.Visibility = System.Windows.Visibility.Hidden;
            second.Visibility = System.Windows.Visibility.Visible;
        }

        private void hideShowBtn2_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            if (mainWindow.WindowState == System.Windows.WindowState.Maximized)
                borderAnswer.Height = 60;
            else
                borderAnswer.Height = 150;
            first.Visibility = System.Windows.Visibility.Visible;
            second.Visibility = System.Windows.Visibility.Hidden;
        }
    }
}