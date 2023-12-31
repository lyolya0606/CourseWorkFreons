﻿using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CourseWorkFreons {
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window {
        public LoginWindow() {
            InitializeComponent();
        }

        private void loginButton_Click(object sender, RoutedEventArgs e) {
            if (loginTextBox.Text == "user" && passwordTextBox.Password == "user") {
                Hide();
                new UserWindow().Show();
                Close();
            }

            if (loginTextBox.Text == "admin" && passwordTextBox.Password == "admin") {
                Hide();
                new AdminWindow().Show();
                Close();
            }
        }

    }
}