using Microsoft.EntityFrameworkCore;

namespace PaymentAPI.Model
{
    [Keyless]

    public class TransactionDetails : BaseEntity
    {
     
        public int UserId { get; set; }
        public decimal Amount { get; set; }
        public string AmountCurrency { get; set; }
        public DateTime Date { get; set; }
        public string Type { get; set; }

       
    }
}
