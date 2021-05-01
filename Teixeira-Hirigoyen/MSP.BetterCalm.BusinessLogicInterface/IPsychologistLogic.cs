using MSP.BetterCalm.Domain;
using MSP.BetterCalm.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace MSP.BetterCalm.BusinessLogicInterface
{
    public interface IPsychologistLogic
    {
        public Psychologist Get(int id);
        public void Add(Psychologist psychologist);
        public List<Psychologist> GetAll();
        public void Delete(Psychologist psychologist);
        public void Update(Psychologist psychologist, int id);
    }
}
