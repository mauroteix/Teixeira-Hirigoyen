using System;
using System.Collections.Generic;
using System.Text;

namespace MSP.BetterCalm.Domain
{
    public class Video
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public int Hour { get; set; }
        public double MinSeconds { get; set; }
        public string LinkVideo { get; set; }
        public ICollection<CategoryVideo> CategoryVideo { get; set; }
        public ICollection<PlaylistVideo> PlaylistVideo { get; set; }

        public Video()
        { }

        public bool NameEmpty()
        {
            return this.Name == null || this.Name.Length == 0;
        }
        public bool AuthorEmpty()
        {
            return this.Author == null || this.Author.Length == 0;
        }
        public bool HourIsEmpty()
        {
            return this.Hour == 0;
        }
        public bool MinSecondsIsEmpty()
        {
            return this.MinSeconds == 0;
        }
        public bool LinkVideoEmpty()
        {
            return this.LinkVideo == null || this.LinkVideo.Length == 0;
        }

        public bool CategoryVideoEmpty()
        {
            return this.CategoryVideo == null || this.CategoryVideo.Count == 0;
        }


        public override bool Equals(object obj)
        {
            if (!this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                Video video = (Video)obj;
                return (this.Id == video.Id);
            }
        }
    }
}
