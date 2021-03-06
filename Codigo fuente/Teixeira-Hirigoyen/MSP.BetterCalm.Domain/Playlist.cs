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
        public ICollection<PlaylistVideo> PlaylistVideo { get; set; }
        public ICollection<PlaylistCategory> PlaylistCategory { get; set; }

        public Playlist()
        { }
        public bool NameEmpty()
        {
            return this.Name == null || this.Name.Length == 0;
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
            return this.Name == null || this.Image.Length == 0;
        }

        public bool PlaylistCategoryEmpty()
        {
            return this.PlaylistCategory == null || this.PlaylistCategory.Count == 0;
        }

        public override bool Equals(object obj)
        {
            if (!this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                Playlist playlist = (Playlist)obj;
                return (this.Id == playlist.Id);
            }
        }

    }
}
