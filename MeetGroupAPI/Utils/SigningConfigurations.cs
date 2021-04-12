using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MeetGroupAPI.Utils
{
    public class SigningConfigurations
    {
        public SecurityKey Key { get; set; }
        public SigningCredentials SigningCredentials { get; set; }

        public SigningConfigurations()
        {
            using (var provider = new RSACryptoServiceProvider(2048))
            {
                Key = new RsaSecurityKey(provider.ExportParameters(true));
            }

            SigningCredentials = new SigningCredentials(Key, SecurityAlgorithms.RsaSha256Signature);
        }
    }


    public class SigningHSConfigurations
    {
        public SecurityKey Key { get; set; }
        public SigningCredentials SigningCredentials { get; set; }

        public SigningHSConfigurations()
        {
            string chave = "2273ed3b1d1be66cf862607c52513c4d";
            Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(chave));

            SigningCredentials = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256Signature);

        }
    }
}
