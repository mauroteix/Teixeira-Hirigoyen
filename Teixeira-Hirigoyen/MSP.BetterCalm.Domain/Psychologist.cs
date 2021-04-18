using System;
using System.Collections.Generic;
using System.Text;

namespace MSP.BetterCalm.Domain
{
    public class Psychologist
    {
        public int Id { get; set; }
        public string Name { get; set;}
        public consultation Consultation{ get; set; }

    }
}
