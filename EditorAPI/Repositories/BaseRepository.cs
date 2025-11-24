using Repositories.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public abstract class BaseRepository<T> where T : class
    {
        protected Task6DbContext _context;
        public BaseRepository(Task6DbContext context)
        {
            _context = context;
        }
        protected void Send(){System.Console.WriteLine("feature2 test");}
        public abstract Task<IEnumerable<T>> GetAll();
        public abstract Task Create(T entity);
        public abstract Task Update(T entity);
        public abstract Task Delete(T entity);
        
        protected void Send(){System.Console.WriteLine("feature test");}
    }
}
