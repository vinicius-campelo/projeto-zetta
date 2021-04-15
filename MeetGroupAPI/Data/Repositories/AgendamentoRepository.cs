using MeetGroupAPI.Models;
using MeetGroupAPI.Models.Interfaces;
using MeetGroupAPI.Models.ModelViews;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wiki.Api.Data.Utils;

namespace MeetGroupAPI.Data.Repositories
{
    public class AgendamentoRepository : Repository<Reserva>, IAgendamentoInterface
    {
        public AgendamentoRepository(ApiDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Reserva>> SelectAllAgendamentoBy()
        {

            return await _dataset.Include(p => p.IdUsuarioReservaNavigation)
                .Where(p => p.IdUsuarioReserva == p.IdUsuarioReservaNavigation.IdUsuario).ToListAsync();
        }

        public async Task<object> SelectRegragendamentoBy(string token, ReservaModel reservaModel)
        {
            var message = "";
            var count = 0;
            var payload = LoadToken.ViewToken(token).First(p => p.Type == "nameid").Value;

            var h1 = TimeSpan.Parse(reservaModel.Hora_Inicio);
            var h2 = TimeSpan.Parse(reservaModel.Hora_Fim);
            var totalDias = (reservaModel.Data_Inicio.Subtract(reservaModel.Data_Fim)).Days;


            if (reservaModel.Data_Inicio.DayOfWeek == DayOfWeek.Saturday || reservaModel.Data_Inicio.DayOfWeek == DayOfWeek.Sunday ||
                    reservaModel.Data_Fim.DayOfWeek == DayOfWeek.Saturday || reservaModel.Data_Fim.DayOfWeek == DayOfWeek.Sunday)
            {
               return message = "As reuniões devem ser agendadas apenas para os dias úteis!";
            }
            else if (reservaModel.Data_Inicio == DateTime.Today)
            {
                return message = "As reuniões devem ser agendadas com no mínimo um dia de antecedência!";
            }
            else if (totalDias > 40)
            {
                return message = "As reuniões devem ser agendadas com no máximo 40 dias de antecedência!";
            }
            else if ((h1.Hours - h2.Hours) > 8)
            {
                return message = "Reuniões não podem durar mais que 8 horas!";
            }



            var result = _context.Reserva.Where(p => p.DataInicioReserva <= reservaModel.Data_Inicio &&
            p.DataFimReserva >= reservaModel.Data_Fim).ToList();

            int totalEquipamentos = _context.Reserva
           .Where(p => p.DataInicioReserva <= reservaModel.Data_Inicio &&
                       p.DataFimReserva >= reservaModel.Data_Fim)
                       .Sum(x => (x.ComputadorReserva ? 1 : 0) + (x.TvReserva ? 1 : 0) + (x.InternetReserva ? 1 : 0) + (x.WebcamReserva ? 1 : 0));


            if (result.Count > 0)
            {

                foreach (var item in result)
                {
                    
                    if (TimeSpan.Parse(item.HoraInicioReserva) <= h1 && TimeSpan.Parse(item.HoraFimReserva) >= h2)
                    {
                        count++;
                        var order = _context.Sala.Where(p => p.QuantidadeLugaresSala == reservaModel.Quantidade_Pessoas 
                        && p.StatusSala == true && p.QuantidadeEquipamentoSala >= totalEquipamentos)
                          .Select((c) => new
                          {
                              c.NumeroSala,
                              c.QuantidadeLugaresSala,
                              c.QuantidadeEquipamentoSala
                          }).ToList().Take(3);

                        return new { message = "Não se pode agendar reuniões conflitantes de lugar e hora! escolha outra data/hora", order};
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

                _dataset.Add(reserva);
                await _context.SaveChangesAsync();

                var queryResult = _context.Sala.FirstOrDefault(p => p.QuantidadeLugaresSala == reservaModel.Quantidade_Pessoas
                        && p.StatusSala == true && p.QuantidadeEquipamentoSala >= totalEquipamentos);

                queryResult.StatusSala = false;


                _context.Sala.Update(queryResult);
                message = "Reserva cadastrada!";
            }


            if (count == 0)
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

                _context.Add(reserva);
                message = "Reserva cadastrada!";
            }
            await _context.SaveChangesAsync();
            return message;
        }
    }
}
