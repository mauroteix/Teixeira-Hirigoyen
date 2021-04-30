using System;
using System.Collections.Generic;
using System.Text;

namespace MSP.BetterCalm.Domain
{
    public class Administrator
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public Administrator()
        { }

        public bool NameEmpty()
        {
            return this.Name == null || this.Name.Length == 0;
        }
        public bool EmailEmpty()
        {
            return this.Email == null || this.Email.Length == 0;
        }
        
    }
}
