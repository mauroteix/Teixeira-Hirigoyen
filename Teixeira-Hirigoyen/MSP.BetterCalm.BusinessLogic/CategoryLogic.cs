using Msp.BetterCalm.HandleMessage;
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
            var categoriesDTO = categories.Select(u => new CategoryDTO { Id = u.Id, Name = u.Name ,CategoryTrack = u.CategoryTrack, PlaylistCategory = u.PlaylistCategory }).ToList();
            return categoriesDTO;
        }

        public CategoryDTO Get(int id)
        {
            ExistCategory(id);
            var category = _repository.Get(id);
            var categoryDTO = new CategoryDTO(category.Id, category.Name, category.CategoryTrack, category.PlaylistCategory);
            return categoryDTO;
        }

        private void ExistCategory(int id)
        {
            Category unCategory = _repository.Get(id);
            if (unCategory == null) throw new EntityNotExists("The playlist with id: " + id + " does not exist");
        }
    }
}
