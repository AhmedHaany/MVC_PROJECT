using AutoMapper;
using Demo.BusinessLogicLayer.Interfaces;
using Demo.DataAccessLayer.Data;
using Demo.DataAccessLayer.Models;
using Demo.PresentationLayer.Utilities;
using Demo.PresentationLayer.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Demo.PresentationLayer.Controllers
{
    public class EmployeesController : Controller
    {
        
        // private IGenericRepository<Employee> _repository; 
        // private readonly IEmployeeRepository _repository;

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public EmployeesController( IUnitOfWork unitOfWork,IMapper mapper)
        {
         
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }



        [HttpGet]
        public IActionResult Index(string? SearchValue)
        {
            var employees = Enumerable.Empty<Employee>();
            if (string.IsNullOrWhiteSpace(SearchValue))
            {
                 employees = _unitOfWork.Employees.GetWithDepartment();
            }
           else
            {
                employees = _unitOfWork.Employees.GetAll(SearchValue);
            }
            
            var employeeViewModel = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(employees);

            return View(employeeViewModel);

        }

        
        public IActionResult Create()
        {
            var departments = _unitOfWork.Department.GetAll();
            SelectList listItems = new SelectList(departments,"Id","Name");
            ViewBag.Departments = listItems;
            return View();
        }

        [HttpPost]
        public IActionResult Create(EmployeeViewModel employeeVM)
        {
            //Server Side Validation 

            if (!ModelState.IsValid)
            {
                return View(employeeVM);
            }
            if(employeeVM.Image is not null)
                employeeVM.ImageName = DocumentSettings.UploadFile(employeeVM.Image, "Image");

            var employee = _mapper.Map<EmployeeViewModel, Employee>(employeeVM);

            _unitOfWork.Employees.Create(employee);
            _unitOfWork.SaveChanges();


            return RedirectToAction(nameof(Index));
        }
        public IActionResult Details(int? id) => EmployeeControllerHandler(id, nameof(Details));


        public IActionResult Edit(int? id) => EmployeeControllerHandler(id, nameof(Edit));

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int id, EmployeeViewModel employeeVM)
        {
            if (id != employeeVM.Id) { return BadRequest(); }
            //Server Side Validation 
            if (!ModelState.IsValid)
            {
                try
                {
                    if (employeeVM.Image is not null)
                        employeeVM.ImageName = DocumentSettings.UploadFile(employeeVM.Image, "Image");

                    var employee = _mapper.Map<EmployeeViewModel, Employee >(employeeVM);
                    _unitOfWork.Employees.Update(employee);
                    if (_unitOfWork.SaveChanges() > 0)
                    {
                        TempData["Messege"] = $"Employee {employeeVM.Name} Updated Succefully";
                    }
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }

            return View(employeeVM);
        }

        public IActionResult Delete(int? id) => EmployeeControllerHandler(id, nameof(Delete));


        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult ConfirmDelete(int? id)
        {
            if (!id.HasValue) return BadRequest();
            var employee = _unitOfWork.Employees.Get(id.Value);
            if (employee == null) return NotFound();

            try
            {
                _unitOfWork.Employees.Delete(employee);
                _unitOfWork.SaveChanges();
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
                var departments = _unitOfWork.Department.GetAll();
                SelectList listItems = new SelectList(departments, "Id", "Name");
                ViewBag.Departments = listItems;
                
            }
            if (!id.HasValue)
            {
                return BadRequest();
            }
            var Employee = _unitOfWork.Employees.Get(id.Value);
            if (Employee is null) { return NotFound(); }
            var employeeVM = _mapper.Map<EmployeeViewModel>(Employee);
           
            return View(viewName, employeeVM);

        }
    }
}
