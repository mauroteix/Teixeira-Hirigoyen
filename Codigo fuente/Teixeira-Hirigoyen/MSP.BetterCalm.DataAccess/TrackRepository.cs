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
    public class TrackRepository : IData<Track>
    {
        readonly BetterCalmContext _context;

        public TrackRepository(BetterCalmContext context)
        {
            _context = context;
        }

        public void Add(Track entity)
        {
            _context.Track.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(Track entity)
        {
            _context.Track.Remove(_context.Track.Find(entity.Id));
            _context.SaveChanges();
        }

        public Track Get(int id)
        {
            return _context.Track
                .Include(t => t.CategoryTrack).ThenInclude(u => u.Category)
                .Include(r => r.PlaylistTrack).ThenInclude(s => s.Playlist)
                .FirstOrDefault(u => u.Id == id);
        }

        public IEnumerable<Track> GetAll()
        {
            return _context.Track
                .Include(t => t.CategoryTrack).ThenInclude(u => u.Category)
                .Include(r => r.PlaylistTrack).ThenInclude(s => s.Playlist)
                .ToList();
        }

        public void Update(Track entity)
        {
            _context.Track.Update(entity);
            _context.SaveChanges();
        }
    }
}
