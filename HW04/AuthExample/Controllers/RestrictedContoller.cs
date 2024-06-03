using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AuthExample.DTO;
using AuthExample.Model;

namespace AuthExample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RestrictedContoller : ControllerBase
    {
        [HttpGet]
        [Route("Admins")]
        [Authorize(Roles = "Admin")]
        public IActionResult AdminEndPoint()
        {
            var currentUser = GetCurrentUser();
            return Ok($"Hello, {currentUser}! You are an admin.");
        }

        [HttpGet]
        [Route("Users")]
        [Authorize(Roles = "Admin, User")]
        public IActionResult UserEndPoint()
        {
            var currentUser = GetCurrentUser();
            return Ok($"Hello, {currentUser}! You are an user.");
        }

        [HttpGet]
        [Route("Guests")]
        [Authorize(Roles = "Admin, User, Guest")]
        public IActionResult GuestEndPoint()
        {
            var currentUser = GetCurrentUser();
            return Ok($"Hello, {currentUser}! You are an guest.");
        }

        private User GetCurrentUser()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                var userClaims = identity.Claims;

                // Получаем значение Role из утверждений
                var roleValue = userClaims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

                Role role;
                if (Enum.TryParse(roleValue, out role))
                {
                    return new User
                    {
                        Email = userClaims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value,
                        Role = role
                    };
                }
            }
            return null;
        }
    }
}
