using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeetGroupAPI.Models.Interfaces
{
    public interface IRepository<T> where T : class
    {
        void InsertAsync(T item);
        Task<IEnumerable<T>> SelectAsync();
        Task<T> SelectAsync(object id);
        Task<bool> DeleteAsync(object id);
        Task<T> UpdateAsync(T item);
    }
}
