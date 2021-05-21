using System;
using System.Collections.Generic;
using System.Text;

namespace MSP.BetterCalm.Domain
{
    public class Meeting
    {
        public User User { get; set; }
        public int IdUser { get; set; }
        public Psychologist Psychologist { get; set; }
        public int IdPsychologist { get; set; }
        public DateTime Date { get; set; }
        public string AdressMeeting { get; set; }
        public meetingDuration MeetingDuration { get; set; }
        public override bool Equals(object obj)
        {
            if (!this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                Meeting meeting = (Meeting)obj;
                return (this.IdPsychologist ==  meeting.IdPsychologist && this.IdUser == meeting.IdUser);
            }
        }

    }
}
