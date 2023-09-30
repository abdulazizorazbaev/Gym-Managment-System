using Gym.Desktop.Constants;
using Gym.Desktop.Security;
using Npgsql;
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
using System.Windows.Shapes;

namespace Gym.Desktop.Windows
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void Lock_MouseDown_Button(object sender, MouseButtonEventArgs e)
        {
            if (pbPassword.Visibility == Visibility.Visible)
            {
                pbPassword.Visibility = Visibility.Hidden;
                tbPassword.Visibility = Visibility.Visible;
                tbPassword.Text = pbPassword.Password;
            }
            else if (pbPassword.Visibility == Visibility.Hidden)
            {
                pbPassword.Visibility = Visibility.Visible;
                tbPassword.Visibility = Visibility.Hidden;
                pbPassword.Password = tbPassword.Text;
            }
        }

        private async void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            string email = tbEmail.Text;
            string password = pbPassword.Password;
            if (email != "" && password != "" && email.Length > 0 && email.Length < 50 && (email.Contains("@gmail.com") || email.Contains("@mail.ru")) && password.Length > 0 && password.Length < 50)
            {
                await using (var connection = new NpgsqlConnection(DbConstants.DB_CONNECTION_STRING))
                {
                    await connection.OpenAsync();
                    string query = $"SELECT email, password_hash, salt FROM users WHERE email like '{email}'";
                    bool check = false;
                    await using (var command = new NpgsqlCommand(query, connection))
                    {
                        await using (var reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                try
                                {
                                    string fromDbPasswordHash = reader.GetString(1);
                                    if (reader.GetString(0) == email)
                                    {
                                        if (PasswordHasher.Verify(password, fromDbPasswordHash, reader.GetString(2)))
                                        {
                                            check = true;
                                            MessageBoxResult d = MessageBox.Show("You are in system!", "Informing message!", MessageBoxButton.OK);
                                            if (d == MessageBoxResult.OK)
                                            {
                                                await Task.Delay(1000);
                                                MainWindow mainWindow = new MainWindow();
                                                mainWindow.ShowDialog();
                                                this.Close();
                                            }
                                        }
                                    }
                                }
                                catch { }
                            }
                            await reader.CloseAsync();
                        }
                    }
                    await connection.CloseAsync();
                    if (!check) { MessageBox.Show("Email or password doesn't match!"); }
                }
            }
            else MessageBox.Show("Please, fill all fields correctly!");
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}