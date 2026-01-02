using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using P10_WebApi.Interfaces;




public class TokenService : ITokenService  

{
    private readonly string _secretKey;     
    public TokenService( string SecretKey)
    {
        _secretKey = SecretKey;
    }

    public string CreateToken(Guid userId, string email, string username, int timeInDays)
    {
        var tokenHandler = new JwtSecurityTokenHandler();   

        var key = Encoding.ASCII.GetBytes(_secretKey);  

        var payload = new SecurityTokenDescriptor      
        {
            Subject = new ClaimsIdentity(
            [
                new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
                new Claim(ClaimTypes.Email, email),
                new Claim(ClaimTypes.Name, username)
            ]),

            Expires = DateTime.UtcNow.AddDays(timeInDays),

            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(payload);   
        return tokenHandler.WriteToken(token);        
    }

    public string CreateToken(string userId, string email, string username, int timeInDays)
    {
          var tokenHandler = new JwtSecurityTokenHandler();   

        var key = Encoding.ASCII.GetBytes(_secretKey); 

        var payload = new SecurityTokenDescriptor      
        {
            Subject = new ClaimsIdentity(
            [
                new Claim(ClaimTypes.NameIdentifier, userId),
                new Claim(ClaimTypes.Email, email),
                new Claim(ClaimTypes.Name, username)
            ]),

            Expires = DateTime.UtcNow.AddDays(timeInDays),

            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(payload);    
        return tokenHandler.WriteToken(token);        
    }




    public string VerifyTokenAndGetId(string token)
    {
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(_secretKey);


            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                IssuerSigningKey = new SymmetricSecurityKey(key)
            };

            var validatToken = tokenHandler.ValidateToken(token, validationParameters, out var validatedToken);


            var userId = validatToken.FindFirst(ClaimTypes.NameIdentifier);

            if (userId != null)
            {
                return userId.Value;
            }
            else
            {
                throw new Exception("User ID not found in token.");
            }
        }
        catch (SecurityTokenExpiredException)
        {
            throw new Exception("Token has expired.");
        }
        catch (Exception ex)
        {
            throw new Exception("Token validation failed: " + ex.Message);
        }

    }


}

