using HRM.DTO;
using HRM.IRepositories;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace HRM.Controllers
{
    public class HomeController : Controller
    {

        
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            Debug.WriteLine("Index called");

            var employeeRepository = HttpContext.ApplicationInstance.Application["EmployeeRepository"] as IEmployeeRepository;
            var log = await employeeRepository.GetAllLog();
            List<TimekeepingEmp> lst = new List<TimekeepingEmp>();
            lst = log;

            return View(lst);
        }

        public  async Task<ActionResult> CheckIn()
        {
            //var UserName = Session["UserName"].ToString();
            var EmployeeId = (int)Session["EmployeeId"];
            var employeeRepository = HttpContext.ApplicationInstance.Application["EmployeeRepository"] as IEmployeeRepository;
            var result = await employeeRepository.InsertLog(EmployeeId);

            if (result > 0)
            {
                var a = await employeeRepository.TimeWorking(DateTime.Now);
                Debug.WriteLine("Check In Sucessful");

                return RedirectToAction("Index");
            }
            return ViewBag.Message="Thử lại!";
        }
    }
}