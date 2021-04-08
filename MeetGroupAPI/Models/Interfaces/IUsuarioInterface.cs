using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeetGroupAPI.Models.Interfaces
{
    public interface IUsuarioInterface : IRepository<Usuario>
    {
        Task<object> LoginUsuario(Usuario user);
    }
}
