using MeetGroupAPI.Models;
using MeetGroupAPI.Models.Interfaces;
using MeetGroupAPI.Models.ModelViews;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using System;
using System.Linq;
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
        public object GetAll()
        {

           
            var query = (from u in _context.Usuario
                         join r in _context.Reserva on u.IdUsuario equals r.IdUsuarioReserva
                        select new {
                          reserva = r.IdReserva,
                          usuario =   u.NomeUsuario,
                          dataInicioReserva =  r.DataInicioReserva,
                          dataFimReserva = r.DataFimReserva,
                          horaInicio = r.HoraInicioReserva,
                          horaFim = r.HoraFimReserva,
                          quantidadePessoas = r.QuantidadePessoasReserva,
                          computador = r.ComputadorReserva,
                          tv = r.TvReserva,
                          internet = r.InternetReserva,
                          webcam = r.WebcamReserva,
                        });
            
            return Ok(query);
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

            var mensagem = "";
            var count = 0;
            _accessor.HttpContext.Request.Headers.TryGetValue("Authorization", out StringValues token);
            var payload = LoadToken.ViewToken(token).First(p => p.Type == "nameid").Value;

            var h1 = TimeSpan.Parse(reservaModel.Hora_Inicio);
            var h2 = TimeSpan.Parse(reservaModel.Hora_Fim);
            var totalDias = (reservaModel.Data_Inicio.Subtract(reservaModel.Data_Fim)).Days;


            if (reservaModel.Data_Inicio.DayOfWeek == DayOfWeek.Saturday || reservaModel.Data_Inicio.DayOfWeek == DayOfWeek.Sunday ||
                    reservaModel.Data_Fim.DayOfWeek == DayOfWeek.Saturday || reservaModel.Data_Fim.DayOfWeek == DayOfWeek.Sunday)
            {
                return Ok(new { message = "As reuniões devem ser agendadas apenas para os dias úteis!" });
            }else if (reservaModel.Data_Inicio == DateTime.Today)
            {
                return Ok(new { message = "As reuniões devem ser agendadas com no mínimo um dia de antecedência!" });
            }else if (totalDias > 40)
            {
                return Ok(new { message = "As reuniões devem ser agendadas com no máximo 40 dias de antecedência!" });
            }else if (h1.Add(h2).Hours > 8)
            {
                return Ok(new { message = "Reuniões não podem durar mais que 8 horas!" });
            }



            var result = _context.Reserva.Where(p => p.DataInicioReserva <= reservaModel.Data_Inicio &&
            p.DataFimReserva >= reservaModel.Data_Fim).ToList();

            if (result.Count > 0)
            {
                foreach (var item in result)
                {
                    if (TimeSpan.Parse(item.HoraInicioReserva) <= h1 && TimeSpan.Parse(item.HoraFimReserva) >= h2)
                    {
                        count++;
                        var order = _context.Sala.Where(p => p.QuantidadeLugaresSala == reservaModel.Quantidade_Pessoas && p.StatusSala == false)
                          .Select((c) => new
                          {
                              c.NumeroSala,
                              c.QuantidadeLugaresSala,
                              c.QuantidadeEquipamentoSala
                          }).ToList().Take(3);

                        mensagem = "Não se pode agendar reuniões conflitantes de lugar e hora! escolha outra data/hora";
                        return Ok(new { mensagem, order });
                    }
                }
            }
            else
            {
                var reserva = new Reserva
                {
                    
                    DataInicioReserva = reservaModel.Data_Inicio,
                    DataFimReserva = reservaModel.Data_Fim,
                    HoraInicioReserva = reservaModel.Hora_Inicio,
                    HoraFimReserva = reservaModel.Hora_Fim,
                    QuantidadePessoasReserva = reservaModel.Quantidade_Pessoas,
                    ComputadorReserva = reservaModel.Computador,
                    TvReserva = reservaModel.Tv,
                    InternetReserva = reservaModel.Internet,
                    WebcamReserva = reservaModel.Webcam,
                    IdUsuarioReserva = int.Parse(payload),
                    StatusReserva = true
                };
                count++;
                _agendamentoInterface.InsertAsync(reserva);
                mensagem = "Reserva cadastrada!";
            }


            if(count == 0)
            {
                var reserva = new Reserva
                {
                    DataInicioReserva = reservaModel.Data_Inicio,
                    DataFimReserva = reservaModel.Data_Fim,
                    HoraInicioReserva = reservaModel.Hora_Inicio,
                    HoraFimReserva = reservaModel.Hora_Fim,
                    QuantidadePessoasReserva = reservaModel.Quantidade_Pessoas,
                    ComputadorReserva = reservaModel.Computador,
                    TvReserva = reservaModel.Tv,
                    InternetReserva = reservaModel.Internet,
                    WebcamReserva = reservaModel.Webcam,
                    IdUsuarioReserva = int.Parse(payload),
                    StatusReserva = true
                };

                _agendamentoInterface.InsertAsync(reserva);
                mensagem = "Reserva cadastrada!";
            }

            return Ok(new { mensagem });
        }
    }
}
