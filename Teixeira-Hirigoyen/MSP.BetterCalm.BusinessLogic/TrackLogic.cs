using Msp.BetterCalm.HandleMessage;
using MSP.BetterCalm.BusinessLogicInterface;
using MSP.BetterCalm.DataAccessInterface;
using MSP.BetterCalm.Domain;
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
        IData<Category> _reposCategory;
        public TrackLogic(IData<Track> repository, IData<Category> reposCategory)
        {
            _repository = repository;
            _reposCategory = reposCategory;
        }

        public Track Get(int id)
        {
            Track unTrack = _repository.Get(id);
            if (unTrack == null) throw new EntityNotExists("The track with id: " + id + " does not exist");
            return _repository.Get(id);
        }

        public void Add(Track track)
        {
            if (track.NameEmpty()) throw new FieldEnteredNotCorrect("The name cannot be empty");
            if (track.AuthorEmpty()) throw new FieldEnteredNotCorrect("The author cannot be empty");
            if(track.SoundEmpty()) throw new FieldEnteredNotCorrect("The sound cannot be empty");
            if(track.CategoryTrackEmpty()) throw new FieldEnteredNotCorrect("You must add a category to the track");
            Track unTrack = ToEntity(track);
            _repository.Add(unTrack);
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
                Category = _reposCategory.Get(py.IdCategory),
                IdCategory = py.IdCategory,
                Track = track,
                IdTrack = track.Id
            }).ToList();
            track.CategoryTrack = list;
            return track;
        }

        public void Delete(Track track)
        {
            Track unTrack = _repository.Get(track.Id);
            if (unTrack == null) throw new EntityNotExists("The track with id: " + track.Id + " does not exist");
            _repository.Delete(unTrack);
        }

    }
}
