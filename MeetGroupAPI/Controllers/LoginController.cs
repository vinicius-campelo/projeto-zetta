using MeetGroupAPI.Models;
using MeetGroupAPI.Models.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MeetGroupAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IUsuarioInterface _usuarioInterface;

        public LoginController(IUsuarioInterface usuarioInterface)
        {
            _usuarioInterface = usuarioInterface;
        }

        /// <summary>
        /// Retorna o token de usuario.
        /// </summary>
        ///   /// <remarks>
        /// Exemplo:
        ///
        ///     POST /login
        ///     {
        ///        "Email": "pessoa@email",
        ///        "Senha": "suasenha123"
        ///     }
        ///  Exemplo url: api/Login
        /// </remarks>
        /// <returns>Token para uso na API</returns>
        /// <response code="200">Sucesso na requisição</response>
        /// <response code="201">Retorna item encontrado</response>
        /// <response code="401">Requer autorização ou autenticação de usuário e senha. (Token)</response>
        /// <response code="404">Item não encontrado</response>
        [AllowAnonymous]
        [HttpPost]
        public async Task<object> PostAsync([FromBody] UsuarioModel usuarioModel)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.Values.SelectMany(p => p.Errors));
            }



            try
            {
               
                var usuario = new Usuario()
                {
                    EmailUsuario = usuarioModel.Email,
                    SenhaUsuario = usuarioModel.Senha
                };



                var result = await _usuarioInterface.LoginUsuario(usuario);

                if (result != null)
                {
                    return Ok(result);
                }
                else
                {
                    return NotFound("Usuário ou senha inválidos");
                }
            }
            catch (ArgumentException err)
            {

                return StatusCode((int)HttpStatusCode.InternalServerError, err.Message);
            }
        }
    }
}
