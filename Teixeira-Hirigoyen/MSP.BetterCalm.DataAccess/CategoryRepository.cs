using MSP.BetterCalm.DataAccessInterface;
using MSP.BetterCalm.Domain;
using System;
using System.Collections.Generic;
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
            throw new NotImplementedException();
        }

        public void Delete(Category entity)
        {
            throw new NotImplementedException();
        }

        public Category Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Category> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Update(Category entity)
        {
            throw new NotImplementedException();
        }
    }
}
