using Homework_NetCore.Models.db;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Homework_NetCore.Models.Repositories
{
    public interface IEmployeeRepository
    {
        void Create(Employee item);

        Task<Employee> FindById(int? id);

        Task<IEnumerable<Employee>> Get();

        Task<IEnumerable<Employee>> Get(Func<Employee, bool> predicate);

        void Remove(Employee item);

        void Update(Employee item);

        Task Save();
    }

    public class EmployeeRepository : IEmployeeRepository
    {
        private CompanyContext _context;
        private DbSet<Employee> _dbSet;

        public EmployeeRepository(CompanyContext context)
        {
            _context = context;
            _dbSet = context.Set<Employee>();
        }

        public async Task<Employee> FindById(int? id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<IEnumerable<Employee>> Get()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<IEnumerable<Employee>> Get(Func<Employee, bool> predicate)
        {
            return await _dbSet.AsNoTracking().Where(predicate).AsQueryable().ToListAsync();
        }

        public void Create(Employee item)
        {
            _dbSet.Add(item);
        }

        public void Remove(Employee item)
        {
            _dbSet.Remove(item);
        }

        public void Update(Employee item)
        {
            _context.Entry(item).State = EntityState.Modified;
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
