using AJAXTable.Data;
using AJAXTable.Data.Models;
using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace AJAXTable.Controllers
{
    public class HomeController : Controller
    {
        private EmployeeDbContext _context;
        public HomeController() {
            _context = new EmployeeDbContext();
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult LoadData(int page, int pageSize = 3)
        {
            var model = _context.Employees
                .OrderBy(x => x.CreatedDate)
                .Skip((page - 1) * pageSize)
                .Take(pageSize);

            int totalRow = _context.Employees.Count();


            return Json(new
            {
                data = model,
                total = totalRow,
                status = true
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Update(string model)
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            Employee employeeNew = js.Deserialize<Employee>(model);

            //Save database
            var employee = _context.Employees.Find(employeeNew.ID);
            employee.Salary = employeeNew.Salary;

            return Json(new
            {
                status = true
            }, JsonRequestBehavior.AllowGet);
        }
    }
}


