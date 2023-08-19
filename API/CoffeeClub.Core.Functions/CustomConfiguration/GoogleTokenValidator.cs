using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using CoffeeClub.Domain.Enumerations;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace CoffeeClub_Core_Functions.CustomConfiguration;

public class GoogleTokenValidator : ISecurityTokenValidator
{
    private readonly JwtSecurityTokenHandler _tokenHandler;
    private readonly string[] _workerEmails = new[] { "garybmcmonagle@gmail.com" };

    public GoogleTokenValidator()
    {
        _tokenHandler = new JwtSecurityTokenHandler();
    }

    public bool CanValidateToken => true;

    public int MaximumTokenSizeInBytes { get; set; } = TokenValidationParameters.DefaultMaximumTokenSizeInBytes;

    public bool CanReadToken(string securityToken) => _tokenHandler.CanReadToken(securityToken);

    public ClaimsPrincipal ValidateToken(string securityToken, TokenValidationParameters validationParameters, out SecurityToken validatedToken)
    {
        validatedToken = null;
        var payload = GoogleJsonWebSignature.ValidateAsync(securityToken, new GoogleJsonWebSignature.ValidationSettings()).GetAwaiter().GetResult(); // here is where I delegate to Google to validate
        var claims = new List<Claim>
                {
                    // new Claim("id", user.Id.ToString()),
                    new Claim(CustomClaimTypes.ExternalIdentityId, payload.Subject),
                    new Claim(CustomClaimTypes.AuthProvider, ((int)AuthProvider.Google).ToString()),
                    new Claim(ClaimTypes.Role, _workerEmails.Contains(payload.Email) ? "CoffeeClubWorker" : "CoffeeClubCustomer")
                };
        try
        {
            var principle = new ClaimsPrincipal();
            principle.AddIdentity(new ClaimsIdentity(claims, JwtBearerDefaults.AuthenticationScheme));
            return principle;
        }
        catch
        {
            var principle = new ClaimsPrincipal();
            return principle;

            throw;
        }
    }
}
