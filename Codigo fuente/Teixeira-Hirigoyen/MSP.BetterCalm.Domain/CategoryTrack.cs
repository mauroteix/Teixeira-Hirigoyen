using System;
using System.Collections.Generic;
using System.Text;

namespace MSP.BetterCalm.Domain
{
    public class CategoryTrack
    {
        public int IdCategory { get; set; }
        public Category Category { get; set; }
        public int IdTrack { get; set; }
        public Track Track { get; set; }

        public CategoryTrack()
        { }

        public CategoryTrack(Track track, Category category)
        {
            this.Track = track;
            this.IdTrack = track.Id;
            this.Category = category;
            this.IdCategory = category.Id;
        }
        public override bool Equals(object obj)
        {
            if (!this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                CategoryTrack categoryTrack = (CategoryTrack)obj;
                return (this.IdCategory == categoryTrack.IdCategory && this.IdTrack == categoryTrack.IdTrack);
            }
        }

    }
}
