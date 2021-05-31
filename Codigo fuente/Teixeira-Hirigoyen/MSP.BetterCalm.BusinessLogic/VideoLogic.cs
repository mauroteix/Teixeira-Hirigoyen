using Msp.BetterCalm.HandleMessage;
using MSP.BetterCalm.BusinessLogicInterface;
using MSP.BetterCalm.DataAccessInterface;
using MSP.BetterCalm.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace MSP.BetterCalm.BusinessLogic
{
    public class VideoLogic : IVideoLogic
    {
        IData<Video> videoRepository;
        IData<Category> categoryRepository;
        IData<Playlist> playlistRepository;

        public VideoLogic(IData<Video> repository, IData<Category> reposCategory, IData<Playlist> playRepository)
        {
            videoRepository = repository;
        }

        public Video Get(int id)
        {
            ExistVideo(id);
            return videoRepository.Get(id);
        }

        private void ExistVideo(int id)
        {
            Video unVideo = videoRepository.Get(id);
            if (unVideo == null) throw new EntityNotExists("The video with id: " + id + " does not exist");
        }
    }
}
