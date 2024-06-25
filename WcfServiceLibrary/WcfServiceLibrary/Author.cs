using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WcfServiceLibrary
{
    [DataContract]
    public class Author
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Bio { get; set; }

        public Author() { }
        public Author(string name, string bio)
        {
            this.Name = name;
            this.Bio = bio;
        }

    }
}
