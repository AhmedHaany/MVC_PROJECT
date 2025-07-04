﻿using Demo.DataAccessLayer.Data;

namespace Demo.BusinessLogicLayer.Repositories
{
    public interface IDepartmentRepository
    {
        int Create(Department entity);
        int Delete(Department entity);
        Department Get(int id);
        IEnumerable<Department> GetAll();
        int Update(Department entity);
    }
}