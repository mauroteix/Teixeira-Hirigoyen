﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MSP.BetterCalm.Domain
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<CategoryTrack> CategoryTrack { get; set; }
        public ICollection<PlaylistCategory> PlaylistCategory { get; set; }

        public Category()
        { }

    }
}
