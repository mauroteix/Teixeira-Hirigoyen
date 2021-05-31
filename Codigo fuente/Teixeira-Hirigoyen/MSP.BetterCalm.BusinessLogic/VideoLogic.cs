using Msp.BetterCalm.HandleMessage;
using MSP.BetterCalm.BusinessLogicInterface;
using MSP.BetterCalm.DataAccessInterface;
using MSP.BetterCalm.Domain;
using MSP.BetterCalm.HandleMessage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UruguayNatural.HandleError;

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

        private void ValidateVideo(Video video)
        {
            if (video.NameEmpty()) throw new FieldEnteredNotCorrect("The name cannot be empty");
            if (video.AuthorEmpty()) throw new FieldEnteredNotCorrect("The author cannot be empty");
            if (video.LinkVideoEmpty()) throw new FieldEnteredNotCorrect("The link video cannot be empty");
            if (video.CategoryVideoEmpty()) throw new FieldEnteredNotCorrect("You must add a category to the video");
            if (video.HourIsEmpty() && video.MinSecondsIsEmpty()) throw new FieldEnteredNotCorrect("Video must have duration");
            if (video.Hour < 0 || video.MinSeconds < 0) throw new FieldEnteredNotCorrect("Video duration must be positive");
            ValidateCategoriesId(video);
            ValidatePlaylistId(video);
            ValidateCategoryUnique(video);
            ValidatePlaylistUnique(video);
        }

        private void ValidateCategoriesId(Video video)
        {
            var categoryList = categoryRepository.GetAll().Select(c => c.Id).ToList();
            var list = video.CategoryVideo.ToList();
            var exist = true;
            list.ForEach(c => {
                if (!categoryList.Contains(c.IdCategory))
                {
                    exist = false;
                }
            });
            if (!exist) throw new EntityNotExists("One ore more category do not exist");
        }

        private void ValidatePlaylistId(Video video)
        {
            var playlistList = playlistRepository.GetAll().Select(u => u.Id).ToList();
            var list = video.PlaylistVideo.ToList();
            var exist = true;
            list.ForEach(c => {
                if (!playlistList.Contains(c.IdPlaylist))
                {
                    exist = false;
                }
            });
            if (!exist) throw new EntityNotExists("One ore more playlist do not exist");
        }

        private void ValidateCategoryUnique(Video video)
        {
            var list = video.CategoryVideo.ToList();
            var repetidos = false;
            var iterador = 1;
            list.ForEach(c => {
                if (list.Skip(iterador).Contains(c))
                {
                    repetidos = true;
                }

                iterador++;
            });
            if (repetidos) throw new EntityAlreadyExist("There are two or more equal categories");
        }

        private void ValidatePlaylistUnique(Video video)
        {
            var list = video.PlaylistVideo.ToList();
            var repetidos = false;
            var iterador = 1;
            list.ForEach(c => {
                if (list.Skip(iterador).Contains(c))
                {
                    repetidos = true;
                }

                iterador++;
            });
            if (repetidos) throw new EntityAlreadyExist("There are two or more equal playlist");
        }
    }
}
