
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Job.Model;
using Microsoft.IdentityModel.Tokens;

public class Token
{

    public string GenerateToken(UserModel userModel)
    {
        string issuer = "Job";
        string signKey = "ababababab@cdcdcdcdcd@efefefefef"; // 更新後的密鑰，長度為 32 字節或更多

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signKey));
        var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Issuer = issuer,
            Subject = new ClaimsIdentity(new[]
            {
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim("account", userModel.account.ToString()),
            new Claim("role", userModel.role.ToString()),

        }),
            Expires = DateTime.Now.AddMinutes(60),
            SigningCredentials = signingCredentials
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var securityToken = tokenHandler.CreateToken(tokenDescriptor);
        var serializeToken = tokenHandler.WriteToken(securityToken);
        return serializeToken;
    }
}