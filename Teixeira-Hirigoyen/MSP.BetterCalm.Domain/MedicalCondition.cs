using System;
using System.Collections.Generic;
using System.Text;

namespace MSP.BetterCalm.Domain
{
    public class MedicalCondition
    {
        public int Id { get; set; }
        public string Name { get; set; }

        
        public ICollection<Expertise> Expertise { get; set; }
        public override bool Equals(object obj)
        {
            if (!this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                MedicalCondition medicalCondition = (MedicalCondition)obj;
                return (this.Id == medicalCondition.Id);
            }
        }
    }

}
