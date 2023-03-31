using N5Company.Repositories.Interfaces;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace N5Company.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private bool _disposed;
        private Dictionary<string, object> _repositories;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            _repositories = new Dictionary<string, object>();
        }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
            _repositories.Clear();
        }

        public IRepository<T> GetRepository<T>() where T : class
        {
            string typeName = typeof(T).Name;
            if (_repositories.Keys.Contains(typeName))
            {
                return _repositories[typeName] as IRepository<T>;
            }
            IRepository<T> newRepository = new Repository<T>(_context);

            _repositories.Add(typeName, newRepository);
            return newRepository;
        }
    }
}
