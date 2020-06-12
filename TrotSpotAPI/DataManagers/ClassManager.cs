using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;
using TrotSpotAPI.Models;
using TrotSpotAPI.Models.Repo;

namespace TrotSpotAPI.DataManagers
{
    public class ClassManager : IDataRepository<Class>
    {
        readonly TrotSpotContext _context;

        public ClassManager(TrotSpotContext context)
        {
            _context = context;
        }

        public async Task<IList<Class>> GetAll()
        {
            return await _context.Classes.ToListAsync();
        }

        public async Task<Class> Get(int id)
        {
          return await _context.Classes.FindAsync(id);
        }

        public async Task<Class> Add(Class @class)
        {
            _context.Classes.Add(@class);
            await _context.SaveChangesAsync();

            return @class;
        }

        public async Task<Class> Update(int id, Class @class)
        {
            _context.Entry(@class).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
                //if (!ClassExists(id))
                //{
                //    return NotFound();
                //}
                //else
                //{
                //    throw;
                //}
            }
            return await _context.Classes.FindAsync(id);
        }

        public async Task<Class> Delete(Class @class)
        {
            _context.Classes.Remove(@class);
            await _context.SaveChangesAsync();

            return @class;
        }

        private bool ClassExists(int id)
        {
            return _context.Classes.Any(e => e.ID == id);
        }

    }
}
