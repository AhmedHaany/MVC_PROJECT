using Demo.BusinessLogicLayer.Repositories;
using Demo.DataAccessLayer.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Demo.PresentationLayer.Controllers
{
    public class DepartmentsController : Controller
    {
        private readonly IDepartmentRepository _repository;

        public DepartmentsController(IDepartmentRepository departmentRepository)
        {
            _repository = departmentRepository;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var departments = _repository.GetAll();
            //Retrieve All Departments 
            return View(departments);
        }


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Department department)
        {
            //Server Side Validation 
            if(!ModelState.IsValid)
            {
                return View(department);
            }
            _repository.Create(department);

            return RedirectToAction(nameof(Index));
        }
        public IActionResult Details(int? id)
        {
            if(!id.HasValue)
            {
                return BadRequest();
            }
            var department = _repository.Get(id.Value);
            if(department is null) { return NotFound(); }
            return View(department);



        }

        public IActionResult Edit(int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest();
            }
            var department = _repository.Get(id.Value);
            if (department is null) { return NotFound(); }
            return View(department);
        }
        [HttpPost]
        public IActionResult Edit([FromRoute] int id,  Department department)
        {
            if(id!= department.Id) { return BadRequest(); }
            //Server Side Validation 
            if (!ModelState.IsValid)
            {
                try
                {
                    _repository.Update(department);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("",ex.Message);
                }
            }
    
            return View(department);
        }

        public IActionResult Delete(int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest();
            }
            var department = _repository.Get(id.Value);
            if (department is null) { return NotFound(); }
            return View(department);
        }

        [HttpPost]
        [ActionName("Delete")]
        public IActionResult ConfirmDelete(int? id)
        {
            if (!id.HasValue) return BadRequest();
            var department = _repository.Get(id.Value);
            if (department is null) return NotFound();

            try
            {
                _repository.Delete(department);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            return View(department);
        }
    }
}
