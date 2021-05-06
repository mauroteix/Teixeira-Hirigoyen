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
    public class PsychologistRepository : IData<Psychologist>
    {
        readonly BetterCalmContext _context;

        public PsychologistRepository(BetterCalmContext context)
        {
            _context = context;
        }
        public void Add(Psychologist entity)
        {
            _context.Psychologist.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(Psychologist entity)
        {
            _context.Psychologist.Remove(_context.Psychologist.Find(entity.Id));
            _context.SaveChanges();
        }

        public Psychologist Get(int id)
        {
            return _context.Psychologist
               .Include(t => t.Meeting).ThenInclude(u => u.User)
               .Include(r => r.Expertise).ThenInclude(s => s.MedicalCondition)
               .FirstOrDefault(u => u.Id == id);         
        }

        public IEnumerable<Psychologist> GetAll()
        {
            return _context.Psychologist
                .Include(t => t.Meeting).ThenInclude(u => u.User)
                .Include(r => r.Expertise).ThenInclude(s => s.MedicalCondition)
                .ToList();
        }

        public void Update(Psychologist entity)
        {
            _context.Psychologist.Update(entity);
            _context.SaveChanges();
        }
    }
}
