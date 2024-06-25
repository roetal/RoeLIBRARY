using ServiceReferenceRoe;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace Library
{
    public partial class ViewBooks : Page
    {
        Service1Client Client = new Service1Client();
        public ObservableCollection<Book> BooksList { get; set; }

        public ViewBooks()
        {
            InitializeComponent();
            LoadBooks();
            SetEditableState(false);
        }

        private async void LoadBooks()
        {
            try
            {
                Book[] booksArray = await Client.GetAllBooksAsync();
                BooksList = new ObservableCollection<Book>(booksArray);
                booksListBox.ItemsSource = BooksList;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void BooksListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (booksListBox.SelectedItem != null)
            {
                Book selectedBook = (Book)booksListBox.SelectedItem;
                DisplayBookInfo(selectedBook);
                SetEditableState(true);
            }
        }

        private void DisplayBookInfo(Book book)
        {
            nameTextBox.Text = book.Name;
            genreTextBox.Text = book.Genre;

            authorIdTextBox.Text = book.AuthorID.ToString();

            availableCopiesTextBox.Text = book.Available_copies.ToString();
            priceTextBox.Text = book.Price.ToString();
        }

        private async void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            if (booksListBox.SelectedItem != null)
            {
                Book selectedBook = (Book)booksListBox.SelectedItem;
                selectedBook.Name = nameTextBox.Text;
                selectedBook.Genre = genreTextBox.Text;
                selectedBook.Available_copies = int.Parse(availableCopiesTextBox.Text);
                selectedBook.Price = int.Parse(priceTextBox.Text);

                try
                {
                    await Client.UpdateBookAsync(selectedBook);
                    MessageBox.Show("Book updated successfully.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error updating book: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Please select a book to update.");
            }
        }

        private void SetEditableState(bool isEditable)
        {
            nameTextBox.IsReadOnly = !isEditable;
            genreTextBox.IsReadOnly = !isEditable;
            availableCopiesTextBox.IsReadOnly = !isEditable;
            priceTextBox.IsReadOnly = !isEditable;
        }

        private async void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (booksListBox.SelectedItem != null)
            {
                Book selectedBook = (Book)booksListBox.SelectedItem;
                try
                {
                    await Client.DeleteBookAsync(selectedBook.Id);
                    BooksList.Remove(selectedBook);
                    ClearBookInfo();

                    DeleteImage(selectedBook.Id);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting book: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Please select a book to delete.");
            }
        }
        public void DeleteImage(int id)
        {
            string currentPath = AppDomain.CurrentDomain.BaseDirectory;
            string directory = Path.Combine(currentPath, @"..\..\..\MainImages");
            string imagePath = Path.Combine(directory, $"MainImage{id}.png");

            if (File.Exists(imagePath))
            {
                File.Delete(imagePath);
                Console.WriteLine($"Image with ID {id} deleted successfully.");
            }
            else
            {
                Console.WriteLine($"Image with ID {id} not found.");
            }
        }

        private void ClearBookInfo()
        {
            nameTextBox.Clear();
            genreTextBox.Clear();
            authorIdTextBox.Clear();
            availableCopiesTextBox.Clear();
            priceTextBox.Clear();
        }
    }
}
