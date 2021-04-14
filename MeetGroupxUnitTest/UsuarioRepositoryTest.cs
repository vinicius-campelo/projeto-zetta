using MeetGroupAPI.Models;
using MeetGroupAPI.Models.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MeetGroupxUnitTest
{
    public class UsuarioRepositoryTest : IRepository<Usuario>, IUsuarioInterface
    {
        
        private readonly ICollection<Usuario> _usuario;
        

        public UsuarioRepositoryTest()
        {
            _usuario = new List<Usuario>()
            {
                new Usuario() { IdUsuario = 4, NomeUsuario = "João da Silva", EmailUsuario = "joao@email.com",
                    SenhaUsuario = "123456", TelefoneUsuario = "61991759171", StatusUsuario = true }
            };
        }
        

        public void InsertAsync(Usuario item)
        {
            throw new NotImplementedException();
        }

        public Task<object> LoginUsuario(Usuario user)
        {
            throw new NotImplementedException();
        }

        public Task<Usuario> SelectAsync(object id)
        {
            throw new NotImplementedException();
        }

        public Task<Usuario> UpdateAsync(Usuario item)
        {
            throw new NotImplementedException();
        }

        static int GeraId()
        {
            Random random = new Random();
            return random.Next(1, 100);
        }

        public Task<bool> DeleteAsync(object id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Usuario>> SelectAsync()
        {
            var enumerable = (IEnumerable)_usuario;
            return null;
           // return (Task<IEnumerable<Usuario>>)enumerable;
        }
    }
}
