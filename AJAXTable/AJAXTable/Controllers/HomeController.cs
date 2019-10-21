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
            }

        };

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult LoadData()
        {
            return Json(new
            {
                data = listEmployee,
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


