using System;
using System.Collections.Generic;
using System.Text;

namespace MSP.BetterCalm.Domain
{
    public class PlaylistVideo
    {
        public int IdPlaylist { get; set; }
        public Playlist Playlist { get; set; }
        public int IdVideo { get; set; }
        public Video Video { get; set; }

        public PlaylistVideo()
        { }

        public override bool Equals(object obj)
        {
            if (!this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                PlaylistVideo playlistVideo = (PlaylistVideo)obj;
                return (this.IdVideo == playlistVideo.IdVideo && this.IdPlaylist == playlistVideo.IdPlaylist);
            }
        }
    }
}
