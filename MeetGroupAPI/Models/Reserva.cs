using System;

namespace MeetGroupAPI.Models
{
    public partial class Reserva
    {
        public int IdReserva { get; set; }
        public DateTime DataInicioReserva { get; set; }
        public DateTime DataFimReserva { get; set; }
        public string HoraInicioReserva { get; set; }
        public string HoraFimReserva { get; set; }
        public int QuantidadePessoasReserva { get; set; }
        public bool ComputadorReserva { get; set; }
        public bool TvReserva { get; set; }
        public bool InternetReserva { get; set; }
        public bool WebcamReserva { get; set; }
        public int IdUsuarioReserva { get; set; }
        public bool StatusReserva { get; set; }
        //public virtual Usuario IdUsuarioReservaNavigation { get; set; }
    }
}
