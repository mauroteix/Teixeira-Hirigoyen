using MSP.BetterCalm.DataAccessInterface;
using MSP.BetterCalm.Domain;
using MSP.BetterCalm.HandleMessage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MSP.BetterCalm.DataAccess
{
    public class CategoryRepository : IData<Category>
    {
        readonly BetterCalmContext _context;

        public CategoryRepository(BetterCalmContext context)
        {
            _context = context;
        }
        public void Add(Category entity)
        {
            _context.Category.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(Category entity)
        {
            throw new CannotBePerformed("You cannot delete category " + entity.Name);
        }

        public Category Get(int id)
        {
            return _context.Category.FirstOrDefault(u => u.Id == id);
        }

        public IEnumerable<Category> GetAll()
        {
            return _context.Category.ToList();
        }

        public void Update(Category entity)
        {
            throw new NotImplementedException();
        }
    }
}
