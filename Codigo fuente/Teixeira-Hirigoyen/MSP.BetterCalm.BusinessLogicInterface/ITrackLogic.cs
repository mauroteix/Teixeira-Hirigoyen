using MSP.BetterCalm.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace MSP.BetterCalm.BusinessLogicInterface
{
    public interface ITrackLogic
    {
        public Track Get(int id);
        public void Add(Track track);
        public void Delete(Track track);
        public void Update(Track track, int id);
        public List<Track> GetAll();
        public bool ExistTrackByName(Track track);
        public bool ValidateTrackToAdd(Track track);
        public Track GetTrackByName(string name);
    }
}
