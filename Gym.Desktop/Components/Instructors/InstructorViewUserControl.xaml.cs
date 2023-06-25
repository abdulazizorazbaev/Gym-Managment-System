using Gym.Desktop.Entities.Instructors;
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
        public InstructorViewUserControl()
        {
            InitializeComponent();
        }

        public void SetData(Instructor instructor)
        {
            AvatarImage.ImageSource = new BitmapImage(new Uri(instructor.ImagePath, UriKind.Relative));
        }
    }
}
