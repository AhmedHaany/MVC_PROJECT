using Demo.BusinessLogicLayer.Interfaces;
using Demo.DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BusinessLogicLayer.Repositories
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository //: IGenericRepository<Employee>
    {
        public EmployeeRepository(DataContext dataContext):base(dataContext)
        { }
        public IEnumerable<Employee> GetAll(string Address)
        {
            return _dataContext.Set<Employee>().Where(e=>e.Address.ToLower() == Address.ToLower()).ToList();
        
        }
    }
}
