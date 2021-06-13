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

        public PlaylistLogic(IData<Playlist> repository, IData<Category> reposCategory, IData<Track> repositoryTrack)
        {
            _repository = repository;
            _repositoryCategory = reposCategory;
            _repositoryTrack = repositoryTrack;
        }

        public Playlist Get(int id)
        {
            ExistPlaylist(id);
            return _repository.Get(id);
        }

        public void Add(Playlist playlist)
        {
            ValidatePlaylist(playlist);
            Playlist play = ToEntity(playlist);
            _repository.Add(play);
        }

        private void ValidatePlaylist(Playlist playlist)
        {
            if (playlist.NameEmpty()) throw new FieldEnteredNotCorrect("The name cannot be empty");
            if (!playlist.DescriptionLength()) throw new FieldEnteredNotCorrect("The length of the description should not exceed 150 characters");
            if (playlist.PlaylistCategoryEmpty()) throw new FieldEnteredNotCorrect("A Playlist Category must be added");
            ValidateCategoriesId(playlist);
            ValidateTrackId(playlist);
            ValidateCategoryUnique(playlist);
            ValidateTrackUnique(playlist);
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
            playlist.PlaylistCategory = listCategory;
            playlist.PlaylistTrack = listTrack;
            return playlist;
        }

        private void ValidateCategoryUnique(Playlist playlist)
        {
            var list = playlist.PlaylistCategory.ToList();
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

        private void ValidateTrackUnique(Playlist playlist)
        {
            var list = playlist.PlaylistTrack.ToList();
            var repetidos = false;
            var iterador = 1;
            list.ForEach(c => {
                if (list.Skip(iterador).Contains(c))
                {
                    repetidos = true;
                }

                iterador++;
            });
            if (repetidos) throw new EntityAlreadyExist("There are two or more equal tracks");
        }

        private void ValidateCategoriesId(Playlist playlist)
        {
            var categoryList = _repositoryCategory.GetAll().Select(c => c.Id).ToList();
            var list = playlist.PlaylistCategory.ToList();
            var exist = true;
            list.ForEach(c => {
                if (!categoryList.Contains(c.IdCategory))
                {
                    exist = false;
                }
            });
            if (!exist) throw new EntityNotExists("One ore more category do not exist");

        }
        private bool ExistTrackByName(Track track)
        {
            List<Track> list = _repositoryTrack.GetAll().ToList();
            string name = track.Name;
            Track findTrack = list.Find(c => c.Name == name);
            if (findTrack == null) return false;
            return true;
        }
        private bool validateTrack(Track track)
        {
            if (track.NameEmpty()) return false;
            if (track.AuthorEmpty()) return false;
            if (track.SoundEmpty()) return false;
            if (track.CategoryTrackEmpty()) return false;
            if (track.HourIsEmpty() && track.MinSeconds == 0) return false;
            if (track.Hour < 0 || track.MinSeconds < 0) return false;
            return true;
        }
        private void setPlayListTrack(Playlist playlist)
        {
            List<PlaylistTrack> list = new List<PlaylistTrack>();
            List<PlaylistTrack> listOfPlaylist = playlist.PlaylistTrack.ToList();
            listOfPlaylist.ForEach(item =>
            {
                if (ExistTrackByName(item.Track))
                {
                    list.Add(item);
                }
                else
                {

                }
            }
            );

        }

        private void ValidateTrackId(Playlist playlist)
        {
            var playlistTrack = playlist.PlaylistTrack.ToList();
            var rangeList = playlistTrack.Count;
            for (int aPlaylistTrack = 0; aPlaylistTrack < rangeList; aPlaylistTrack++)
            {
                var idTrack = playlistTrack[aPlaylistTrack].IdTrack;
                if(idTrack <= 0 ) throw new FieldEnteredNotCorrect("One or more tracks are with id invalid");
                var unTrack = _repositoryTrack.Get(playlistTrack[aPlaylistTrack].IdTrack);
                if (unTrack == null) throw new EntityNotExists("One or more tracks not exist");      
            }
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
