using System;
using System.Collections.Generic;
using System.Text;

namespace MSP.BetterCalm.Domain
{
    public class Playlist
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public ICollection<PlaylistTrack> PlaylistTrack { get; set; }

        // public Category Category { get; set; }

        public bool NameEmpty()
        {
            return this.Name.Length == 0;
        }
        public bool DescriptionLength()
        {
            const int maxLength = 150;
            bool ok = false;
            if (Description.Length < maxLength) ok = true;
            return ok;

        }
        public bool ImageEmpty()
        {
            return this.Image.Length == 0;
        }



    }
}
