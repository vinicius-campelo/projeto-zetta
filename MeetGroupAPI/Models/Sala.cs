using System.Collections.Generic;

namespace MeetGroupAPI.Models
{
    public partial class Sala
    {
        public int IdSala { get; set; }
        public int NumeroSala { get; set; }
        public int QuantidadeLugaresSala { get; set; }
        public int QuantidadeEquipamentoSala { get; set; }
        public int IdSalaEquipamento { get; set; }
        public bool StatusSala { get; set; }

        public virtual Equipamento IdSalaEquipamentoNavigation { get; set; }
    }
}
