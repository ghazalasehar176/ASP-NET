namespace OnlineBookStore.Models
{
    public class CartViewModel
    {
        public int bookId { get; set; }
     
        public string Title { get; set; }
        public int price { get; set; }
        public string imageUrl { get; set; }
        public int Quantity { get; set; }
        public int Total
        {
            get { return Quantity * price; }
        }
    }
}
 