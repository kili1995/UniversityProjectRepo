namespace University.Api.Helpers
{
    using Microsoft.IdentityModel.Tokens;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using University.Api.Models;
    public static class JWTHelpers
    {
        public const string ADMIN_ROLE = "Jorge M.";
        public const string ADMINISTRATOR_ROLE_CLAIM = "Administrator";
        public const string USER_ROLE_CLAIM = "Generic_User";
        public static IEnumerable<Claim> GetClaims(this UserToken userAccount, Guid Id)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim("Id", userAccount.Id.ToString()),
                new Claim(ClaimTypes.Name, userAccount.UserName),
                new Claim(ClaimTypes.Email, userAccount.EmailId),
                new Claim(ClaimTypes.NameIdentifier, userAccount.Id.ToString()),
                new Claim(ClaimTypes.Expiration, DateTime.UtcNow.AddDays(1).ToString("dd-MM-yyyy HH-mm-ss tt"))
            }; 

            if(userAccount.UserName == ADMIN_ROLE)
            {
                claims.Add(new Claim(ClaimTypes.Role, ADMINISTRATOR_ROLE_CLAIM));
            }
            else
            {
                claims.Add(new Claim(ClaimTypes.Role, USER_ROLE_CLAIM));
            }
            return claims;
        }

        public static IEnumerable<Claim> GetClaims(this UserToken userAccount, out Guid Id)
        {
            Id = Guid.NewGuid();
            return GetClaims(userAccount, Id);
        }

        public static UserToken GetTokenKey(UserToken model, JWTSettings settings)
        {
            try
            {
                if(model == null)
                {
                    throw new ArgumentNullException(nameof(model));
                }
                var userToken = new UserToken();

                //Obtain secrey key
                if (string.IsNullOrWhiteSpace(settings.IssuerSigningKey))
                {
                    throw new NullReferenceException(nameof(settings.IssuerSigningKey));
                }
                var key = System.Text.Encoding.ASCII.GetBytes(settings.IssuerSigningKey);

                Guid Id;

                DateTime expirationTime = DateTime.UtcNow.AddDays(1);

                //Validity of token
                userToken.Validity = expirationTime.TimeOfDay;

                //Generate JWT
                var jwToken = new JwtSecurityToken(
                    issuer: settings.ValidIssuer,
                    audience: settings.ValidAudience,
                    claims: GetClaims(model, out Id),
                    notBefore: new DateTimeOffset(DateTime.Now).DateTime,
                    expires: new DateTimeOffset(expirationTime).DateTime,
                    signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
                );

                userToken.Token = new JwtSecurityTokenHandler().WriteToken(jwToken);
                userToken.UserName = model.UserName;
                userToken.Id = model.Id;
                userToken.GuidId = Id;
                userToken.ExpiredTime = expirationTime;
                return userToken;
            }
            catch (Exception e)
            {
                throw new Exception($"Error generando el JWT, {e.Message}.");
            }
        }
    }
}
