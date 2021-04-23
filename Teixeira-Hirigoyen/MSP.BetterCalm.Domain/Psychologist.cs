using System;
using System.Collections.Generic;
using System.Text;

namespace MSP.BetterCalm.Domain
{
    public class Psychologist
    {
        public int Id { get; set; }
        public string Name { get; set;}
        public meetingType MeetingType { get; set; }
        public string AdressMeeting { get; set; }
        public ICollection<MedicalCondition> ListMedicalCondition { get; set; }


    }
}
