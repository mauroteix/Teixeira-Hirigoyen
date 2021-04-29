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
            ValidateCategoriesId(track.CategoryTrack.ToList());
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
        private void ValidateCategoriesId(List<CategoryTrack> list)
        {
            int largeList = list.Count;
            var listCategories = categoryRepository.GetAll().ToList();
            int largelistCategories = listCategories.Count;
            bool state = false;
            for (int categoryTrack = 0; categoryTrack < largeList; categoryTrack++)
            {
                for (int category = 0; category < largelistCategories; category++)
                {
                        if ((listCategories[category].Id == list[categoryTrack].IdCategory))
                        {
                            state = true;
                            category = largelistCategories;
                        }
                }
                if (!state)
                { 
                    throw new FieldEnteredNotCorrect("The category that you add do not exist");
                }
                state = false;
            }
           
        }

        public void Delete(Track track)
        {
            Track unTrack = _repository.Get(track.Id);
            if (unTrack == null) throw new EntityNotExists("The track with id: " + track.Id + " does not exist");
            _repository.Delete(unTrack);
        }

    }
}
