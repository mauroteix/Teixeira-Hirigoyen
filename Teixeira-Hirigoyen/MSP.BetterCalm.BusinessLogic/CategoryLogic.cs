using MSP.BetterCalm.BusinessLogicInterface;
using MSP.BetterCalm.DataAccessInterface;
using MSP.BetterCalm.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MSP.BetterCalm.BusinessLogic
{
    public class CategoryLogic : ICategoryLogic
    {
        IData<Category> _repository;
        public CategoryLogic(IData<Category> repository)
        {
            _repository = repository;
        }

        public List<Category> GetAll()
        {
            return _repository.GetAll().ToList();
        }
    }
}
