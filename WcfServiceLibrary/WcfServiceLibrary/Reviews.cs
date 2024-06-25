using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WcfServiceLibrary
{
    [DataContract]
    public class Reviews
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public int Rating { get; set; }
        [DataMember]
        public int Book_id { get; set; }
        [DataMember]
        public int User_id { get; set; }
        [DataMember]
        public DateTime Review_date { get; set; }

        public Reviews() { }

        public Reviews(string description, int rating, int book_id, int user_id,DateTime dt)
        {
            this.Description = description;
            this.Rating = rating;
            this.Book_id = book_id;
            this.User_id = user_id;
            this.Review_date = dt;
        }
    }
}
