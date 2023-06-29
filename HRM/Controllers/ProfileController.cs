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
    public class ProfileController : Controller
    {
        // GET: Profile

        public async Task<ActionResult> Profile()
        {
            Debug.WriteLine("Profile called");
            //var UserName = Session["UserName"].ToString();
            var EmployeeId = (int)Session["EmployeeId"];
            var employeeRepository = HttpContext.ApplicationInstance.Application["EmployeeRepository"] as IEmployeeRepository;
            var lstEmpHis = await employeeRepository.GetHistoriesById(EmployeeId);
            var lstSal = await employeeRepository.GetSalariesByEmployeeId(EmployeeId);
            var employee = await employeeRepository.GetEmployeeById(EmployeeId);
            var employeeinfo = new EmployeeInfo
            {
                employee = employee,
                employeeHistories = lstEmpHis,
                salaryFixes = lstSal
            };
            Debug.WriteLine("Hello " + employee.EmployeeName);

            return View(employeeinfo);
        }
        public ActionResult Demo()
        {
            return View();
        }
        public async Task<ActionResult> WorkStatus()
        {
            var EmployeeId = (int)Session["EmployeeId"];
            var employeeRepository = HttpContext.ApplicationInstance.Application["EmployeeRepository"] as IEmployeeRepository;
            var logmonth =await employeeRepository.GetById(EmployeeId);
            return View(logmonth);
        }
    }
}