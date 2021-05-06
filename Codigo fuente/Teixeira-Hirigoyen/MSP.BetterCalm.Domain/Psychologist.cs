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
        public ICollection<Expertise> Expertise { get; set; }
        public ICollection<Meeting> Meeting { get; set; }

        public Psychologist()
        { }
        public bool NameEmpty()
        {
            return this.Name == null || this.Name.Length == 0;
        }
        public bool ExpertiseEmpty()
        {
            return this.Expertise == null || this.Expertise.Count == 0;
        }
        public bool AdressMeetingEmpty()
        {
            return this.AdressMeeting == null || this.AdressMeeting.Length == 0;
        }
        public override bool Equals(object obj)
        {
            if (!this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                Psychologist psychologist = (Psychologist)obj;
                return (this.Id == psychologist.Id);
            }
        }

    }
}
