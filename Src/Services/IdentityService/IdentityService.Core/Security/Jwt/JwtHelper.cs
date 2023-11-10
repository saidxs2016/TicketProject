using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;


namespace IdentityService.Core.Security.Jwt;

public class JwtHelper : IJwtHelper
{

    private readonly ILogger<JwtHelper> _logger;
    public readonly IConfiguration _configuration;
    private readonly JwtSetting _tokenOptions;
    private DateTime _accessTokenExpiration;

    public JwtHelper(ILogger<JwtHelper> logger, IOptions<JwtSetting> tokenOptions)
    {
        _logger = logger;
        _tokenOptions = tokenOptions.Value;

    }
    /// <summary>
    /// claims_options tipi dynamic ör: new {FullName = "SAİD YUNUS", UserName="saidxs2016"}
    /// bir token oluşturulması için gönderilmesi gereken parametereler FullName:Name+Surname, UserName
    /// 
    /// </summary>
    /// <param name="claims_options">Bu bir dynamic tip </param>
    /// <returns></returns>
    public AccessTokenHelper CreateToken(dynamic claims_options, string expirationDate = null, string addExpirationDate = null)
    {
        var minutes = DateIntervalHelpers.GetInternalAsMinutes(expirationDate);
        var addMinutes = DateIntervalHelpers.GetInternalAsMinutes(addExpirationDate);
        if (minutes == 0)
            _accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOptions.AccessTokenExpiration + addMinutes);
        else
            _accessTokenExpiration = DateTime.Now.AddMinutes(minutes + addMinutes);


        AccessTokenHelper accessToken = new();
        try
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenOptions.SecurityKey));
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
            var jwt = CreateJwtSecurityToken(claims_options, signingCredentials);
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var token = jwtSecurityTokenHandler.WriteToken(jwt);

            accessToken.Token = token;
            accessToken.Expiration = _accessTokenExpiration;
            accessToken.ReTokenExpiration = _accessTokenExpiration.AddMinutes(30);
            accessToken.ReToken = CreateRefreshToken();
        }
        catch (Exception)
        {
            throw;
        }
        return accessToken;
    }
    private string CreateRefreshToken()
    {
        byte[] number = new byte[32];
        using RandomNumberGenerator random = RandomNumberGenerator.Create();
        random.GetBytes(number);
        return Convert.ToBase64String(number);

    }
    private JwtSecurityToken CreateJwtSecurityToken(dynamic claims_options, SigningCredentials signingCredentials)
    {
        var jwt = new JwtSecurityToken(
            issuer: _tokenOptions.Issuer,
            audience: _tokenOptions.Audience,
            expires: _accessTokenExpiration,
            notBefore: DateTime.Now,
            claims: SetClaims(claims_options),
            signingCredentials: signingCredentials
            );

        return jwt;
    }
    private IEnumerable<Claim> SetClaims(dynamic claims_options)
    {
        var Name = claims_options.GetType().GetProperty(ClaimHelper.FullName).GetValue(claims_options);
        var UserName = claims_options.GetType().GetProperty(ClaimHelper.UserName).GetValue(claims_options);       

        List<Claim> claims = new()
        {
            new Claim(ClaimHelper.FullName, Name),
            new Claim(ClaimHelper.UserName, UserName)
        };
        return claims;
    }



    /// <summary>
    /// (FullName, UserName)
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    public (string, string) ValidateToken(string token)
    {
        try
        {

            var tokenHandler = new JwtSecurityTokenHandler();


            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_tokenOptions.SecurityKey)),
                ValidateIssuer = false,
                ValidateAudience = false,

                ValidateLifetime = true,
                RequireExpirationTime = true,
                ValidAudience = _tokenOptions.Audience,
                ValidIssuer = _tokenOptions.Issuer,
                LifetimeValidator = (notBefore, expires, securityToken, validationParameters) => expires != null && expires > DateTime.UtcNow,
                ClockSkew = TimeSpan.Zero,
            }, out SecurityToken validatedToken);


            var jwtToken = (JwtSecurityToken)validatedToken;
            var fullName = jwtToken.Claims.FirstOrDefault(i => i.Type == ClaimHelper.FullName)?.Value;
            var userName = jwtToken.Claims.FirstOrDefault(i => i.Type == ClaimHelper.UserName)?.Value;

            return (fullName, userName);
        }
        catch (Exception) { }


        return (null, null);


    }

}
