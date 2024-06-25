using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WcfServiceLibrary
{
    [DataContract]
    public class Shoping_cart
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int Book_id { get; set; }
        [DataMember]
        public int User_id { get; set; }
        [DataMember]
        public DateTime AddedDate { get; set; }

        public Shoping_cart() { }

        public Shoping_cart(int book_id, int user_id,DateTime dt)
        {
            this.Book_id = book_id;
            this.User_id = user_id;
            this.AddedDate = dt;
        }
    }
}
