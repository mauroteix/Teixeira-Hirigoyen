using System;
using System.Collections.Generic;
using System.Text;

namespace MSP.BetterCalm.Domain
{
    public class CategoryVideo
    {
        public int IdCategory { get; set; }
        public Category Category { get; set; }
        public Video Video { get; set; }

        public CategoryVideo()
        { }

     
    }
}
