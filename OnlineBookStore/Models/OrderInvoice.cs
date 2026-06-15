namespace OnlineBookStore.Models
{
    public class OrderInvoice
    {
        public int OrderId { get; set; }
        public string Email { get; set; }
        public DateTime OrderDate { get; set; }
        public List<OrderItemViewModel> Items {get; set;}
        public int TotalAmount { get; set; }
    }
}
