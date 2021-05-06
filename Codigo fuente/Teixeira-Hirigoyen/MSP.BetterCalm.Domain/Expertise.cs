using System;
using System.Collections.Generic;
using System.Text;

namespace MSP.BetterCalm.Domain
{
    public class Expertise
    {
        public int IdMedicalCondition { get; set; }
        public MedicalCondition MedicalCondition { get; set; }
        public int IdPsychologist { get; set; }
        public Psychologist Psychologist { get; set; }

        public override bool Equals(object obj)
        {
            if (!this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                Expertise expertise = (Expertise)obj;
                return (this.IdMedicalCondition == expertise.IdMedicalCondition && this.IdPsychologist == expertise.IdPsychologist);
            }
        }

    }
}
