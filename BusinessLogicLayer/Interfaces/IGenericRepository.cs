using Demo.DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BusinessLogicLayer.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
       void Create(T entity);
        void Delete(T entity);
        T Get(int id);
        IEnumerable<T> GetAll();
        void Update(T entity);

    }
}
