using MeetGroupAPI.Models.ModelViews;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MeetGroupAPI.Models.Interfaces
{
    public interface IAgendamentoInterface : IRepository<Reserva>
    {
        Task<IEnumerable<Reserva>> SelectAllAgendamentoBy();

        Task<object> SelectRegragendamentoBy(string token, ReservaModel reservaModel);
    }
}
