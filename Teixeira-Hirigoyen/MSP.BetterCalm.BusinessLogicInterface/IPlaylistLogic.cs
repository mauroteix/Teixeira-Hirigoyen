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
        public void Delete(Playlist playlist);
        public void Update(Playlist playlist);

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
