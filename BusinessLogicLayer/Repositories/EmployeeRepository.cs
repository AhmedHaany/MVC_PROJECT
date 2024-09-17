using Demo.BusinessLogicLayer.Interfaces;
using Demo.DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BusinessLogicLayer.Repositories
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository //: IGenericRepository<Employee>
    {
        public EmployeeRepository(DataContext dataContext):base(dataContext)
        { }
        public IEnumerable<Employee> GetAll(string name)
        {
            return _dataContext.Set<Employee>().Where(e=>e.Name.ToLower().Contains(name.ToLower())).Include(e=>e.Department).ToList();
        
        }

        public IEnumerable<Employee> GetWithDepartment()
        {
            return _dataContext.Set<Employee>().Include(e => e.Department).ToList();

        }
    }
}
