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
        public int IdVideo { get; set; }

        public CategoryVideo()
        { }

        public override bool Equals(object obj)
        {
            if (!this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                CategoryVideo categoryVideo = (CategoryVideo)obj;
                return (this.IdCategory == categoryVideo.IdCategory && this.IdVideo == categoryVideo.IdVideo);
            }
        }

    }
}
