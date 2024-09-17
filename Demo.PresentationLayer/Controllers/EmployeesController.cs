using AutoMapper;
using Demo.BusinessLogicLayer.Interfaces;
using Demo.DataAccessLayer.Data;
using Demo.DataAccessLayer.Models;
using Demo.PresentationLayer.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Demo.PresentationLayer.Controllers
{
    public class EmployeesController : Controller
    {
        private IEmployeeRepository _repo;
        private IDepartmentRepository _repoDept;
        // private IGenericRepository<Employee> _repository; 
        // private readonly IEmployeeRepository _repository;

        private readonly IMapper _mapper;

        public EmployeesController(IEmployeeRepository repo, IDepartmentRepository departmentRepository, IMapper mapper)
        {
            _repo = repo;
            _repoDept = departmentRepository;
            _mapper = mapper;
        }



        [HttpGet]
        public IActionResult Index(string? SearchValue)
        {
            var employees = Enumerable.Empty<Employee>();
            if (string.IsNullOrWhiteSpace(SearchValue))
            {
                 employees = _repo.GetWithDepartment();
            }
           else
            {
                employees = _repo.GetAll(SearchValue);
            }
            
            var employeeViewModel = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(employees);

            return View(employeeViewModel);

        }


        public IActionResult Create()
        {
            var departments = _repoDept.GetAll();
            SelectList listItems = new SelectList(departments,"Id","Name");
            ViewBag.Departments = listItems;
            return View();
        }

        [HttpPost]
        public IActionResult Create(EmployeeViewModel employeeVM)
        {
            //Server Side Validation 
            var employee = _mapper.Map<EmployeeViewModel, Employee>(employeeVM);

            if (!ModelState.IsValid)
            {
                return View(employeeVM);
            }
            _repo.Create(employee);

            return RedirectToAction(nameof(Index));
        }
        public IActionResult Details(int? id) => EmployeeControllerHandler(id, nameof(Details));


        public IActionResult Edit(int? id) => EmployeeControllerHandler(id, nameof(Edit));

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int id, EmployeeViewModel employeVM)
        {
            if (id != employeVM.Id) { return BadRequest(); }
            //Server Side Validation 
            if (!ModelState.IsValid)
            {
                try
                {
                    var employee = _mapper.Map<EmployeeViewModel, Employee >(employeVM);
                    if (_repo.Update(employee) > 0)
                    {
                        TempData["Messege"] = $"Employee {employeVM.Name} Updated Succefully";
                    }
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }

            return View(employeVM);
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
            if(viewName== nameof(Edit))
            {
                var departments = _repoDept.GetAll();
                SelectList listItems = new SelectList(departments, "Id", "Name");
                ViewBag.Departments = listItems;
                
            }
            if (!id.HasValue)
            {
                return BadRequest();
            }
            var Employee = _repo.Get(id.Value);
            if (Employee is null) { return NotFound(); }
            var employeeVM = _mapper.Map<EmployeeViewModel>(Employee);
           
            return View(viewName, employeeVM);

        }
    }
}
