using Demo.BusinessLogicLayer.Interfaces;
using Demo.DataAccessLayer.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
 
namespace Demo.PresentationLayer.Controllers
{
    public class DepartmentsController : Controller
    {
        private IDepartmentRepository _repo;
       // private IGenericRepository<Department> _repository; 
       // private readonly IDepartmentRepository _repository;

        public DepartmentsController(IDepartmentRepository repo)
        {
            _repo = repo;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var departments = _repo.GetAll();
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
            _repo.Create(department);

            return RedirectToAction(nameof(Index));
        }
        public IActionResult Details(int? id) => DepartmentControllerHandler(id, nameof(Details));
       

        public IActionResult Edit(int? id) => DepartmentControllerHandler(id, nameof(Edit));

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int id,  Department department)
        {
            if(id!= department.Id) { return BadRequest(); }
            //Server Side Validation 
            if (!ModelState.IsValid)
            {
                try
                {
                    _repo.Update(department);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("",ex.Message);
                }
            }
    
            return View(department);
        }

        public IActionResult Delete(int? id) => DepartmentControllerHandler(id , nameof(Delete));


        [HttpPost]
        [ActionName("Delete")]
        public IActionResult ConfirmDelete(int? id)
        {
            if (!id.HasValue) return BadRequest();
            var department = _repo.Get(id.Value);
            if (department is null) return NotFound();

            try
            {
                _repo.Delete(department);
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

        private IActionResult DepartmentControllerHandler(int? id , string viewName)
        {
            if (!id.HasValue)
            {
                return BadRequest();
            }
            var department = _repo.Get(id.Value);
            if (department is null) { return NotFound(); }
            return View(viewName ,department);

        }
    }

}
