using Msp.BetterCalm.HandleMessage;
using MSP.BetterCalm.BusinessLogicInterface;
using MSP.BetterCalm.DataAccessInterface;
using MSP.BetterCalm.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MSP.BetterCalm.BusinessLogic
{
    public class MedicalConditionLogic : IMedicalConditionLogic
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
        public MedicalCondition Get(int id)
        {
            ExistMedicalCondition(id);
            return _repository.Get(id);
        }
        private void ExistMedicalCondition(int id)
        {
            MedicalCondition unMedicalCondition = _repository.Get(id);
            if (unMedicalCondition == null) throw new EntityNotExists("The medical condition with id: " + id + " does not exist");
        }
    }
}
