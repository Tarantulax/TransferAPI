namespace PaymentAPI.DTOs
{
    public class TransferRequest
    {
        public string SenderIbanNumber { get; set; }
        public string RecieverIbanNumber { get; set; }
        public decimal Amount { get; set; }
    }
}
