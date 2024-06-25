using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using ServiceReferenceRoe;

namespace Library
{
    public partial class ViewUsers : Page
    {
        Service1Client Client = new Service1Client();
        public ObservableCollection<Users> UsersList { get; set; }

        public ViewUsers()
        {
            InitializeComponent();
            LoadUsers();
        }

        private async void LoadUsers()
        {
            try
            {
                Users[] usersArray = await Client.GetAllUsersAsync();
                UsersList = new ObservableCollection<Users>(usersArray);
                userListBox.ItemsSource = UsersList;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void UserListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (userListBox.SelectedItem != null)
            {
                Users selectedUser = (Users)userListBox.SelectedItem;
                DisplayUserInfo(selectedUser);
            }
        }

        private void DisplayUserInfo(Users user)
        {
            usernameTextBox.Text = user.Username;
            emailTextBox.Text = user.Email;
            phoneTextBox.Text = user.Phone;
            isAdminCheckBox.IsChecked = user.IsAdmin;
            passwordTextBox.Text = user.Password;
        }

        private async void DeleteUserButton_Click(object sender, RoutedEventArgs e)
        {
            if (userListBox.SelectedItem != null)
            {
                Users selectedUser = (Users)userListBox.SelectedItem;
                try
                {
                    await Client.DeleteUserAsync(selectedUser.Id);
                    UsersList.Remove(selectedUser);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private async void IsAdminCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            UpdateUserAdminStatus(true);
        }

        private async void IsAdminCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            UpdateUserAdminStatus(false);
        }

        private async void UpdateUserAdminStatus(bool isAdmin)
        {
            if (userListBox.SelectedItem != null)
            {
                Users selectedUser = (Users)userListBox.SelectedItem;
                try
                {
                    selectedUser.IsAdmin = isAdmin;
                    await Client.UpdateUserAsync(selectedUser);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
