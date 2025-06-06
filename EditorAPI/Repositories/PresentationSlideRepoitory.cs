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
    public class PresentationSlideRepoitory : BaseRepository<Sharedpresentationslide>
    {
        public PresentationSlideRepoitory(Task6DbContext context) : base(context)
        {
        }

        public override async Task Create(Sharedpresentationslide entity)
        {
            _context.Sharedpresentationslides.Add(entity);
            await _context.SaveChangesAsync();
        }

        public override async Task Delete(Sharedpresentationslide entity)
        {
            _context.Sharedpresentationslides.Remove(entity);
            await _context.SaveChangesAsync();
        }
        public override async Task Update(Sharedpresentationslide entity)
        {
            _context.Update(entity);
            await _context.SaveChangesAsync();
        }

        public override async Task<IEnumerable<Sharedpresentationslide>> GetAll()
        {
            return await _context.Sharedpresentationslides.ToListAsync();
        }

        /// <summary>
        /// Returns slides of corresponding presentation
        /// </summary>
        /// <param name="id">Id of presentation</param>
        /// <returns></returns>
        public async Task<IEnumerable<Sharedpresentationslide>> GetByPresentationId(int id)
        {
            return await _context.Sharedpresentationslides.Where(p=>p.Presentationid == id).ToListAsync();
        }
    }
}
