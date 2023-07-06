namespace WebApplication1.Framework.Identity
{
    public class Identity
    {
        public Guid ID { get; set; }

        public Guid? OriginatorID { get; set; }
    }

    public static class IdentityConstants
    {
        public const string IdentityKey = "WebApplication1_IdentityClaim_Key";
    }
}