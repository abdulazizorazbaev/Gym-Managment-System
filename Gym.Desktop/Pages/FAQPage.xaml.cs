using Gym.Desktop.Components.FAQ;
using Gym.Desktop.Components.Packages;
using Gym.Desktop.Utilities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Gym.Desktop.Pages
{
    /// <summary>
    /// Interaction logic for FAQPage.xaml
    /// </summary>
    public partial class FAQPage : Page
    {
        public FAQPage()
        {
            InitializeComponent();
        }

        private async Task RefreshAsync()
        {
            wrpFAQ.Children.Clear();
            Dictionary<string, string> questions = new Dictionary<string, string>();
            questions.Add("How much cardio should I be doing?", "With cardio, the amount you’re doing depends on a lot of different factors: your current activity level, your goals, and how hard you’re working. If you’re currently sedentary, general recommendations of around 150 minutes of moderate cardio each week (about 30 minutes most days of the day), would be way too much. In that case, you’d start with shorter rounds: 10-15 minutes each day of easy cardio (like walking) and build on from there.");
            questions.Add("How often should I rest?", "Rest is such a huge piece of the fitness puzzle and it’s easy to forget that the magic happens when we REST. This is when the body is able to restore its levels, rebuild muscle, and gather energy and fuel for the next session. If you’re constantly working yourself into the ground, there’s a point where you’ll hit diminishing returns. Too much exercise can lead to overtraining, which can potentially cause elevated resting heart rate, injury, poor sleep, low energy levels, depression/anxiety, agitation, decrease in performance, and extreme soreness/pain.");
            questions.Add("Should I stretch before my workouts?", "Research is mixed on this, so if you don’t want to stretch, you can skip it! Static stretching before a workout has actually been shown to DECREASE speed and performance. If you’re going to stretch before a workout, use this chance to move your body through full range of motion exercises to prepare for the workout you’re about to do. Your warmup moves should mimic the “meat” of your workout and include dynamic stretching. Static stretching (holding for 15-25 seconds per stretch) is a better choice AFTER your workout. You can foam roll either before or after your workout.");
            questions.Add("How much weight should I be lifting when I strength train?", "ResearchWhen you choose a weight for strength training, pick a weight that’s “heavy for you.” For example, 5 lbs may be heavy for one person, while 50 lbs is heavy for someone else. This will depend on your current strength and fitness level.\r\nYou should be able to complete all reps in a set with good form, and have to push yourself to finish the last 1-2 reps of each set. If you could easily breeze through 15-20 reps of an exercise at your current weight, it’s a good sign to bump it up a little.");
            questions.Add("What should I do about muscle soreness?", "What’s the benefit of pushing yourself super hard so you can’t move the rest of the week? If your muscles are incredbily sore, focus on adequate water and protein intake, stretch your muscles (dynamic and static stretching feels great), a bath with epsom salts, and magnesium oil. Easy cardio and moving the legs can help to reduce soreness. Also, if you have a sauna blanket or access to a sauna, this is an incredible recovery tool for sore muscles and inflammation. Make sure you’re designating 1-2 days as a recovery day during the week, too.");
            foreach (var item in questions)
            {
                FAQViewUserControl fAQViewUserControl = new FAQViewUserControl();
                fAQViewUserControl.SetData(item.Key, item.Value);
                wrpFAQ.Children.Add(fAQViewUserControl);
            }
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            await RefreshAsync();
        }
    }
}
