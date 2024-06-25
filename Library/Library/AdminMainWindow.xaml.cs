using System;
using System.Windows;

namespace Library
{
    public partial class AdminMainWindow : Window
    {
        public AdminMainWindow()
        {
            InitializeComponent();
        }

        private void AddBook_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MainFrame.Content = new AddBookPage();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error navigating to AddBookPage: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ViewBooks_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new ViewBooks();
        }

        private void AddAuthor_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new AddAuthorPage();
        }

        private void ViewAuthors_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new ViewAuthors();
        }

        private void User_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new ViewUsers();
        }
        private void UserItems_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new UserItems();
        }
        private void Button_Back(object sender, RoutedEventArgs e)
        {
          //היה לי בעיות בחלק זה כשביצאתי שינויים קלים התקן עד הבגרות
        }

        public void UpdateGreetingMessage(string username)
        {
            GreetingTextBlockAdmin.Text = $"Hello, {username}!";
            GreetingTextBlockAdmin.Visibility = Visibility.Visible; 
        }
    }
}
