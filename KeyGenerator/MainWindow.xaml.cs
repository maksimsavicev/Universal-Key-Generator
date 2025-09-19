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

namespace KeyGenerator
{
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
        }

        // main logic

        private Random random = new Random();


        private void SendError(string errorMessage)
        {
            MessageBox.Show(errorMessage, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private string GeneratePassword(string passwordFormat, string charactersAllowed)
        {
            string password = string.Empty;

            for (int i = 0; i < passwordFormat.Length; i++)
            {
                if (passwordFormat[i] == '-')
                {
                    password += '-';
                    continue;
                }


                password += charactersAllowed[random.Next(0, charactersAllowed.Length)];
            }

            return password;
        }

        // components

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                this.DragMove();
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string charactersAllowed = string.Empty;

            if (IncludeLowercase.IsChecked == true)
                charactersAllowed += "abcdefghijklmnopqrstuvwxyz";
            if (IncludeUppercase.IsChecked == true)
                charactersAllowed += "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            if (IncludeNumbers.IsChecked == true)
                charactersAllowed += "0123456789";
            if (IncludeSpecial.IsChecked == true)
                charactersAllowed += "!@#$%";

            if (string.IsNullOrEmpty(charactersAllowed))
            {
                SendError("Select at least one character type.");
                return;
            }

            string keyFormat = FormatBoxTextBox.Text.Trim();
            if (string.IsNullOrEmpty(keyFormat))
            {
                SendError("Key format cannot be empty.");
                return;
            }

            int count;

            try
            {
                count = int.Parse(CountTextBox.Text);
                if (count <= 0)
                {
                    SendError("Enter a number greater than zero.");
                    return;
                }
            }
            catch
            {
                SendError("Enter a valid number.");
                return;
            }

            PasswordResultsTextBox.Clear();

            for (int i = 0; i < count; i++)
            {
                PasswordResultsTextBox.Text += GeneratePassword(keyFormat, charactersAllowed) + Environment.NewLine;
            }
        }

        private void Close_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private async void MinimizeWithLoop()
        {

            // default windows minimize animation did not work, because of WindowStyle settings :( so this is custom

            for (int i = 0; i <= 10; i++)
            {
                this.Opacity = 1 - (double)i / 10;
                await Task.Delay(1);
            }

            this.WindowState = WindowState.Minimized;
            this.Opacity = 1; // to return velocity
        }

        private void Minim_Click_1(object sender, RoutedEventArgs e)
        {
            MinimizeWithLoop();
        }

        private void CopyButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(PasswordResultsTextBox.Text))
            {
                Clipboard.SetText(PasswordResultsTextBox.Text);
                MessageBox.Show("Passwords copied to clipboard!", "Copied", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                SendError("Nothing to copy.");
            }
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void IncludeSpecial_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
