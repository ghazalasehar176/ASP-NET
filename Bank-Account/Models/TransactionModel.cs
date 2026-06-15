namespace Bank_Account.Models
{
    public class TransactionModel
    {
        public int Id { get; set; }
        public DateTime? TranDate { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public string TranType { get; set; }
    }
}
