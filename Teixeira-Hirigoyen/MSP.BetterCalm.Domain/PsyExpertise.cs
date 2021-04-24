using System;
using System.Collections.Generic;
using System.Text;

namespace MSP.BetterCalm.Domain
{
    public class PsyExpertise
    {
        public int IdMedicalCondition { get; set; }
        public MedicalCondition MedicalCondition { get; set; }
        public int IdPsychologist { get; set; }
        public Psychologist Psychologist { get; set; }



    }
}
