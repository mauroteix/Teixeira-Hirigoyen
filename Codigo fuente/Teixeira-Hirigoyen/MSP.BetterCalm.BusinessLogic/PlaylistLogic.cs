using Msp.BetterCalm.HandleMessage;
using MSP.BetterCalm.BusinessLogicInterface;
using MSP.BetterCalm.DataAccessInterface;
using MSP.BetterCalm.Domain;
using MSP.BetterCalm.HandleMessage;
using System;
using System.Collections.Generic;
using System.Linq;
using UruguayNatural.HandleError;

namespace MSP.BetterCalm.BusinessLogic
{
    public class PlaylistLogic : IPlaylistLogic
    {
        IData<Playlist> _repository;
        IData<Category> _repositoryCategory;
        IData<Track> _repositoryTrack;
        ITrackLogic logicTrack;
        IVideoLogic logicVideo;

        public PlaylistLogic(IData<Playlist> repository, IData<Category> reposCategory, ITrackLogic _logicTrack,IVideoLogic _logicVideo)
        {
            _repository = repository;
            _repositoryCategory = reposCategory;
            logicTrack = _logicTrack;
            logicVideo = _logicVideo;
        }

        public Playlist Get(int id)
        {
            ExistPlaylist(id);
            return _repository.Get(id);
        }

        public void Add(Playlist playlist)
        {
            ValidatePlaylist(playlist);
            if(playlist.PlaylistVideo.Count > 0) setPlayListVideo(playlist);
            if(playlist.PlaylistTrack.Count > 0) setPlayListTrack(playlist);
            Playlist play = ToEntity(playlist);       
            _repository.Add(play);
        }

        private void ValidatePlaylist(Playlist playlist)
        {
            if (playlist.NameEmpty()) throw new FieldEnteredNotCorrect("The name cannot be empty");
            if (!playlist.DescriptionLength()) throw new FieldEnteredNotCorrect("The length of the description should not exceed 150 characters");
            if (playlist.PlaylistCategoryEmpty()) throw new FieldEnteredNotCorrect("A Playlist Category must be added");
            ValidatePlaylistCategory(playlist);


        }

        public List<Playlist> GetAll()
        {
            return _repository.GetAll().ToList();
        }
        private Playlist ToEntity(Playlist playlist)
        {
            Playlist play = new Playlist()
            { 
                 Name = playlist.Name,
                 Image = playlist.Image,
                 Description = playlist.Description,
            };
            List<PlaylistCategory> listCategory = playlist.PlaylistCategory.Select(py => new PlaylistCategory() 
            { 
                Category = _repositoryCategory.Get(py.IdCategory),
                IdCategory = py.IdCategory,
                Playlist = playlist,
                IdPlaylist = playlist.Id
            }).ToList();
            List<PlaylistTrack> listTrack = playlist.PlaylistTrack.Select(py => new PlaylistTrack()
            {
                Track = _repositoryTrack.Get(py.IdTrack),
                IdTrack = py.IdTrack,
                Playlist = playlist,
                IdPlaylist = playlist.Id
            }).ToList();
            play.PlaylistCategory = listCategory;
            play.PlaylistTrack = listTrack;
            return play;
        }

        private void ValidatePlaylistCategory(Playlist playlist)
        {
            var categoryList = _repositoryCategory.GetAll().Select(c => c.Id).ToList();
            var list = playlist.PlaylistCategory.ToList();
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
            if (!exist) throw new EntityNotExists("One ore more category do not exist");
            if (!repetidos) throw new EntityAlreadyExist("There are two or more equal categories");

        }
        private bool ValidatePlayListVideo(List<PlaylistVideo> list)
        {
            bool exist = true;
            list.ForEach(item =>
            {
                if (!logicVideo.ValidateVideoToAdd(item.Video)) exist = false;
            });
            return exist;
        }
        private bool ValidatePlayListTrack(List<PlaylistTrack> list)
        {
            bool exist = true;
            list.ForEach(item =>
            {
                if (!logicTrack.ValidateTrackToAdd(item.Track)) exist = false;
            });
            return exist;
        }
        private void setPlayListVideo(Playlist playlist)
        {
            List<PlaylistVideo> list = new List<PlaylistVideo>();
            List<PlaylistVideo> listOfPlaylistVideo = playlist.PlaylistVideo.ToList();
            if (!ValidatePlayListVideo(listOfPlaylistVideo)) throw new FieldEnteredNotCorrect("One or more video incorrect");
            listOfPlaylistVideo.ForEach(item =>
            {

                if (logicVideo.ExistVideoByName(item.Video))
                {
                    item.Video = logicVideo.GetVideoByName(item.Video.Name);
                    item.IdVideo = item.Video.Id;
                    list.Add(item);
                }
                else
                {
                    logicVideo.Add(item.Video);
                    item.Video = logicVideo.GetVideoByName(item.Video.Name);
                    item.IdVideo = item.Video.Id;
                    list.Add(item);
                }

            }
            );
            playlist.PlaylistVideo = list;
        }

        private void setPlayListTrack(Playlist playlist)
        {
            List<PlaylistTrack> list = new List<PlaylistTrack>();
            List<PlaylistTrack> listOfPlaylistTrack = playlist.PlaylistTrack.ToList();
            if(!ValidatePlayListTrack(listOfPlaylistTrack)) throw new FieldEnteredNotCorrect("One or more track incorrect");
            listOfPlaylistTrack.ForEach(item =>
            {

                if (logicTrack.ExistTrackByName(item.Track))
                {
                    item.Track = logicTrack.GetTrackByName(item.Track.Name);
                    item.IdTrack = item.Track.Id;
                    list.Add(item);
                }
                else
                {
                    logicTrack.Add(item.Track);
                    item.Track = logicTrack.GetTrackByName(item.Track.Name);
                    item.IdTrack = item.Track.Id;
                    list.Add(item);
                }
                
            }
            );
            playlist.PlaylistTrack = list;
        }
   
        private void ExistPlaylist(int id)
        {
            Playlist unPlaylist = _repository.Get(id);
            if (unPlaylist == null) throw new EntityNotExists("The playlist with id: " + id + " does not exist");
        }

        public void Delete(Playlist playlist)
        {
            ExistPlaylist(playlist.Id);
            _repository.Delete(playlist);
        }

        public void Update(Playlist playlist, int id)
        {
            ExistPlaylist(id);
            Playlist unPlaylist = _repository.Get(id);
            ValidatePlaylist(playlist);
            unPlaylist.Name = playlist.Name;
            unPlaylist.Description = playlist.Description;
            unPlaylist.Image = playlist.Image;
            unPlaylist.PlaylistCategory = playlist.PlaylistCategory;
            unPlaylist.PlaylistTrack = playlist.PlaylistTrack;
            _repository.Update(unPlaylist);
        }

        
    }
}
