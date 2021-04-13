using MeetGroupAPI.Models;
using MeetGroupAPI.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeetGroupAPI.Data.Repositories
{
    public class AgendamentoRepository : Repository<Reserva>, IAgendamentoInterface
    {
        public AgendamentoRepository(ApiDbContext context) : base(context)
        {

        }
    }
}
