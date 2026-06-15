namespace OnlineBookStore.Models
{
    public class Order
    {
        public int OrderId { get; set; }

        public string names { get; set; }
        public string Email { get; set; }
        public int TotalAmount { get; set; }
        public DateTime OrderDate { get; set; }
    }
}