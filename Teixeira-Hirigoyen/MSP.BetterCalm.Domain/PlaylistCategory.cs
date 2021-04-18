using System;
using System.Collections.Generic;
using System.Text;

namespace MSP.BetterCalm.Domain
{
    public class PlaylistCategory
    {
        public int IdPlaylist { get; set; }
        public Playlist Playlist { get; set; }
        public int IdCategory { get; set; }
        public Category Category { get; set; }

        public PlaylistCategory()
        { }
    }
}
