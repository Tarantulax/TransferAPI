namespace PaymentAPI.Model
{
    public class Accounts : BaseEntity
    {
        public int UserId { get; set; }
        public string Currency { get; set; }
        public string IbanNumber { get; set; }
        public decimal Balance { get; set; }
        public DateTime UpdatedDate { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
