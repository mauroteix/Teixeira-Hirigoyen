using System;
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
        public MedicalCondition MedicalCondition { get; set; }
        public meetingDuration MeetingDuration { get; set; }
        public discount Discount { get; set; }
        public int MeetingCount { get; set; }

        public User()
        {
        }
        public bool NameEmpty()
        {
            return this.Name == null || this.Name.Length == 0;
        }
        public bool SurnameEmpty()
        {
            return this.Surname == null || this.Surname.Length == 0;
        }
        public bool CellphoneEmpty()
        {
            return this.Cellphone == null ||  this.Cellphone.Length == 0;
        }
        public bool MeetingEmpty()
        {
            return this.Meeting == null || this.Meeting.Count == 0;
        }
        public bool MedicalConditionEmpty()
        {
            return this.MedicalCondition == null;
        }
    }
}
