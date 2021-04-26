using Msp.BetterCalm.HandleMessage;
using MSP.BetterCalm.BusinessLogicInterface;
using MSP.BetterCalm.DataAccessInterface;
using MSP.BetterCalm.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace MSP.BetterCalm.BusinessLogic
{
    public class TrackLogic : ITrackLogic
    {
        IData<Track> _repository;
        public TrackLogic(IData<Track> repository)
        {
            _repository = repository;
        }

        public Track Get(int id)
        {
            Track unTrack = _repository.Get(id);
            if (unTrack == null) throw new EntityNotExists("The track with id: " + id + " does not exist");
            return _repository.Get(id);
        }

    }
}
