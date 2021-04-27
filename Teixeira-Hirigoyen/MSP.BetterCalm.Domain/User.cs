﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MSP.BetterCalm.Domain
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime Birthday { get; set; }
        public string Email { get; set; }
        public string Cellphone { get; set; }
        public ICollection<Meeting> Meeting { get; set; }

        public User()
        {
        }
        public bool NameEmpty()
        {
            return this.Name.Length == 0;
        }
    }
}
