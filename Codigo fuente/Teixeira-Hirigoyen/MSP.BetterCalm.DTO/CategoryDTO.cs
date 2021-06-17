using MSP.BetterCalm.Domain;
using System;
using System.Collections.Generic;

namespace MSP.BetterCalm.DTO
{
    public class CategoryDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<CategoryTrack> CategoryTrack { get; set; }
        public ICollection<PlaylistCategory> PlaylistCategory { get; set; }
        public ICollection<CategoryVideo> CategoryVideo { get; set; }

        public CategoryDTO()
        { }

        public CategoryDTO(int unId, string unName, ICollection<CategoryTrack> unCategoryTrack, ICollection<PlaylistCategory> unPlaylistCategory, ICollection<CategoryVideo> unCategoryVideo)
        {
            this.Id = unId;
            this.Name = unName;
            this.CategoryTrack = unCategoryTrack;
            this.PlaylistCategory = unPlaylistCategory;
            this.CategoryVideo = unCategoryVideo;
        }
    }
}
