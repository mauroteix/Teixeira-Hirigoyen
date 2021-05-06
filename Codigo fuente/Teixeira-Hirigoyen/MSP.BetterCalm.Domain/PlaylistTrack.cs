using System;
using System.Collections.Generic;
using System.Text;

namespace MSP.BetterCalm.Domain
{
    public class PlaylistTrack
    {
        public int IdPlaylist { get; set; }
        public Playlist Playlist { get; set; }
        public int IdTrack { get; set; }
        public Track Track { get; set; }
    
    public PlaylistTrack()
    { }

    public override bool Equals(object obj)
    {
        if (!this.GetType().Equals(obj.GetType()))
        {
            return false;
        }
        else
        {
                PlaylistTrack playlistTrack = (PlaylistTrack)obj;
            return (this.IdTrack == playlistTrack.IdTrack && this.IdPlaylist == playlistTrack.IdPlaylist);
        }
    }

    }
}
