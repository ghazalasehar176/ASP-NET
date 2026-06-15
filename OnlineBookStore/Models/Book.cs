
//using Microsoft.Data.SqlClient;
using System.Data.SqlClient;

namespace OnlineBookStore.Models
{
    public class Book
    {
        public int bookId { get; set; }
        public string title { get; set; }
        public string author { get; set; }
        public int price { get; set; }
        public string category { get; set; }
        public int stock { get; set; }
        public string imageUrl { get; set; }
    }
}
