using System.Collections.Generic;

namespace MeetGroupAPI.Models
{
    public partial class Usuario
    {

        public int IdUsuario { get; set; }
        public string NomeUsuario { get; set; }
        public string TelefoneUsuario { get; set; }
        public string EmailUsuario { get; set; }
        public string SenhaUsuario { get; set; }
        public bool StatusUsuario { get; set; }

    }
}
