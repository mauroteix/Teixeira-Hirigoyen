using System;
using System.Collections.Generic;
using System.Text;

namespace MSP.BetterCalm.Domain
{
    public class PlaylistVideo
    {
        public int IdPlaylist { get; set; }
        public Playlist Playlist { get; set; }

        public PlaylistVideo()
        { }
    }
}
