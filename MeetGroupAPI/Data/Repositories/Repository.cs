using MeetGroupAPI.Models;
using MeetGroupAPI.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeetGroupAPI.Data.Repositories
{
    public abstract class Repository<T> : IRepository<T> where T : class
    {
        protected readonly ApiDbContext _context;
        protected DbSet<T> _dataset;

        public Repository(ApiDbContext context)
        {
            _context = context;
            _dataset = _context.Set<T>();
        }

        public async Task<bool> DeleteAsync(object id)
        {
            try
            {
                var result = await _dataset.FindAsync(id);
                if (result == null)
                {
                    return false;
                }

                _dataset.Remove(result);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception err)
            {

                throw err;
            }
        }

        public void InsertAsync(T item)
        {
            try
            {
                _dataset.Add(item);
                _context.SaveChanges();
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public async Task<IEnumerable<T>> SelectAsync()
        {
            try
            {
                return await _dataset.ToListAsync();
            }
            catch (Exception err)
            {

                throw err;
            }
        }

        public async Task<T> SelectAsync(object id)
        {
            try
            {
                return await _dataset.FindAsync(id);
            }
            catch (Exception err)
            {

                throw err;
            }
        }

        public async Task<T> UpdateAsync(T item)
        {
            try
            {
                var result = await _dataset.FindAsync(item);
                if (result == null)
                {
                    return null;
                }

                _context.Entry(result).CurrentValues.SetValues(item);
                await _context.SaveChangesAsync();
            }
            catch (Exception err)
            {

                throw err;
            }

            return item;
        }
    
    }
}
