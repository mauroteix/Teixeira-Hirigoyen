using Msp.BetterCalm.HandleMessage;
using MSP.BetterCalm.BusinessLogicInterface;
using MSP.BetterCalm.DataAccessInterface;
using MSP.BetterCalm.Domain;
using MSP.BetterCalm.HandleMessage;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MSP.BetterCalm.BusinessLogic
{
    public class PlaylistLogic : IPlaylistLogic
    {
        IData<Playlist> _repository;
        IData<Category> _repositoryCategory;
        public PlaylistLogic(IData<Playlist> repository, IData<Category> repositoryCategory)
        {
            _repository = repository;
            _repositoryCategory = repositoryCategory;
        }

        public Playlist Get(int id)
        {
            Playlist unPlaylist = _repository.Get(id);
            if (unPlaylist == null) throw new EntityNotExists("The playlist with id: " + id + " does not exist");
            return _repository.Get(id);
        }

        public void Add(Playlist playlist)
        {
            if (playlist.NameEmpty()) throw new FieldEnteredNotCorrect("The name cannot be empty");
            if(!playlist.DescriptionLength()) throw new FieldEnteredNotCorrect("The length of the description should not exceed 150 characters");
            if (playlist.PlaylistCategoryEmpty()) throw new FieldEnteredNotCorrect("A Playlist Category must be added");           
            _repository.Add(playlist);
        }

        public List<Playlist> GetAll()
        {
            return _repository.GetAll().ToList();
        }

    }
}
