using Microsoft.EntityFrameworkCore;
using Models;
using Repositories.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class PresentationRepository : BaseRepository<Presentation>
    {
        private static readonly int _batchSize = 20;
        public PresentationRepository(Task6DbContext context) : base(context)
        {
        }

        public override async Task Create(Presentation entity)
        {
            _context.Presentations.Add(entity);
            await _context.SaveChangesAsync();
        }

        public override async Task Delete(Presentation entity)
        {
            _context.Presentations.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public override async Task Update(Presentation entity)
        {
            _context.Presentations.Update(entity);
            await _context.SaveChangesAsync();
        }

        public override async Task<IEnumerable<Presentation>> GetAll()
        {
            return await _context.Presentations.ToListAsync();
        }
        public async Task<IEnumerable<Presentation>> GetByBatch(int batch)
        {
            return await _context.Presentations.Skip(_batchSize * batch).Take(_batchSize).ToListAsync();
        }

        public async Task<Presentation?> GetById(int id)
        {
            return await _context.Presentations.FirstOrDefaultAsync(p => p.Id == id);
        }

    }
}
