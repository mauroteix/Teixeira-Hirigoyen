using MSP.BetterCalm.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace MSP.BetterCalm.BusinessLogicInterface
{
    public interface IVideoLogic
    {
        public Video Get(int id);
        public void Add(Video video);
        public void Delete(Video video);
        public List<Video> GetAll();
        public void Update(Video video, int id);
        public bool ExistVideoByName(Video video);
        bool ValidateVideoToAdd(Video video);
        Video GetVideoByName(string name);
    }
}
