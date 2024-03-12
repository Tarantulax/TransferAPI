namespace PaymentAPI.Model
{
    public class Transactions : BaseEntity
    {
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public decimal Amount { get; set; }
        public string AmountCurrency { get; set; }
        public DateTime Date { get; set; }

    }
}
