using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Google.Apis;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using CoffeeClub.Domain.Enumerations;

namespace CoffeeClub.Core.Api.CustomConfiguration;

public class GoogleTokenValidator : ISecurityTokenValidator
{
    private readonly IEnumerable<string> _workerEmails;
    private readonly JwtSecurityTokenHandler _tokenHandler;

    public GoogleTokenValidator(IEnumerable<string> workerEmails)
    {
        _workerEmails = workerEmails;
        _tokenHandler = new JwtSecurityTokenHandler();
    }

    public bool CanValidateToken => true;

    public int MaximumTokenSizeInBytes { get; set; } = TokenValidationParameters.DefaultMaximumTokenSizeInBytes;

    public bool CanReadToken(string securityToken)
    {
        return _tokenHandler.CanReadToken(securityToken);
    }

    public ClaimsPrincipal ValidateToken(string securityToken, TokenValidationParameters validationParameters, out SecurityToken validatedToken)
    {
        validatedToken = null;
        var payload = GoogleJsonWebSignature.ValidateAsync(securityToken, new GoogleJsonWebSignature.ValidationSettings()).Result; // here is where I delegate to Google to validate

        var claims = new List<Claim>
                {
                    // new Claim(ClaimTypes.NameIdentifier, payload.Name),
                    // new Claim(ClaimTypes.Name, payload.Name),
                    // new Claim(ClaimTypes.Email, payload.Email),
                    // new Claim("picture", payload.Picture),
                    // new Claim("locale", payload.Locale),
                    // new Claim("family_name", payload.FamilyName),
                    // new Claim("given_name", payload.GivenName),
                    // new Claim("iat", payload.IssuedAtTimeSeconds.ToString()),
                    // new Claim("exp", payload.ExpirationTimeSeconds.ToString()),
                    // new Claim("iss", payload.Issuer),
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