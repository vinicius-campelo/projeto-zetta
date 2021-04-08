using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MeetGroupAPI.Models.ModelViews
{
    public class ReservaModel
    {
        [Required(ErrorMessage = "Data inicial é obrigatório!")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date)]
        public DateTime Data_Inicio { get; set; }

        [Required(ErrorMessage = "Data final é obrigatório!")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date)]
        public DateTime Data_Fim { get; set; }

        [Required(ErrorMessage = "Hora inicial é obrigatório!")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:hh\\:mm}")]
        public string Hora_Inicio { get; set; }

        [Required(ErrorMessage = "Hora final é obrigatório!")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:hh\\:mm}")]
        public string Hora_Fim { get; set; }


        [Required(ErrorMessage = "Quantidade de pessoas é obrigatório!")]
        [Range(1, 20)]
        public int Quantidade_Pessoas { get; set; }

        [Range(0, 1)]
        public bool Computador { get; set; }

        [Range(0, 1)]
        public bool Tv { get; set; }

        [Range(0, 1)]
        public bool Internet { get; set; }

        [Range(0, 1)]
        public bool Webcam { get; set; }

    }
}
