using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Google.Apis;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using CoffeeClub.Domain.Enumerations;
using CoffeeClub.Domain.Repositories;

namespace CoffeeClub.Core.Api.CustomConfiguration;

public class GoogleTokenValidator : ISecurityTokenValidator
{
    private readonly IEnumerable<string> _workerEmails;
    private readonly JwtSecurityTokenHandler _tokenHandler;

    private readonly IUserRepository _userRepository;

    public GoogleTokenValidator(IEnumerable<string> workerEmails, IUserRepository userRepository)
    {
        _workerEmails = workerEmails;
        _tokenHandler = new JwtSecurityTokenHandler();
        _userRepository = userRepository;
    }

    public bool CanValidateToken => true;

    public int MaximumTokenSizeInBytes { get; set; } = TokenValidationParameters.DefaultMaximumTokenSizeInBytes;

    public bool CanReadToken(string securityToken)
    {
        return _tokenHandler.CanReadToken(securityToken);
    }

    public ClaimsPrincipal ValidateToken(string securityToken, TokenValidationParameters validationParameters, out SecurityToken validatedToken)
    {

        // TODO cache
        validatedToken = null;
        var payload = GoogleJsonWebSignature.ValidateAsync(securityToken, new GoogleJsonWebSignature.ValidationSettings()).Result; // here is where I delegate to Google to validate
        var user = _userRepository.GetOrCreateAsync(payload.Subject, AuthProvider.Google).Result;
        var claims = new List<Claim>
                {
                    new Claim("id", user.Id.ToString()),
                    new Claim("sub", payload.Subject),
                    new Claim("authProvider", ((int)AuthProvider.Google).ToString()),
                    new Claim(ClaimTypes.Role, _workerEmails.Contains(payload.Email) ? "CoffeeClubWorker" : "CoffeeClubCustomer")
                };
        try
        {
            var principle = new ClaimsPrincipal();
            principle.AddIdentity(new ClaimsIdentity(claims, JwtBearerDefaults.AuthenticationScheme));
            return principle;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}