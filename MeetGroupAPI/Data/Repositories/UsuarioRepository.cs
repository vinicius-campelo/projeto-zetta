using MeetGroupAPI.Models;
using MeetGroupAPI.Models.Interfaces;
using MeetGroupAPI.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Wiki.Api.Data.Utils;

namespace MeetGroupAPI.Data.Repositories
{
    public class UsuarioRepository : Repository<Usuario>, IUsuarioInterface
    {

        private SigningHSConfigurations _signingConfiguration;
        private TokenConfigurations _tokenConfigurations;

        public UsuarioRepository(ApiDbContext context,
            SigningHSConfigurations signingConfiguration,
            TokenConfigurations tokenConfigurations) : base(context)
        {
            _signingConfiguration = signingConfiguration;
            _tokenConfigurations = tokenConfigurations;
        }


        /// <summary>
        /// Traz dados de usuario (token)
        /// </summary>
        /// <param name="user"></param>
        /// <returns>token</returns>
        public async Task<object> LoginUsuario(Usuario user)
        {
            var usuario = new Usuario();

            if (user != null && !string.IsNullOrWhiteSpace(user.EmailUsuario)
                && !string.IsNullOrWhiteSpace(user.SenhaUsuario))
            {
                usuario = await _dataset
                   .FirstOrDefaultAsync(p => p.EmailUsuario == user.EmailUsuario && p.SenhaUsuario == Criptografia.Encrypt(user.SenhaUsuario));

                if (usuario == null)
                {
                    return new
                    {
                        authenticated = false,
                        message = "Falha na autenticação"
                    };
                }
                else
                {
                    var identity = new ClaimsIdentity(
                        new GenericIdentity(user.EmailUsuario),
                        new[]
                        {
                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                            new Claim(JwtRegisteredClaimNames.Email, user.EmailUsuario),
                            new Claim(JwtRegisteredClaimNames.FamilyName, usuario.NomeUsuario),
                            new Claim(JwtRegisteredClaimNames.NameId, usuario.IdUsuario.ToString()),
                        }
                    );

                    DateTime createDate = DateTime.Now;
                    DateTime expirationDate = createDate + TimeSpan.FromMinutes(_tokenConfigurations.Minutes);

                    var handler = new JwtSecurityTokenHandler();
                    string token = await CreateToken(identity, createDate, expirationDate, handler);
                    return SuccessObject(createDate, expirationDate, token, usuario);

                }
            }
            else
            {
                return new
                {
                    authenticated = false,
                    message = "Falha na autenticação"
                };
            }
        }


        /// <summary>
        /// Cria o token de usuario
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="createDate"></param>
        /// <param name="expirationDate"></param>
        /// <param name="handler"></param>
        /// <returns></returns>
        private Task<string> CreateToken(ClaimsIdentity identity, DateTime createDate, DateTime expirationDate, JwtSecurityTokenHandler handler)
        {
            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _tokenConfigurations.Issuer,
                Audience = _tokenConfigurations.Audience,
                SigningCredentials = _signingConfiguration.SigningCredentials,
                Subject = identity,
                NotBefore = createDate,
                Expires = expirationDate,
            });

            return Task.FromResult(handler.WriteToken(securityToken));

        }


        /// <summary>
        /// Retorna as informações de Header do usuario logado.
        /// </summary>
        /// <param name="createDate"></param>
        /// <param name="expirationDate"></param>
        /// <param name="token"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        private object SuccessObject(DateTime createDate, DateTime expirationDate, string token, Usuario user)
        {
            return new
            {
                authenticated = true,
                created = createDate.ToString("yyyy-MM-dd HH:mm:ss"),
                expiration = expirationDate.ToString("yyyy-MM-dd HH:mm:ss"),
                acessToken = token,
                userName = user.NomeUsuario,
                message = "Usuario logado com sucesso!"
            };
        }
    }
}
