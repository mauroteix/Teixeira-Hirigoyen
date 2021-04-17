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
            throw new NotImplementedException();
        }

        public Playlist Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Playlist> GetAll()
        {
            return _context.Playlist.ToList();
        }

        public void Update(Playlist entity)
        {
            throw new NotImplementedException();
        }
    }
}
