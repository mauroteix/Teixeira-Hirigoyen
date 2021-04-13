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

    }
}
