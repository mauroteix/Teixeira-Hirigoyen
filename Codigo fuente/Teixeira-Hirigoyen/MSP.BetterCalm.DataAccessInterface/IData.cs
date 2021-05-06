using System;
using System.Collections.Generic;

namespace MSP.BetterCalm.DataAccessInterface
{
    public interface IData<T>
    {
        IEnumerable<T> GetAll();
        T Get(int id);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
