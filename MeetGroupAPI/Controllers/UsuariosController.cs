using MeetGroupAPI.Models;
using MeetGroupAPI.Models.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wiki.Api.Data.Utils;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MeetGroupAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly ApiDbContext _context;
        private readonly IUsuarioInterface _usuarioInterface;

        public UsuariosController(ApiDbContext context, IUsuarioInterface usuarioInterface)
        {
            _context = context;
            _usuarioInterface = usuarioInterface;
        }



        /// <summary>
        /// Lista os usuarios ja cadastrados!
        /// </summary>
        /// <returns></returns>
        [Authorize("Bearer")]
        [HttpGet]
        public object GetAll()
        {

            var result = _context.Usuario.Select((c) => new
            {
              c.IdUsuario,
              c.NomeUsuario,
              c.TelefoneUsuario,
              c.EmailUsuario
            }).ToList();

            return Ok(result);
            
        }


        /// <summary>
        /// Cadastra usuario.
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        [Authorize("Bearer")]
        [HttpPost]
        public ActionResult<object> Post([FromBody] UsuarioCadastroModel usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.Values.SelectMany(p => p.Errors));
            }

            var mensagem = "";
            var result = _context.Usuario.FirstOrDefault(p => p.EmailUsuario == usuario.Email);

            if (result == null)
            {
                var user = new Usuario
                {
                    NomeUsuario = usuario.Nome,
                    TelefoneUsuario = usuario.Telefone,
                    EmailUsuario = usuario.Email,
                    SenhaUsuario = Criptografia.Encrypt(usuario.Senha),
                    StatusUsuario = true
                };


                _usuarioInterface.InsertAsync(user);
                mensagem = "Usuario cadastrado com sucesso!";
            }
            else 
            {
                mensagem = "Usuario já cadastrado!";
            }

            return Ok( new { mensagem });
        }
    }
}
