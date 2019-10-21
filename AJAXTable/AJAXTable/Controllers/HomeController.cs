using AJAXTable.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace AJAXTable.Controllers
{
    public class HomeController : Controller
    {
        List<EmployeeModel> listEmployee = new List<EmployeeModel>()
        {
            new EmployeeModel()
            {
                ID = 1,
                Name = "Nguyen Van A",
                Salary = 2000000,
                Status = true
            },
            new EmployeeModel()
            {
                 ID = 2,
                Name = "Nguyen Van B",
                Salary = 3000000,
                Status = true
            },
            new EmployeeModel()
            {
                ID = 3,
                Name = "Nguyen Van C",
                Salary = 4000000,
                Status = true
            },
            new EmployeeModel()
            {
                 ID = 4,
                Name = "Nguyen Van D",
                Salary = 5000000,
                Status = true
            },
            new EmployeeModel()
            {
                ID = 5,
                Name = "Nguyen Van E",
                Salary = 6000000,
                Status = true
            },
            new EmployeeModel()
            {
                 ID = 6,
                Name = "Nguyen Van F",
                Salary = 7000000,
                Status = true
            },
            new EmployeeModel()
            {
                ID = 7,
                Name = "Nguyen Van G",
                Salary = 8000000,
                Status = true
            },
            new EmployeeModel()
            {
                 ID = 8,
                Name = "Nguyen Van H",
                Salary = 9000000,
                Status = true
            },
            new EmployeeModel()
            {
                ID = 9,
                Name = "Nguyen Van I",
                Salary = 10000000,
                Status = true
            },
            new EmployeeModel()
            {
                 ID = 10,
                Name = "Nguyen Van K",
                Salary = 1100000,
                Status = true
            },
            new EmployeeModel()
            {
                ID = 11,
                Name = "Nguyen Van L",
                Salary = 1200000,
                Status = true
            }

        };

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult LoadData(int page, int pageSize = 3)
        {
            var model = listEmployee.Skip((page - 1) * pageSize).Take(pageSize);
            int totalRow = listEmployee.Count;


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
            EmployeeModel employeeNew = js.Deserialize<EmployeeModel>(model);

            //Save database
            var employee = listEmployee.SingleOrDefault(x => x.ID == employeeNew.ID);
            employee.Salary = employeeNew.Salary;

            return Json(new
            {
                status = true
            }, JsonRequestBehavior.AllowGet);
        }
    }
}


