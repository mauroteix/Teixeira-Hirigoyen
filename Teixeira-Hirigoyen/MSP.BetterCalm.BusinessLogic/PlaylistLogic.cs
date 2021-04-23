using MSP.BetterCalm.BusinessLogicInterface;
using MSP.BetterCalm.DataAccessInterface;
using MSP.BetterCalm.Domain;
using System;

namespace MSP.BetterCalm.BusinessLogic
{
    public class PlaylistLogic : IPlaylistLogic
    {
        IData<Playlist> _repository;
        public PlaylistLogic(IData<Playlist> repository)
        {
            _repository = repository;
        }

        public Playlist Get(int id)
        {
            return _repository.Get(id);
        }
    }
}
