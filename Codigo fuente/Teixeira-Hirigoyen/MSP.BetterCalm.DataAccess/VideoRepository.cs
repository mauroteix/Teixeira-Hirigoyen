using Microsoft.EntityFrameworkCore;
using MSP.BetterCalm.DataAccessInterface;
using MSP.BetterCalm.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MSP.BetterCalm.DataAccess
{
    public class VideoRepository : IData<Video>
    {
        readonly BetterCalmContext _context;

        public VideoRepository(BetterCalmContext context)
        {
            _context = context;
        }

        public void Add(Video entity)
        {
            _context.Video.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(Video entity)
        {
            throw new NotImplementedException();
        }

        public Video Get(int id)
        {
            return _context.Video
                 .Include(t => t.CategoryVideo).ThenInclude(u => u.Category)
                 .Include(r => r.PlaylistVideo).ThenInclude(s => s.Playlist)
                 .FirstOrDefault(u => u.Id == id);
        }

        public IEnumerable<Video> GetAll()
        {
            return _context.Video
                .Include(t => t.CategoryVideo).ThenInclude(u => u.Category)
                .Include(r => r.PlaylistVideo).ThenInclude(s => s.Playlist)
                .ToList();
        }

        public void Update(Video entity)
        {
            throw new NotImplementedException();
        }
    }
}
