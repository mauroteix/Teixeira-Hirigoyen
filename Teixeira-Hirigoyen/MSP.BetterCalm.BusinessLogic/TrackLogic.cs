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
            Track unTrack = _repository.Get(id);
            if (unTrack == null) throw new EntityNotExists("The track with id: " + id + " does not exist");
            return _repository.Get(id);
        }

        public void Add(Track track)
        {
            ValidateTrack(track);
            ValidateCategoriesId(track);
            ValidatePlaylistId(track);
            ValidateCategoryUnique(track);
            ValidatePlaylistUnique(track);
            Track unTrack = ToEntity(track);
            _repository.Add(unTrack);
        }

        private void ValidateTrack(Track track)
        {
            if (track.NameEmpty()) throw new FieldEnteredNotCorrect("The name cannot be empty");
            if (track.AuthorEmpty()) throw new FieldEnteredNotCorrect("The author cannot be empty");
            if (track.SoundEmpty()) throw new FieldEnteredNotCorrect("The sound cannot be empty");
            if (track.CategoryTrackEmpty()) throw new FieldEnteredNotCorrect("You must add a category to the track");
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
            if (!exist) throw new FieldEnteredNotCorrect("One ore more playlist do not exist");
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
            if (repetidos) throw new FieldEnteredNotCorrect("There are two or more equal categories");
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
            if (repetidos) throw new FieldEnteredNotCorrect("There are two or more equal playlist");
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
            if (!exist) throw new FieldEnteredNotCorrect("One ore more category do not exist");

        }

        public void Delete(Track track)
        {
            Track unTrack = _repository.Get(track.Id);
            if (unTrack == null) throw new EntityNotExists("The track with id: " + track.Id + " does not exist");
            _repository.Delete(unTrack);
        }

    }
}
