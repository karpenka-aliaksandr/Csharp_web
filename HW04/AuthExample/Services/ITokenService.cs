using AuthExample.DTO;
using AuthExample.Model;

namespace AuthExample.Services;

public interface ITokenService
{
    public string GenerateToken(LoginViewModel loginViewModel);
}