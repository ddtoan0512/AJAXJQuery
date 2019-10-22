using AJAXTable.Data;
using AJAXTable.Data.Models;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace AJAXTable.Controllers
{
    public class HomeController : Controller
    {
        private EmployeeDbContext _context;
        public HomeController()
        {
            _context = new EmployeeDbContext();
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult LoadData(string name, string status, int page, int pageSize = 3)
        {
            IQueryable<Employee> model = _context.Employees;

            if (!string.IsNullOrEmpty(name))
                model = model.Where(x => x.Name.Contains(name));
            if (!string.IsNullOrEmpty(status))
            {
                var statusBool = bool.Parse(status);
                model = model.Where(x => x.Status == statusBool);
            }

            int totalRow = model.Count();

            model = model.OrderByDescending(x => x.CreatedDate)
            .Skip((page - 1) * pageSize)
            .Take(pageSize);



            return Json(new
            {
                data = model,
                total = totalRow,
                status = true
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetDetail(int id)
        {
            var employee = _context.Employees.Find(id);

            return Json(new
            {
                data = employee,
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
            _context.SaveChanges();

            return Json(new
            {
                status = true
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Delete(int id)
        {
            var employee = _context.Employees.Find(id);
            _context.Employees.Remove(employee);


            try
            {
                _context.SaveChanges();
                return Json(new
                {
                    status = true
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    status = false,
                    message = ex.Message
                }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult SaveData(string strEmployee)
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            Employee employee = js.Deserialize<Employee>(strEmployee);

            bool status = false;
            string Message = string.Empty;

            // Add new Employee if not exist (id = 0)
            if (employee.ID == 0)
            {
                employee.CreatedDate = DateTime.Now;
                _context.Employees.Add(employee);
                try
                {
                    _context.SaveChanges();
                    status = true;
                }
                catch (Exception e)
                {
                    status = false;
                    Message = e.Message;
                }
            }
            else
            {
                //Update existing Employee
                var entity = _context.Employees.Find(employee.ID);
                entity.Salary = employee.Salary;
                entity.Name = employee.Name;
                entity.Status = employee.Status;

                try
                {
                    _context.SaveChanges();
                    status = true;
                }
                catch (Exception e)
                {
                    status = false;
                    Message = e.Message;
                }
            }

            return Json(new
            {
                status = status,
                message = Message
            }, JsonRequestBehavior.AllowGet);
        }
    }
}


