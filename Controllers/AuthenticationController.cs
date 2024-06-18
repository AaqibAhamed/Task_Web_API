using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace CityInfo.API.Controllers
{
    [Route("api/v{version:apiVersion}/Authentication")]
    [ApiController]
    [ApiVersion(1)]
    public class AuthenticationController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public AuthenticationController(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }
        public class AuthenticationRequestBody()
        {
            public string? UserName { get; set; }

            public string? Password { get; set; }
        }

        private class UserInfo
        {
            public int UserId { get; set; }

            public string UserName { get; set; }

            public string FirstName { get; set; }

            public string LastName { get; set; }


            public UserInfo(int userId, string userName, string firstName, string lastName)
            {
                UserId = userId;
                UserName = userName;
                FirstName = firstName;
                LastName = lastName;

            }

        }

        [HttpPost("authenticate")]
        public ActionResult<string> Authenticate(AuthenticationRequestBody authenticationRequestBody)
        {
            //Validate User
            var valideUser = ValidateUserCredentials(authenticationRequestBody.UserName, authenticationRequestBody.Password);

            if (valideUser == null)
            {
                return Unauthorized();
            }

            // create Token
            var securityKey = new SymmetricSecurityKey(Convert.FromBase64String(_configuration["Authentication:SecretForKey"]?? " "));

            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            // Claim the token
            var claimsForToken = new List<Claim>
            {
                new Claim("id", valideUser.UserId.ToString()),
                new Claim("user_name", valideUser.UserName.ToString()),
                new Claim("given_name", valideUser.FirstName),
                new Claim("family_name", valideUser.LastName)
            };

            var jwtSecurityToken = new JwtSecurityToken(
                _configuration["Authentication:Issuer"],
                _configuration["Authentication:Audience"],
                claimsForToken,
                DateTime.UtcNow,
                DateTime.UtcNow.AddHours(2),// Token Valid period
                signingCredentials);

            var tokenToReturn = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

            //var tokenObj = new
            //{
            //    tokenToReturn = tokenToReturn,
            //};


            return Ok(tokenToReturn);

        }


        private UserInfo ValidateUserCredentials(string? userName, string? password)
        {
            // we don't have a user DB or table.  If we have, check the passed-through
            // username/password against what's stored in the database.
            // For this test purposes, I assume the credentials are valid

            return new UserInfo(
                1,
                userName ?? "",
                "Aaqib",
                "Wiki"
            );
        }


    }
}
