using Demo.BusinessLogicLayer.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Demo.PresentationLayer.Controllers
{
    public class DepartmentsController : Controller
    {
        private readonly IDepartmentRepository DepartmentRepository;

        public DepartmentsController(IDepartmentRepository departmentRepository)
        {
            DepartmentRepository = departmentRepository;
        }

        public IActionResult Index()
        {

            
            //Retrieve All Departments 
            return View();
        }
    }
}
