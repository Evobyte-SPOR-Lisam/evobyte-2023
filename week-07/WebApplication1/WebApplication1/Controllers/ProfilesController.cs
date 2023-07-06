using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Configuration;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApplication1.Data;
using WebApplication1.Models;
using WebApplication1.Models.Security;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SecurityController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;

        private readonly IConfiguration configuration;

        private readonly WebApplication1Context webApplication1Context;

        public SecurityController(
            WebApplication1Context webApplication1Context,
            IConfiguration configuration,
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager)
        {
            this.webApplication1Context = webApplication1Context;
            this.configuration = configuration;

            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<Token> Register(RegisterModel registerModel)
        {
            IdentityUser identityUser = new IdentityUser() { Email = registerModel.Email, UserName = registerModel.Email };

            IdentityResult result = await userManager.CreateAsync(identityUser, registerModel.Password);

            IdentityUser user = await userManager.FindByEmailAsync(registerModel.Email);

            Profile profile = new Profile()
            {
                ID = Guid.Parse(user.Id),
                Name = $"{registerModel.FirstName} {registerModel.LastName}",
            };

            webApplication1Context.Profile.Add(profile);

            webApplication1Context.SaveChanges();

            return new Token();
        }


        [HttpPost]
        [AllowAnonymous]
        public async Task<Token> Login(LoginModel loginModel)
        {
            IdentityUser identityUser = await userManager.FindByEmailAsync(loginModel.Email);

            Microsoft.AspNetCore.Identity.SignInResult result = await signInManager.CheckPasswordSignInAsync(identityUser, loginModel.Password, true);

            return GenerateToken(identityUser);
        }


        private Token GenerateToken(IdentityUser user)
        {
            List<Claim> claims = new List<Claim>
            {   
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            };

            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.configuration.GetValue<string>("Authentication:Secret")));

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: configuration.GetValue<string>("Authentication:Issuer"),
                audience: configuration.GetValue<string>("Authentication:Issuer"),
                claims: claims,
                expires: DateTime.Now.AddDays(this.configuration.GetValue<int>("Authentication:ExpiryTimeInDays")),
                signingCredentials: new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256)
            );

            return new Token()
            {
                Value = new JwtSecurityTokenHandler().WriteToken(token),
                Expiry = token.ValidTo,
            };
        }
    }
}
