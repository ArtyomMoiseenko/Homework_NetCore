using Homework_NetCore.Models.db;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Homework_NetCore.Models.Repositories
{
    public class ProjectRepository
    {
        private CompanyContext _context;
        private DbSet<Project> _dbSet;

        public ProjectRepository(CompanyContext context)
        {
            _context = context;
            _dbSet = context.Set<Project>();
        }

        public async Task<Project> FindById(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<IEnumerable<Project>> Get()
        {
            return await _dbSet.ToListAsync();
        }

        public IEnumerable<Project> Get(Func<Project, bool> predicate)
        {
            return _dbSet.AsNoTracking().Where(predicate);
        }

        public void Create(Project item)
        {
            _dbSet.Add(item);
        }

        public void Remove(Project item)
        {
            _dbSet.Remove(item);
        }

        public void Update(Project item)
        {
            _context.Entry(item).State = EntityState.Modified;
        }

        public async Task Save(Project item)
        {
            await _context.SaveChangesAsync();
        }
    }
}
