﻿using MeetGroupAPI.Models;
using MeetGroupAPI.Models.Interfaces;
using MeetGroupAPI.Models.ModelViews;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
            _accessor.HttpContext.Request.Headers.TryGetValue("Authorization", out StringValues token);
            var payload = LoadToken.ViewToken(token).First(p => p.Type == "nameid").Value;

            var h1 = TimeSpan.Parse(reservaModel.Hora_Inicio);
            var h2 = TimeSpan.Parse(reservaModel.Hora_Fim);

            var result = _context.Reserva.Where(p => p.DataInicioReserva <= reservaModel.Data_Inicio &&
            p.DataFimReserva >= reservaModel.Data_Fim).ToList();

            if (result.Count > 0)
            {
               var order = _context.Sala.Where(p => p.QuantidadeLugaresSala == reservaModel.Quantidade_Pessoas && p.StatusSala == false)
                   .Select((c) => new
                   {
                      c.NumeroSala,
                      c.QuantidadeLugaresSala,
                      c.QuantidadeEquipamentoSala
                   }).ToList();

                mensagem = "Data cadastrada escolha outra!";
                return Ok(new { mensagem, order });
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
                    WebcamReserva = reservaModel.Webcam
                };

                _agendamentoInterface.InsertAsync(reserva);
                mensagem = "Reserva cadastrada!";
            }

            return Ok(new { mensagem });
        }
    }
}
