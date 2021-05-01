using MSP.BetterCalm.DataAccessInterface;
using MSP.BetterCalm.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MSP.BetterCalm.BusinessLogic
{
    public class MedicalConditionLogic
    {
        IData<MedicalCondition> _repository;
        public MedicalConditionLogic(IData<MedicalCondition> repository)
        {
            _repository = repository;
        }
        public List<MedicalCondition> GetAll()
        {
            return _repository.GetAll().ToList();
        }
    }
}
