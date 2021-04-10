using System;

namespace MSP.BetterCalm.Domain
{
    public class Track
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }

        public bool NameEmpty()
        {
            return this.Name.Length == 0;
        }
        public bool AuthorEmpty()
        {
            return this.Author.Length == 0;
        }
    }
}
