using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using WcfServiceLibrary;

namespace Library
{
    public partial class UserItems : Page
    {
        private Service1 _service;

        public UserItems()
        {
            InitializeComponent();
            _service = new Service1();
            LoadUsers();
        }

        private void LoadUsers()
        {
            UsersListBox.ItemsSource = _service.GetAllUsers();
        }

        private async void UsersListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (UsersListBox.SelectedItem is Users selectedUser)
            {
                var cartItems = await _service.GetCartItemsByUsernameAsync(selectedUser.Username);
                var activeItems = cartItems.Where(item => !item.RemainingTime.Contains("-")).ToList();
                var expiredItems = cartItems.Where(item => item.RemainingTime.Contains("-")).ToList();

                ActiveCartItemsListBox.ItemsSource = activeItems;
                ExpiredCartItemsListBox.ItemsSource = expiredItems;
            }
        }
    }
}
