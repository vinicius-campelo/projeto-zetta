using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;

namespace Wiki.Api.Data.Utils
{
    public class LoadToken
    {
        /// <summary>
        /// Retorna somente o token
        /// </summary>
        /// <param name="token"></param>
        /// <returns>token</returns>
        public static IEnumerable<Claim> ViewToken(string token)
        {
            var stream = token.Replace("Bearer ", "");
            var handler = new JwtSecurityTokenHandler();
            var tokenS = handler.ReadToken(stream) as JwtSecurityToken;
            return tokenS.Claims.ToList();
        }
    }
}
