using MSP.BetterCalm.BusinessLogicInterface;
using MSP.BetterCalm.DataAccessInterface;
using MSP.BetterCalm.Domain;
using MSP.BetterCalm.DTO;
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

        public List<CategoryDTO> GetAll()
        {
            var categories = _repository.GetAll().ToList();
            var categoriesDTO = categories.Select(u => new CategoryDTO { Id = u.Id, Name = u.Name }).ToList();
            return categoriesDTO;
        }
    }
}
