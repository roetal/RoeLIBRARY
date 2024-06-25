using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WcfServiceLibrary
{
    [DataContract]
    public class Users
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Username { get; set; }

        [DataMember]
        public string Password { get; set; }
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public string Phone { get; set; }
        [DataMember]
        public bool IsAdmin { get; set; }

        public Users(string username, string password, string email, string phone)
        {
            this.Username = username;
            this.Password = password;
            this.Email = email;
            this.Phone = phone;
            this.IsAdmin = false;
        }
        public Users() { }
    }
}
