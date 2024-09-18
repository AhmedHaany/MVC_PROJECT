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

        public GenericRepository(DataContext dataContext) => _dataContext = dataContext;
        public void Create(TEntity entity) => _dataContext.Set<TEntity>().Add(entity);

        public void Delete(TEntity entity) => _dataContext.Set<TEntity>().Remove(entity);

        public TEntity Get(int id) => _dataContext.Set<TEntity>().Find(id);

        public IEnumerable<TEntity> GetAll() => _dataContext.Set<TEntity>().ToList();

        public void Update(TEntity entity) => _dataContext.Set<TEntity>().Remove(entity);
    }
}
