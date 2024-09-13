using Demo.BusinessLogicLayer.Interfaces;
using Demo.DataAccessLayer.Data;
using Demo.DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BusinessLogicLayer.Repositories
{
    public class DepartmentRepository : GenericRepository<Department>, IDepartmentRepository //: IGenericRepository<Department>
    {

        public DepartmentRepository(DataContext dataContext):base(dataContext) 
        { 
        }

    }
}
