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

        public Video()
        { }

        public bool NameEmpty()
        {
            return this.Name == null || this.Name.Length == 0;
        }
       

    }
}
