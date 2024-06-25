using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Contexts;
using System.ServiceModel;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WcfServiceLibrary
{
    [ServiceBehavior(IncludeExceptionDetailInFaults = true)]
    public class Service1 : IService1
    {
        private AppDbContext _context = new AppDbContext();
        private static Process _process = null;

        public void AddBook(Book book)
        {
            _context.Book.Add(book);
            _context.SaveChanges();
        }

        public List<Book> GetAllBooks()
        {
            return _context.Book.ToList();
        }

        public Book GetBookById(int bookId)
        {
            return _context.Book.FirstOrDefault(b => b.Id == bookId);
        }

        public int GetBookIdbyName(string name)
        {
            return _context.Book.FirstOrDefault(b => b.Name == name).Id;
        }

        public void AddAuthor(Author a)
        {
            foreach (Author tmp in _context.Author)
            {
                if (tmp.Name == a.Name)
                {
                    return;
                }
            }
            _context.Author.Add(a);
            _context.SaveChanges();
        }

        private ObservableCollection<Author> _authors;
        public ObservableCollection<Author> Authors
        {
            get => _authors;
            set
            {
                _authors = value;
                OnPropertyChanged(nameof(Authors));
            }
        }
        public void LoadAuthors()
        {
            using (var context = new AppDbContext())
            {
                Authors = new ObservableCollection<Author>(context.Author.ToList());
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public string GetAuthorName(int id)
        {
            Author tmp = _context.Author.FirstOrDefault(a => a.Id == id);
            return tmp != null ? tmp.Name : "Unknown Author";
        }

        public Author GetAuthorById(int id)
        {
            return _context.Author.FirstOrDefault(a => a.Id == id);
        }

        public List<Author> GetAllAuthor()
        {
            if (_authors == null)
            {
                LoadAuthors();
            }
            return _authors?.ToList() ?? new List<Author>();
        }

        public List<Book> GetAuthorBooks(int authorId)
        {
            Author author = _context.Author.FirstOrDefault(a => a.Id == authorId);

            if (author == null)
            {
                return new List<Book>();
            }

            List<Book> authorBooks = _context.Book.Where(b => b.AuthorID == authorId).ToList();

            return authorBooks;
        }

        public void AddUser(Users user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public bool checkusername(Users user)
        {
            return !_context.Users.Any(u => u.Username == user.Username);
        }

        public Users GetUserWithName(string username)
        {
            if (string.IsNullOrEmpty(username))
                return null;

            return _context.Users.FirstOrDefault(u => u.Username == username);
        }

        public List<Users> GetAllUsers()
        {
            return _context.Users.ToList();
        }

        public Users ValidateUser(string username, string password)
        {
            return _context.Users.FirstOrDefault(u => u.Username == username && u.Password == password);
        }

        public bool IsUserAdmin(string username)
        {
            if (username == null)
                return false;

            if (GetUserWithName(username).IsAdmin == true)
                return true;

            return false;
        }

        public List<Author> GetAuthorNameList()
        {
            List<Author> list = new List<Author>();
            foreach (Author a in _context.Author)
            {
                list.Add(a);
            }
            return list;
        }

        public byte[] getImageBytes(int id)
        {
            string currentPath = AppDomain.CurrentDomain.BaseDirectory.ToString();
            string directory = Directory.GetParent(Directory.GetParent(currentPath).FullName).FullName;
            string path = Path.Combine(directory, "MainImages", id.ToString() + ".png");

            if (File.Exists(path))
            {
                return File.ReadAllBytes(path);
            }
            return null;
        }

        public void AddToCart(Shoping_cart SC)
        {
            foreach (Shoping_cart _sc in _context.Shoping_cart)
            {
                if (_sc.User_id == SC.User_id && _sc.Book_id == SC.Book_id)
                    return;
            }
            _context.Shoping_cart.Add(SC);
            _context.SaveChanges();
        }

        public async Task<List<CartItemDto>> GetCartItemsByUsernameAsync(string username)
        {
            var cartItems = await (from u in _context.Users
                                   join sc in _context.Shoping_cart on u.Id equals sc.User_id
                                   join b in _context.Book on sc.Book_id equals b.Id
                                   join a in _context.Author on b.AuthorID equals a.Id
                                 
                                   where u.Username == username
                                   select new CartItemDto
                                   {
                                       CartID = sc.Id,
                                       BookName = b.Name,
                                       AuthorName = a.Name,
                                       Price = b.Price,
                                       RemainingTime = (14-(DateTime.Now - sc.AddedDate).Days).ToString() + " days"
                                   }).ToListAsync();

            return cartItems;
        }

        public void UpdateUser(Users updatedUser)
        {
            Users existingUser = _context.Users.FirstOrDefault(u => u.Id == updatedUser.Id);
            if (existingUser != null)
            {
                existingUser.Username = updatedUser.Username;
                existingUser.Email = updatedUser.Email;
                existingUser.Phone = updatedUser.Phone;
                existingUser.Password = updatedUser.Password;
                existingUser.IsAdmin = updatedUser.IsAdmin;

                _context.SaveChanges();
            }
            else
            {
                throw new ArgumentException("User not found");
            }
        }
        public void DeleteUserCarts(int userId)
        {
            Shoping_cart cartsToDelete = _context.Shoping_cart.FirstOrDefault(cart => cart.User_id == userId);
            _context.Shoping_cart.Remove(cartsToDelete);
            _context.SaveChanges();
        }
        public void DeleteUserReviews(int RevID)
        {
            Reviews tmp = _context.Reviews.FirstOrDefault(t => t.Id == RevID);
            _context.Reviews.Remove(tmp);
            _context.SaveChanges();
        }
        public void DeleteUser(int userId)
        {
            Users user = _context.Users.FirstOrDefault(u => u.Id == userId);
            if (user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
            }
            else
            {
                throw new ArgumentException("User not found");
            }
        }
        public void DeleteBook(int bookID)
        {
            Book book = _context.Book.FirstOrDefault(b => b.Id == bookID);
            if (book != null)
            {
                _context.Book.Remove(book);
                _context.SaveChanges();
            }
            else
            {
                throw new ArgumentException("Book not found");
            }
        }

        public void DeleteAuthor(int authorID)
        {
            Author author = _context.Author.FirstOrDefault(a => a.Id == authorID);
            if (author != null)
            {
                _context.Author.Remove(author);
                _context.SaveChanges();
            }
            else
            {
                throw new ArgumentException("Author not found");
            }
        }
        public void AddReview(Reviews review)
        {
            _context.Reviews.Add(review);
            _context.SaveChanges();
        }

        public double GetAverageRating(int bookId)
        {
            var reviews = GetBookReviews(bookId);
            if (reviews.Count == 0) return 0;

            return reviews.Average(r => r.Rating);
        }

        public List<Reviews> GetBookReviews(int bookId)
        {
            List<Reviews> list = new List<Reviews>();
            foreach (Reviews r in _context.Reviews)
            {
                if (r.Book_id == bookId)
                {
                    list.Add(r);
                }
            }
            return list;
        }
        public void OpenAdminMenu()
        {

            //if (_process == null)
            //{
            //    string executablePath = @"C:\Users\10\source\repos\Library\Library\bin\Release\net6.0-windows\Library.exe";
            //    _process = Process.Start(executablePath);
            //}

            string userDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            string relativePath = @"source\repos\Library\Library\bin\Release\net6.0-windows\Library.exe";
            string executablePath = Path.Combine(userDirectory, relativePath);

            if (File.Exists(executablePath))
            {
                Process.Start(executablePath);
            }
            else
            {
                Console.WriteLine("Executable not found at: " + executablePath);
            }


        }
        public void CloseAdminMenu()
        {
            if (_process != null)
            {
                _process.Kill();
                _process.Dispose();
                _process = null;
            }
        }



        public async Task UpdateBookAsync(Book updatedBook)
        {
            Book existingBook = await _context.Book.FindAsync(updatedBook.Id);
            if (existingBook != null)
            {
                existingBook.Name = updatedBook.Name;
                existingBook.Genre = updatedBook.Genre;
                existingBook.Available_copies = updatedBook.Available_copies;
                existingBook.Price = updatedBook.Price;

                await _context.SaveChangesAsync();
            }
            else
            {
                throw new ArgumentException("Book not found");
            }
        }

        public void DeleteBookFromCart(int cartID)
        {
            Shoping_cart cartItem = _context.Shoping_cart.FirstOrDefault(sc => sc.Id == cartID);
            if (cartItem != null)
            {
                _context.Shoping_cart.Remove(cartItem);
                _context.SaveChanges();
            }
        }
    }
    
}
