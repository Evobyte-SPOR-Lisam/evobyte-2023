namespace WebApplication1.Models.Security
{
    public class Token
    {
        public string Value { get; set; }

        public DateTime Expiry { get; set; }
    }
}
