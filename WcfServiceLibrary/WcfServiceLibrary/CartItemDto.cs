using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WcfServiceLibrary
{
    public class CartItemDto
    {
        public string BookName { get; set; }
        public string AuthorName { get; set; }
        public int Price { get; set; }
        public string RemainingTime { get; set; }
        public int CartID { get; set; }
    }
}
