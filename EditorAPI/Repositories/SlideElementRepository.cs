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
    public class SlideElementRepository : BaseRepository<Sharedslideelement>
    {
        public SlideElementRepository(Task6DbContext context) : base(context)
        {
        }

        public override async Task Create(Sharedslideelement entity)
        {
            _context.Sharedslideelements.Add(entity);
            await _context.SaveChangesAsync();
        }

        public override async Task Delete(Sharedslideelement entity)
        {
            _context.Sharedslideelements.Remove(entity); 
            await _context.SaveChangesAsync();
        }

        public override async Task Update(Sharedslideelement entity)
        {
            _context.Update(entity);
            await _context.SaveChangesAsync();
        }

        public override async Task<IEnumerable<Sharedslideelement>> GetAll()
        {
            return await _context.Sharedslideelements.ToListAsync();
        }

        /// <summary>
        /// Returns elements of corresponding slide
        /// </summary>
        /// <param name="id">Id of slide</param>
        /// <returns></returns>
        public async Task<IEnumerable<Sharedslideelement>> GetBySlideId(int id)
        {
            return await _context.Sharedslideelements.Where(s=>s.Slideid == id).ToListAsync();
        }
    }
}
