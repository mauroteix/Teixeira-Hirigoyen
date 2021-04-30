using MSP.BetterCalm.BusinessLogicInterface;
using MSP.BetterCalm.DataAccessInterface;
using MSP.BetterCalm.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MSP.BetterCalm.BusinessLogic
{
    public class PsychologistLogic : IPsychologistLogic
    {
        IData<Psychologist> _repository;
        public PsychologistLogic(IData<Psychologist> repository)
        {
            _repository = repository;

        }
        public void Add(Psychologist psychologist)
        {
            _repository.Add(psychologist);
        }

        public void Delete(Psychologist psychologist)
        {
            _repository.Delete(psychologist);
        }

        public Psychologist Get(int id)
        {
            return _repository.Get(id);
        }

        public List<Psychologist> GetAll()
        {
            return _repository.GetAll().ToList();
        }

        public void Update(Psychologist psychologist, int id)
        {
            throw new NotImplementedException();
        }
    }
}
