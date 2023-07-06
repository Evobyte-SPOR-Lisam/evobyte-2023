namespace WebApplication1.Framework
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public class RequireRoleAttribute : Attribute, Microsoft.AspNetCore.Mvc.Filters.IFilterMetadata
    {
        public string Role { get; set; }
    }
}