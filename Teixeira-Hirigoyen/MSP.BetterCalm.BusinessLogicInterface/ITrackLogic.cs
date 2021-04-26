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
    }
}
