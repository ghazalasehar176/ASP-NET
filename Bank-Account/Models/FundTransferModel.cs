using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;

namespace Bank_Account.Models
{
    public class FundTransferModel
    {
        public string? FromAccount { get; set; }
        public string? ToAccount { get; set; }
        public decimal Amount { get; set; }
        public DateTime TransactionDate { get; set; }
        public string TransactionPassword { get; set; }

    }
}



