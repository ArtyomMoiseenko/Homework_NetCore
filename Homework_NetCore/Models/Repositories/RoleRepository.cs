using Homework_NetCore.Models.db;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Homework_NetCore.Models.Repositories
{
    public class RoleRepository
    {
        private CompanyContext _context;
        private DbSet<Role> _dbSet;

        public RoleRepository(CompanyContext context)
        {
            _context = context;
            _dbSet = context.Set<Role>();
        }

        public async Task<Role> FindById(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<IEnumerable<Role>> Get()
        {
            return await _dbSet.ToListAsync();
        }

        public IQueryable<Role> Get(Func<Role, bool> predicate)
        {
            return _dbSet.AsNoTracking().Where(predicate).AsQueryable();
        }

        public void Create(Role item)
        {
            _dbSet.Add(item);
        }

        public void Remove(Role item)
        {
            _dbSet.Remove(item);
        }

        public void Update(Role item)
        {
            _context.Entry(item).State = EntityState.Modified;
        }

        public async Task Save(Role item)
        {
            await _context.SaveChangesAsync();
        }
    }
}
