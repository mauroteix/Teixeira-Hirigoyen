using Microsoft.EntityFrameworkCore;
using MSP.BetterCalm.DataAccessInterface;
using MSP.BetterCalm.Domain;
using MSP.BetterCalm.HandleMessage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace MSP.BetterCalm.DataAccess
{
    public class MedicalConditionRepository : IData<MedicalCondition>
    {
        readonly BetterCalmContext _context;

        public MedicalConditionRepository(BetterCalmContext context)
        {
            _context = context;
        }

        public void Add(MedicalCondition entity)
        {
            _context.MedicalCondition.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(MedicalCondition entity)
        {
            throw new CannotBePerformed("You cannot delete medicalCondition " + entity.Name);
        }

        public MedicalCondition Get(int id)
        {
            return _context.MedicalCondition
                .Include(t => t.Expertise).ThenInclude(u => u.Psychologist)
               .FirstOrDefault(u => u.Id == id);
        }

        public IEnumerable<MedicalCondition> GetAll()
        {
            return _context.MedicalCondition
                .Include(t => t.Expertise).ThenInclude(u => u.Psychologist)
                .ToList();
        }

        public void Update(MedicalCondition entity)
        {
            _context.MedicalCondition.Update(entity);
            _context.SaveChanges();
        }
    }
}
