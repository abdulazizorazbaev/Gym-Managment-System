using Gym.Desktop.Entities.Instructors;
using Gym.Desktop.Windows.Instructors;
using MaterialDesignColors.Recommended;
using System;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Gym.Desktop.Components.Instructors
{
    /// <summary>
    /// Interaction logic for InstructorViewUserControl.xaml
    /// </summary>
    public partial class InstructorViewUserControl : UserControl
    {
        private Instructor Instructor { get; set; }

        public InstructorViewUserControl()
        {
            InitializeComponent();
            Instructor = new Instructor();
        }

        public void SetData(Instructor instructor)
        {
            AvatarImage.ImageSource = new BitmapImage(new Uri(instructor.ImagePath, UriKind.Relative));
            lbFirstName.Content = instructor.FirstName;
            lbLastName.Content = instructor.LastName;
            lbTitle.Content = instructor.Title;        
            lbPhoneNumber.Content = instructor.PhoneNumber;
            Instructor = instructor;
        }

        private void deleteIcon_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            InstructorUpdateWindow instructorUpdateWindow = new InstructorUpdateWindow();
            instructorUpdateWindow.DeleteAsync(Instructor);
        }

        private void updateIcon_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            InstructorUpdateWindow instructorUpdateWindow = new InstructorUpdateWindow();
            instructorUpdateWindow.SetData(Instructor);
            instructorUpdateWindow.ShowDialog();
        }
    }
}
