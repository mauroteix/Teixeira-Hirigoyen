﻿using MSP.BetterCalm.DataAccessInterface;
using MSP.BetterCalm.Domain;
using MSP.BetterCalm.HandleMessage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MSP.BetterCalm.DataAccess
{
    public class UserRepository : IData<User>
    {
        readonly BetterCalmContext _context;

        public UserRepository(BetterCalmContext context)
        {
            _context = context;
        }
        public void Add(User entity)
        {
           _context.User.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(User entity)
        {
            throw new CannotBePerformed("You cannot delete user " + entity.Name);
        }

        public User Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetAll()
        {
            return _context.User.ToList();
        }

        public void Update(User entity)
        {
            throw new NotImplementedException();
        }
    }
}