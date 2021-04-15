using System;

namespace MeetGroupAPI.Models.ModelViews
{
    public class AgendaModel
    {

        public int reserva { get; set; }
        public string usuario { get; set; }
        public DateTime dataInicioReserva { get; set; }
        public DateTime dataFimReserva { get; set; }
        public string horaInicio { get; set; }
        public string horaFim { get; set; }
        public int quantidadePessoas { get; set; }
        public bool computador { get; set; }
        public bool tv { get; set; }
        public bool internet { get; set; }
        public bool webcam { get; set; }

    }


    public static class AgendaModelView
    {
        public static AgendaModel ToAgendaModel(this Reserva entity)
        {
            return new AgendaModel
            {
                reserva = entity.IdReserva,
                usuario = entity.IdUsuarioReservaNavigation.NomeUsuario,
                dataInicioReserva = entity.DataInicioReserva,
                dataFimReserva = entity.DataFimReserva,
                horaInicio = entity.HoraInicioReserva,
                horaFim = entity.HoraFimReserva,
                quantidadePessoas = entity.QuantidadePessoasReserva,
                computador = entity.ComputadorReserva,
                tv = entity.TvReserva,
                internet = entity.InternetReserva,
                webcam = entity.WebcamReserva
            };
        }
    }
 }
