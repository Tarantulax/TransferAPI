namespace PaymentAPI.Model
{
    public class Users:BaseEntity
    {
        public string Name { get; set; }
        public string Surname { get; set; }

        public string Address { get; set; }
        public string Password { get; set; }
        public string TC { get; set; }

        public DateTime UpdatedDate { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
