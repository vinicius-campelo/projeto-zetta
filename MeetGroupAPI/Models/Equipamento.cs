using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeetGroupAPI.Models
{
    public partial class Equipamento
    {
        public Equipamento()
        {
            Sala = new HashSet<Sala>();
        }

        public int IdEquipamento { get; set; }
        public string NomeEquipamento { get; set; }
        public bool StatusEquipamento { get; set; }

        public virtual ICollection<Sala> Sala { get; set; }
    }
}
