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
            categoryRepository = reposCategory;
            playlistRepository = playRepository;
        }

        public Video Get(int id)
        {
            ExistVideo(id);
            return videoRepository.Get(id);
        }

        public void Add(Video video)
        {
            ValidateVideo(video);
            if (ExistVideoByName(video) == true) throw new EntityAlreadyExist("The track with name: " + video.Name + " already exist");
            Video unVideo = ToEntity(video);
            videoRepository.Add(video);
        }
        private Video ToEntity(Video video)
        {
         
                Video unVideo = new Video()
                {
                    Name = video.Name,
                    Author = video.Author,
                    Hour = video.Hour,
                    MinSeconds = video.MinSeconds,
                    LinkVideo = video.LinkVideo
                };
                List<CategoryVideo> list = video.CategoryVideo.Select(py => new CategoryVideo()
                {
                    Category = categoryRepository.Get(py.IdCategory),
                    IdCategory = py.IdCategory,
                    Video = video,
                    IdVideo = video.Id
                }).ToList();
                List<PlaylistVideo> listTrack = video.PlaylistVideo.Select(py => new PlaylistVideo()
                {
                    Video = video,
                    IdVideo = video.Id,
                    Playlist = playlistRepository.Get(py.IdPlaylist),
                    IdPlaylist = py.IdPlaylist
                }).ToList();
                unVideo.CategoryVideo = list;
                unVideo.PlaylistVideo = listTrack;
                return unVideo;
          
        }

        public void Delete(Video video)
        {
            ExistVideo(video.Id);
            videoRepository.Delete(video);
        }

        public List<Video> GetAll()
        {
            return videoRepository.GetAll().ToList();
        }

        public void Update(Video video, int id)
        {
            ExistVideo(id);
            Video unVideo = videoRepository.Get(id);
            ValidateVideo(video);
            if (video.Name != unVideo.Name)
            {
                if (ExistVideoByName(video) == true) throw new EntityAlreadyExist("The track with name: " +video.Name + " already exist");
            }
            unVideo.Name = video.Name;
            unVideo.Author = video.Author;
            unVideo.MinSeconds = video.MinSeconds;
            unVideo.Hour = video.Hour;
            unVideo.LinkVideo = video.LinkVideo;
            unVideo.CategoryVideo = video.CategoryVideo;
            unVideo.PlaylistVideo = video.PlaylistVideo;
            videoRepository.Update(unVideo);
        }

        private void ExistVideo(int id)
        {
            Video unVideo = videoRepository.Get(id);
            if (unVideo == null) throw new EntityNotExists("The video with id: " + id + " does not exist");
        }
        private bool ValidateCategoryVideo(Video video)
        {
            var categoryList = categoryRepository.GetAll().Select(c => c.Id).ToList();
            var list = video.CategoryVideo.ToList();
            var exist = true;
            var repetidos = true;
            var iterador = 1;
            list.ForEach(c => {
                if (!categoryList.Contains(c.IdCategory))
                {
                    exist = false;
                }
                if (list.Skip(iterador).Contains(c))
                {
                    repetidos = false;
                }
                iterador++;
            });
            if (!exist) return false;
            if (!repetidos) return false;
            return true;
        }
        private bool ValidatePlaylistVideo(Video video)
        {
            var playlistList = playlistRepository.GetAll().Select(u => u.Id).ToList();
            var list = video.PlaylistVideo.ToList();
            var exist = true;
            var repetidos = true;
            var iterador = 1;
            list.ForEach(c => {
                if (!playlistList.Contains(c.IdPlaylist))
                {
                    exist = false;
                }
                if (list.Skip(iterador).Contains(c))
                {
                    repetidos = false;
                }
                iterador++;
            });
            if (!exist) return false;
            if (!repetidos) return false;
            return true;
        }
        public Video GetVideoByName(string name)
        {
            List<Video> list = videoRepository.GetAll().ToList();
            Video findVideo = list.Find(c => c.Name == name);
            return findVideo;
        }
        public bool ExistVideoByName(Video video)
        {
            List<Video> list = videoRepository.GetAll().ToList();
            string name = video.Name;
            Video findVideo = list.Find(c => c.Name == name);
            if (findVideo == null) return false;
            return true;
        }
        public bool ValidateVideoToAdd(Video video)
        {
            if (video.NameEmpty()) return false;
            if (video.AuthorEmpty()) return false;
            if (video.LinkVideoEmpty()) return false;
            if (video.CategoryVideoEmpty()) return false;
            if (video.HourIsEmpty() && video.MinSecondsIsEmpty()) return false;
            if (video.Hour < 0 || video.MinSeconds < 0) return false;
            if (ValidateCategoryVideo(video) == false) return false;
            if(ExistVideoByName(video)== true)
            {
                if (ValidatePlaylistVideo(video) == false) return false;
            }
            
            return true;
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
