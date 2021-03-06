using Microsoft.EntityFrameworkCore;
using MSP.BetterCalm.DataAccessInterface;
using MSP.BetterCalm.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MSP.BetterCalm.DataAccess
{
    public class PlaylistRepository : IData<Playlist>
    {
        readonly BetterCalmContext _context;

        public PlaylistRepository(BetterCalmContext context)
        {
            _context = context;
        }

        public void Add(Playlist entity)
        {
            _context.Playlist.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(Playlist entity)
        {
            _context.Playlist.Remove(_context.Playlist.Find(entity.Id));
            _context.SaveChanges();
        }

        public Playlist Get(int id)
        {
            return _context.Playlist
                   .Include(t => t.PlaylistCategory).ThenInclude(u => u.Category)
                   .Include(r => r.PlaylistTrack).ThenInclude(s => s.Track)
                   .Include(r => r.PlaylistVideo).ThenInclude(v => v.Video)
                   .FirstOrDefault(u => u.Id == id);
        }

        public IEnumerable<Playlist> GetAll()
        {
            return _context.Playlist
                .Include(t => t.PlaylistCategory).ThenInclude(u => u.Category)
                .Include(r => r.PlaylistTrack).ThenInclude(s => s.Track)
                .Include(r => r.PlaylistVideo).ThenInclude(v => v.Video)
                .ToList();
        }

        public void Update(Playlist entity)
        {
            _context.Playlist.Update(entity);
            _context.SaveChanges();
        }
    }
}
