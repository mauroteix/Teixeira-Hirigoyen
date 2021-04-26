using System;
using System.Collections.Generic;

namespace MSP.BetterCalm.Domain
{
    public class Track
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public string Image { get; set; }
        public int Hour { get; set; }
        public double MinSeconds { get; set; }
        public string Sound { get; set; }
        public ICollection<CategoryTrack> CategoryTrack { get; set; }
        public ICollection<PlaylistTrack> PlaylistTrack { get; set; }


        public bool NameEmpty()
        {
            return this.Name.Length == 0;
        }
        public bool AuthorEmpty()
        {
            return this.Author.Length == 0;
        }
        public bool ImageEmpty()
        {
            return this.Image.Length == 0;
        }
        public bool SoundEmpty()
        {
            return this.Sound.Length == 0;
        }

        public bool HourIsEmpty()
        {
            return this.Hour == 0;
        }
        public bool CategoryTrackEmpty()
        {
            return this.CategoryTrack.Count == 0;
        }

        public override bool Equals(object obj)
        {
            if (!this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                Track track = (Track)obj;
                return (this.Id == track.Id);
            }
        }
    }
}
