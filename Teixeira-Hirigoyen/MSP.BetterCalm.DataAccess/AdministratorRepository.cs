using MSP.BetterCalm.DataAccessInterface;
using MSP.BetterCalm.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MSP.BetterCalm.DataAccess
{
    public class AdministratorRepository : IData<Administrator>
    {
        readonly BetterCalmContext _context;

        public AdministratorRepository(BetterCalmContext context)
        {
            _context = context;
        }

        public void Add(Administrator entity)
        {
            _context.Administrator.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(Administrator entity)
        {
            _context.Administrator.Remove(_context.Administrator.Find(entity.Id));
            _context.SaveChanges();
        }

        public Administrator Get(int id)
        {
            return _context.Administrator
                .FirstOrDefault(u => u.Id == id);
        }

        public IEnumerable<Administrator> GetAll()
        {
            return _context.Administrator.ToList();
        }

        public void Update(Administrator entity)
        {
            throw new NotImplementedException();
        }
    }
}
