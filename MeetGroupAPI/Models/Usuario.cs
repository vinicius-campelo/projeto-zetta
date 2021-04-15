using System.Collections.Generic;

namespace MeetGroupAPI.Models
{
    public partial class Usuario
    {
        public Usuario()
        {
            Reserva = new HashSet<Reserva>();
        }

        public int IdUsuario { get; set; }
        public string NomeUsuario { get; set; }
        public string TelefoneUsuario { get; set; }
        public string EmailUsuario { get; set; }
        public string SenhaUsuario { get; set; }
        public bool StatusUsuario { get; set; }

        public virtual ICollection<Reserva> Reserva { get; set; }
    }
}
