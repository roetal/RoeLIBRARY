using ServiceReferenceRoe;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Library
{
    public partial class ViewAuthors : Page
    {
        Service1Client Client = new Service1Client();

        public ViewAuthors()
        {
            InitializeComponent();
            LoadAuthors();
        }

        private async void LoadAuthors()
        {
            try
            {
                Author[] authors = await Client.GetAllAuthorAsync();

                ObservableCollection<AuthorViewModel> authorViewModels = new ObservableCollection<AuthorViewModel>();

                foreach (var author in authors)
                {
                    Book[] books = await Client.GetAuthorBooksAsync(author.Id);

                    AuthorViewModel viewModel = new AuthorViewModel
                    {
                        Id = author.Id,
                        Name = author.Name,
                        Bio = author.Bio,
                        Books = new ObservableCollection<Book>(books)
                    };

                    authorViewModels.Add(viewModel);
                }

                AuthorsListBox.ItemsSource = authorViewModels;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load authors: {ex.Message}");
            }
        }

        private async void DeleteAuthorButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {
                int authorId = (int)button.Tag;
                try
                {
                    await Client.DeleteAuthorAsync(authorId);
                    MessageBox.Show("Author deleted successfully");
                    LoadAuthors();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Failed to delete author: {ex.Message}");
                }
            }
        }

        public class AuthorViewModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Bio { get; set; }
            public ObservableCollection<Book> Books { get; set; }
        }
    }
}
