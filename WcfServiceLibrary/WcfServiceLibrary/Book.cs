using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WcfServiceLibrary
{
    [DataContract]
    public class Book
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public int AuthorID { get; set; }
        [DataMember]
        public string Genre { get; set; }
        [DataMember]
        public int Price { get; set; }
        [DataMember]
        public int Available_copies { get; set; }
        [DataMember]
        public string Bio { get; set; }

        public Book() { }
        public Book(string name, int authorID, string genre, int price, int available_copies, string bio)
        {
            this.Name = name;
            this.AuthorID = authorID;
            this.Genre = genre;
            this.Price = price;
            this.Available_copies = available_copies;
            this.Bio = bio;
        }
    }
}
