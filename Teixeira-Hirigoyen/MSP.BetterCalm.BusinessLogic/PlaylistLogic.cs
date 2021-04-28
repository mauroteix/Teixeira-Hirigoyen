﻿using Msp.BetterCalm.HandleMessage;
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
        IData<Category> _reposCategory;

        public PlaylistLogic(IData<Playlist> repository, IData<Category> reposCategory)
        {
            _repository = repository;
            _reposCategory = reposCategory;
        }

        public Playlist Get(int id)
        {
            ExistPlaylist(id);
            return _repository.Get(id);
        }

        public void Add(Playlist playlist)
        {
            ValidatePlaylist(playlist);
            ValidateCategoriesId(playlist.PlaylistCategory.ToList());
            Playlist play = ToEntity(playlist);
            _repository.Add(play);
        }

        private void ValidatePlaylist(Playlist playlist)
        {
            if (playlist.NameEmpty()) throw new FieldEnteredNotCorrect("The name cannot be empty");
            if (!playlist.DescriptionLength()) throw new FieldEnteredNotCorrect("The length of the description should not exceed 150 characters");
            if (playlist.PlaylistCategoryEmpty()) throw new FieldEnteredNotCorrect("A Playlist Category must be added");
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
            List<PlaylistCategory> list = playlist.PlaylistCategory.Select(py => new PlaylistCategory() 
            { 
                Category = _reposCategory.Get(py.IdCategory),
                IdCategory = py.IdCategory,
                Playlist = playlist,
                IdPlaylist = playlist.Id
            }).ToList();
            playlist.PlaylistCategory = list;
            return playlist;
        }

        private void ValidateCategoriesId(List<PlaylistCategory> list)
        {
            int largeList = list.Count;
            var listCategories = _reposCategory.GetAll().ToList();
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

        public void Update(Playlist playlist)
        {
            ValidatePlaylist(playlist);
            ValidateCategoriesId(playlist.PlaylistCategory.ToList());
            _repository.Update(playlist);
        }

    }
}
