using MSP.BetterCalm.Domain;
using System;

namespace MSP.BetterCalm.BusinessLogicInterface
{
    public interface IPlaylistLogic
    {
        public Playlist Get(int id);
   
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
