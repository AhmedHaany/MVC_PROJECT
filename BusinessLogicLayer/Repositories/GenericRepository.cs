using Demo.BusinessLogicLayer.Interfaces;
using Demo.DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BusinessLogicLayer.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        protected  DataContext _dataContext;

        public GenericRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public int Create(TEntity entity)
        {
            _dataContext.Set<TEntity>().Add(entity);
            return _dataContext.SaveChanges();
        }

        public int Delete(TEntity entity)
        {
            _dataContext.Set<TEntity>().Remove(entity);
            return _dataContext.SaveChanges();
        }

        public TEntity Get(int id) => _dataContext.Set<TEntity>().Find(id);

        public IEnumerable<TEntity> GetAll() => _dataContext.Set<TEntity>().ToList();

        public int Update(TEntity entity)
        {
            _dataContext.Set<TEntity>().Remove(entity);
            return _dataContext.SaveChanges();
        }
    }
}
