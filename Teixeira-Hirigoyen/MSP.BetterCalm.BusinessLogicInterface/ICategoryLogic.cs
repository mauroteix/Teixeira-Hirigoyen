﻿using MSP.BetterCalm.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace MSP.BetterCalm.BusinessLogicInterface
{
    public interface ICategoryLogic
    {
        List<Category> GetAll();
    }
}