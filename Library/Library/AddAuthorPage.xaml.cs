using System;
using System.Windows;
using System.Windows.Controls;
using ServiceReferenceRoe;

namespace Library
{
    /// <summary>
    /// Interaction logic for AddAuthorPage.xaml
    /// </summary>
    public partial class AddAuthorPage : Page
    {
        public AddAuthorPage()
        {
            InitializeComponent();
        }

        private async void AddAuthorButton_Click(object sender, RoutedEventArgs e)
        {
            string authorName = AuthorNameTextBox.Text;
            string authorBio = AuthorBioTextBox.Text;

            if (string.IsNullOrWhiteSpace(authorName) || string.IsNullOrWhiteSpace(authorBio))
            {
                MessageBox.Show("Please fill out both the author name and bio.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Use the correct namespace for the LibraryServiceClient and Author
            using (Service1Client Client = new Service1Client())
            {
                Author tmp = new Author();
                tmp.Bio = authorBio;
                tmp.Name = authorName;
                Client.AddAuthorAsync(tmp);

                try
                {
                    MessageBox.Show("Author added successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                    // Clear the text boxes
                    AuthorNameTextBox.Clear();
                    AuthorBioTextBox.Clear();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Failed to add author. Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
