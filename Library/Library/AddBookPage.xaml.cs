using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using ServiceReferenceRoe;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using System.Diagnostics;

namespace Library
{
    public partial class AddBookPage : Page
    {
        //private readonly AuthorViewModel _authorViewModel;

        public AddBookPage()
        {
            InitializeComponent();
            //_authorViewModel = new AuthorViewModel();
            //this.DataContext = _authorViewModel;
            using (Service1Client Client = new Service1Client())
            {
                List<Author> Authors = new List<Author> (Client.GetAuthorNameListAsync().Result);
                cmb_Author.ItemsSource = Authors;
    
            }   
        }


        private string selectedImagePath = null;

        private void btn_picture_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files (*.jpg; *.jpeg; *.png)|*.jpg; *.jpeg; *.png";
            if (openFileDialog.ShowDialog() == true)
            {
                selectedImagePath = openFileDialog.FileName;
            }
        }

        private async void Button_Check(object sender, RoutedEventArgs e)
        {
            string name = txt_name.Text;
            string genre = txt_genre.Text;
            int price;
            int availableCopies;
            int authorID;
            string bio = txt_bio.Text;

            if (cmb_Author.SelectedValue != null)
            {
                authorID = (int)cmb_Author.SelectedValue;
            }
            else
            {
                MessageBox.Show("Please select an author.");
                return;
            }

            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(genre) || string.IsNullOrEmpty(bio) ||
                !int.TryParse(txt_price.Text, out price) ||
                !int.TryParse(txt_Available_copies.Text, out availableCopies) || selectedImagePath == null)
            {
                MessageBox.Show("Please ensure all fields are filled out correctly.");
                return;
            }

            using (Service1Client Client = new Service1Client())
            {
                Book book = new Book
                {
                    Name = name,
                    AuthorID = authorID,
                    Genre = genre,
                    Price = price,
                    Available_copies = availableCopies,
                    Bio = bio
                };

                await Client.AddBookAsync(book);

                int bookId = await Client.GetBookIdbyNameAsync(name);

                if (bookId > 0)
                {
                    string targetFolder = @"C:\Users\10\source\repos\WcfServiceLibrary\WcfServiceLibrary\MainImages\";
                    string targetPath = Path.Combine(targetFolder, $"{bookId}{Path.GetExtension(selectedImagePath)}");

                    if (!Directory.Exists(targetFolder))
                    {
                        Directory.CreateDirectory(targetFolder);
                    }

                    Debug.WriteLine($"Selected Image Path: {selectedImagePath}");
                    Debug.WriteLine($"Target Path: {targetPath}");

                    try
                    {
                        File.Copy(selectedImagePath, targetPath, true);
                        MessageBox.Show("Book added successfully and image saved!");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Failed to save the image: {ex.Message}");
                    }
                }
                else
                {
                    MessageBox.Show("Failed to add the book.");
                }
            }
        }

    }
}
