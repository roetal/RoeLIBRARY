using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;

namespace WcfServiceLibrary
{
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        void AddBook(Book book);

        [OperationContract]
        List<Book> GetAllBooks();

        [OperationContract]
        Book GetBookById(int bookId);

        [OperationContract]
        int GetBookIdbyName(string name);

        [OperationContract]
        void AddAuthor(Author a);

        [OperationContract]
        string GetAuthorName(int id);

        [OperationContract]
        Author GetAuthorById(int id);

        [OperationContract]
        void LoadAuthors();

        [OperationContract]
        void OnPropertyChanged(string propertyName);

        [OperationContract]
        void AddUser(Users user);

        [OperationContract]
        bool checkusername(Users user);

        [OperationContract]
        Users GetUserWithName(string username);

        [OperationContract]
        List<Users> GetAllUsers();

        [OperationContract]
        Users ValidateUser(string username, string password);

        [OperationContract]
        List<Book> GetAuthorBooks(int authorId);

        [OperationContract]
        List<Author> GetAllAuthor();

        [OperationContract]
        bool IsUserAdmin(string username);

        [OperationContract]
        List<Author> GetAuthorNameList();

        [OperationContract]
        void AddToCart(Shoping_cart SC);

        [OperationContract]
        Task<List<CartItemDto>> GetCartItemsByUsernameAsync(string username);

        [OperationContract]
        byte[] getImageBytes(int id);

        [OperationContract]
        void UpdateUser(Users updatedUser);

        [OperationContract]
        void DeleteUser(int userId);

        [OperationContract]
        void DeleteBook(int bookID);

        [OperationContract]
        void DeleteAuthor(int authorID);

        [OperationContract]
        void AddReview(Reviews review);

        [OperationContract]
        double GetAverageRating(int bookId);

        [OperationContract]
        List<Reviews> GetBookReviews(int bookId);

        [OperationContract]
        void OpenAdminMenu();

        [OperationContract]
        void CloseAdminMenu();
        [OperationContract]
        Task UpdateBookAsync(Book updatedBook);
        [OperationContract]
        void DeleteBookFromCart(int cartID);

        [OperationContract]
        void DeleteUserReviews(int RevID);
    }
}
