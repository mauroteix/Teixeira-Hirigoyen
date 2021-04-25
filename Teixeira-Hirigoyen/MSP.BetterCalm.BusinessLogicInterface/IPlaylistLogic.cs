using MSP.BetterCalm.Domain;
using System;
using System.Collections.Generic;

namespace MSP.BetterCalm.BusinessLogicInterface
{
    public interface IPlaylistLogic
    {
        public Playlist Get(int id);
        public void Add(Playlist playlist);
        public List<Playlist> GetAll();

        /* 
         AddPlaylist
         GetPlaylistById
         GetAllPlaylist
        -----------------
         UpdatePlaylist
         GetPlaylistByCategory
        */
    }
}
