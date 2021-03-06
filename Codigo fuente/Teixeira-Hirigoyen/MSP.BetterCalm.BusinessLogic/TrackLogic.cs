using Msp.BetterCalm.HandleMessage;
using MSP.BetterCalm.BusinessLogicInterface;
using MSP.BetterCalm.DataAccessInterface;
using MSP.BetterCalm.Domain;
using MSP.BetterCalm.DTO;
using MSP.BetterCalm.HandleMessage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UruguayNatural.HandleError;

namespace MSP.BetterCalm.BusinessLogic
{
    public class TrackLogic : ITrackLogic
    {
        IData<Track> _repository;
        IData<Category> categoryRepository;
        IData<Playlist> playlistRepository;
        public TrackLogic(IData<Track> repository, IData<Category> reposCategory, IData<Playlist> playRepository)
        {
            _repository = repository;
            categoryRepository = reposCategory;
            playlistRepository = playRepository;
        }

        public Track Get(int id)
        {
            ExistTrack(id);
            return _repository.Get(id);
        }
        public bool ExistTrackByName(Track track)
        {
            List<Track> list = _repository.GetAll().ToList();
            string name = track.Name;
            Track findTrack = list.Find(c => c.Name == name);
            if (findTrack == null) return false;
            return true;
        }
        public void Add(Track track)
        {
            ValidateTrack(track);
            if (ExistTrackByName(track) == true) throw new EntityAlreadyExist("The track with name: " + track.Name + " already exist");
            Track unTrack = ToEntity(track);
            _repository.Add(unTrack);
        }

        private void ExistTrack(int id)
        {
            Track unTrack = _repository.Get(id);
            if (unTrack == null) throw new EntityNotExists("The track with id: " + id + " does not exist");
        }
        public bool ValidateTrackToAdd(Track track)
        {
            if (track.NameEmpty()) return false;
            if (track.AuthorEmpty()) return false;
            if (track.SoundEmpty()) return false;
            if (track.CategoryTrackEmpty()) return false;
            if (track.HourIsEmpty() && track.MinSeconds == 0) return false;
            if (track.Hour < 0 || track.MinSeconds < 0) return false;
            if (ValidateListCategory(track) == false) return false;
            if (ExistTrackByName(track) == true)
            {
                if (ValidateListPlayList(track) == false) return false;
            }
                
            return true;
        }
        private bool ValidateListPlayList(Track track)
        {
            var playlistList = playlistRepository.GetAll().Select(u => u.Id).ToList();
            var list = track.PlaylistTrack.ToList();
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
        private bool ValidateListCategory(Track track)
        {
            var categoryList = categoryRepository.GetAll().Select(c => c.Id).ToList();
            var list = track.CategoryTrack.ToList();
            var exist = true;
            var repetidos = true;
            var iterador = 1;
            list.ForEach(c => {
                if (!categoryList.Contains(c.IdCategory))
                {
                    exist =false;
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

        private void ValidateTrack(Track track)
        {
            if (track.NameEmpty()) throw new FieldEnteredNotCorrect("The name cannot be empty");
            if (track.AuthorEmpty()) throw new FieldEnteredNotCorrect("The author cannot be empty");
            if (track.SoundEmpty()) throw new FieldEnteredNotCorrect("The sound cannot be empty");
            if (track.CategoryTrackEmpty()) throw new FieldEnteredNotCorrect("You must add a category to the track");
            if (track.HourIsEmpty() && track.MinSeconds == 0 ) throw new FieldEnteredNotCorrect("Track must have duration");
            if (track.Hour < 0 ||  track.MinSeconds < 0) throw new FieldEnteredNotCorrect("Track duration must be positive");
            
            ValidateCategoriesId(track);
            ValidatePlaylistId(track);
            ValidateCategoryUnique(track);
            ValidatePlaylistUnique(track);
        }
        
        private Track ToEntity(Track track)
        {
            Track unTrack = new Track()
            {
                Name = track.Name,
                Author = track.Author,
                Sound = track.Sound,
                Hour = track.Hour,
                MinSeconds = track.MinSeconds,
                Image = track.Image
            };
            List<CategoryTrack> list = track.CategoryTrack.Select(py => new CategoryTrack()
            {
                Category = categoryRepository.Get(py.IdCategory),
                IdCategory = py.IdCategory,
                Track = track,
                IdTrack = track.Id
            }).ToList();
            List<PlaylistTrack> listTrack = track.PlaylistTrack.Select(py => new PlaylistTrack()
            {
                Track = track,
                IdTrack = track.Id,
                Playlist = playlistRepository.Get(py.IdPlaylist),
                IdPlaylist = py.IdPlaylist
            }).ToList();
            unTrack.CategoryTrack = list;
            unTrack.PlaylistTrack = listTrack;
            return unTrack;
        }
        private void ValidatePlaylistId(Track track)
        {
            var playlistList = playlistRepository.GetAll().Select(u => u.Id).ToList();
            var list = track.PlaylistTrack.ToList();
            var exist = true;
            list.ForEach(c => {
                if (!playlistList.Contains(c.IdPlaylist))
                {
                    exist = false;
                }
            });
            if (!exist) throw new EntityNotExists("One ore more playlist do not exist");
        }

            private void ValidateCategoryUnique(Track track)
        {
            var list = track.CategoryTrack.ToList();
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

        private void ValidatePlaylistUnique(Track track)
        {
            var list = track.PlaylistTrack.ToList();
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
        private void ValidateCategoriesId(Track track)
        {
            var categoryList = categoryRepository.GetAll().Select(c => c.Id).ToList();
            var list = track.CategoryTrack.ToList();
            var exist = true;
            list.ForEach(c => {
                if (!categoryList.Contains(c.IdCategory))
                {
                    exist = false;
                }
            });
            if (!exist) throw new EntityNotExists("One ore more category do not exist");

        }

        public void Delete(Track track)
        {
            ExistTrack(track.Id);
            _repository.Delete(track);
        }

        public List<Track> GetAll()
        {
            return _repository.GetAll().ToList();
        }

        public void Update(Track track, int id)
        {
            ExistTrack(id);
            Track unTrack = _repository.Get(id);
            ValidateTrack(track);
            if (track.Name != unTrack.Name)
            {
                if (ExistTrackByName(track) == true) throw new EntityAlreadyExist("The track with name: " + track.Name + " already exist");
            }
            unTrack.Name = track.Name;
            unTrack.Image = track.Image;
            unTrack.Author = track.Author;
            unTrack.MinSeconds = track.MinSeconds;
            unTrack.Hour = track.Hour;
            unTrack.CategoryTrack = track.CategoryTrack;
            unTrack.PlaylistTrack = track.PlaylistTrack;
            _repository.Update(unTrack);
        }
        public Track GetTrackByName(string name)
        {
            List<Track> list = _repository.GetAll().ToList();
            Track findTrack = list.Find(c => c.Name == name);
             return findTrack;
        }



    }
}
