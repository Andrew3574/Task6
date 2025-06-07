using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
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
            var existingPresentation = await _context.Presentations
                .Include(p => p.Sharedpresentationslides)
                .ThenInclude(sps => sps.Slide)
                .ThenInclude(s => s.Sharedslideelements)
                .FirstOrDefaultAsync(p => p.Id == entity.Id);

            if (existingPresentation != null)
            {
                existingPresentation.Title = entity.Title;
                existingPresentation.Author = entity.Author;
                foreach (var updatedSlideLink in entity.Sharedpresentationslides)
                {
                    var existingLink = existingPresentation.Sharedpresentationslides.FirstOrDefault(s => s.Slideid == updatedSlideLink.Slideid);
                    if (existingLink?.Slide != null && updatedSlideLink.Slide != null)
                    {
                        UpdateSlide(existingLink.Slide, updatedSlideLink.Slide);                    
                    }
                }
                await _context.SaveChangesAsync();
            }

        }
        private void UpdateSlide(Slide existingSlide, Slide updatedSlide)
        {
            existingSlide.Background = updatedSlide.Background;
            foreach (var updatedElement in updatedSlide.Sharedslideelements)
            {
                var existingElement = existingSlide.Sharedslideelements.FirstOrDefault(e => e.Id == updatedElement.Id);
                UpdateElement(existingElement, updatedElement, existingSlide);
            }
        }

        private void UpdateElement(Sharedslideelement existingElement, Sharedslideelement updatedElement, Slide existingSlide)
        {
            if (existingElement != null)
            {
                existingElement.ElementX = updatedElement.ElementX;
                existingElement.ElementY = updatedElement.ElementY;
                existingElement.ElementWidth = updatedElement.ElementWidth;
                existingElement.ElementHeight = updatedElement.ElementHeight;
                existingElement.ElementContent = updatedElement.ElementContent;
            }
            else
            {
                existingSlide.Sharedslideelements.Add(updatedElement);
            }
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
