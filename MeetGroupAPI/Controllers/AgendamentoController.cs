using MeetGroupAPI.Models;
using MeetGroupAPI.Models.Interfaces;
using MeetGroupAPI.Models.ModelViews;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using System;
using System.Linq;
using System.Threading.Tasks;
using Wiki.Api.Data.Utils;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MeetGroupAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgendamentoController : ControllerBase
    {

        private readonly ApiDbContext _context;
        private readonly IHttpContextAccessor _accessor;
        private readonly IAgendamentoInterface _agendamentoInterface;

        public AgendamentoController(ApiDbContext context, IAgendamentoInterface agendamentoInterface, IHttpContextAccessor accessor)
        {
            _context = context;
            _accessor = accessor;
            _agendamentoInterface = agendamentoInterface;
        }


        /// <summary>
        /// Lista as reservas cadastradas
        /// </summary>
        /// <returns></returns>
        [Authorize("Bearer")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {

            var result = (await _agendamentoInterface.SelectAllAgendamentoBy())
                .Select(p => p.ToAgendaModel());
                
            return Ok(result);
        }


        /// <summary>
        /// Agendar Reserva de sala
        /// </summary>
        /// <param name="reservaModel"></param>
        /// <returns></returns>
        [Authorize("Bearer")]
        [HttpPost]
        public ActionResult<object> Post([FromBody] ReservaModel reservaModel)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.Values.SelectMany(p => p.Errors));
            }

            _accessor.HttpContext.Request.Headers.TryGetValue("Authorization", out StringValues token);

            var result = _agendamentoInterface.SelectRegragendamentoBy(token, reservaModel);

            return Ok(new { result });
        }
    }
}
