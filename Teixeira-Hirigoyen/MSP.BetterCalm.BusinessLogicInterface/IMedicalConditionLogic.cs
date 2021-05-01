﻿using MSP.BetterCalm.Domain;
using MSP.BetterCalm.DTO;
using System;
using System.Collections.Generic;
using System.Text;


namespace MSP.BetterCalm.BusinessLogicInterface
{
    public interface IMedicalConditionLogic
    {
        List<MedicalCondition> GetAll();
        public MedicalCondition Get(int id);
    }
}
