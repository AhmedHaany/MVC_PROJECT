using Demo.BusinessLogicLayer.Interfaces;
using Demo.DataAccessLayer.Data;
using Demo.DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;

namespace Demo.PresentationLayer.Controllers
{
    public class EmployeesController : Controller
    {
        private IEmployeeRepository _repo;
        // private IGenericRepository<Employee> _repository; 
        // private readonly IEmployeeRepository _repository;

        public EmployeesController(IEmployeeRepository repo)
        {
            _repo = repo;
        }


        
        [HttpGet]
        public IActionResult Index()
        {
           //  ViewData["Message"] = new Employee {Name ="Abdallah" }  ;
            var employees = _repo.GetAll();
            //Retrieve All Employees 
            return View(employees);
        }


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Employee employee)
        {
            //Server Side Validation 
            if (!ModelState.IsValid)
            {
                return View(employee);
            }
            _repo.Create(employee);

            return RedirectToAction(nameof(Index));
        }
        public IActionResult Details(int? id) => EmployeeControllerHandler(id, nameof(Details));


        public IActionResult Edit(int? id) => EmployeeControllerHandler(id, nameof(Edit));

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int id, Employee employee)
        {
            if (id != employee.Id) { return BadRequest(); }
            //Server Side Validation 
            if (!ModelState.IsValid)
            {
                try
                {
                    _repo.Update(employee);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }

            return View(employee);
        }

        public IActionResult Delete(int? id) => EmployeeControllerHandler(id, nameof(Delete));


        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult ConfirmDelete(int? id)
        {
            if (!id.HasValue) return BadRequest();
            var employee = _repo.Get(id.Value);
            if (employee == null) return NotFound();

            try
            {
                _repo.Delete(employee);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(employee); // Return the same view in case of an error
            }
        }
        private IActionResult EmployeeControllerHandler(int? id, string viewName)
        {
            if (!id.HasValue)
            {
                return BadRequest();
            }
            var Employee = _repo.Get(id.Value);
            if (Employee is null) { return NotFound(); }
            return View(viewName, Employee);

        }
    }
}
