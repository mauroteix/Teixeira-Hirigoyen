using System;
using System.Collections.Generic;
using System.Text;

namespace MSP.BetterCalm.Domain
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public ICollection<CategoryTrack> CategoryTrack { get; set; }
        public ICollection<CategoryVideo> CategoryVideo { get; set; }
        public ICollection<PlaylistCategory> PlaylistCategory { get; set; }
        public Category()
        { }

        public override bool Equals(object obj)
        {
            if (!this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                Category category = (Category)obj;
                return (this.Id==category.Id);
            }
        }


    }
}
