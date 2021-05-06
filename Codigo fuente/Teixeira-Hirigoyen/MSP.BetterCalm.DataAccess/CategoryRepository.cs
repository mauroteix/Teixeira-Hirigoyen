using Microsoft.EntityFrameworkCore;
using MSP.BetterCalm.DataAccessInterface;
using MSP.BetterCalm.Domain;
using MSP.BetterCalm.HandleMessage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MSP.BetterCalm.DataAccess
{
    public class CategoryRepository : IData<Category>
    {
        readonly BetterCalmContext _context;
        
        public CategoryRepository(BetterCalmContext context)
        {
            _context = context;
        }

        public void Add(Category entity)
        {
            _context.Category.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(Category entity)
        {
            throw new CannotBePerformed("You cannot delete category " + entity.Name);
        }

        public Category Get(int id)
        {
            return _context.Category
                    .Include(t => t.PlaylistCategory).ThenInclude(u => u.Playlist)
                    .Include(r => r.CategoryTrack).ThenInclude(s => s.Track)
                    .FirstOrDefault(u => u.Id == id);
        }

        public IEnumerable<Category> GetAll()
        {
            return _context.Category
                .Include(t => t.PlaylistCategory).ThenInclude(u => u.Playlist)
                .Include(r => r.CategoryTrack).ThenInclude(s => s.Track)
                .ToList();
        }

        public void Update(Category entity)
        {
            _context.Category.Update(entity);
            _context.SaveChanges();
        }
    }
}
