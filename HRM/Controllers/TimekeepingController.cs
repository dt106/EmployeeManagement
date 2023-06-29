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
    public class TimekeepingController : Controller
    {
        // GET: Timekeeping
        public async Task<ActionResult> Timekeeping()
        {
            List<LogMonth> lst = new List<LogMonth>();
            Debug.WriteLine("LogMonth called");
            var employeeRepository = HttpContext.ApplicationInstance.Application["EmployeeRepository"] as IEmployeeRepository;
            lst = await employeeRepository.GetAllLogMonth();
            return View(lst);
        }
        public async Task<ActionResult> Detail()
        {
            List<DailyLog> lst = new List<DailyLog>();
            Debug.WriteLine("LogMonth called");
            var employeeRepository = HttpContext.ApplicationInstance.Application["EmployeeRepository"] as IEmployeeRepository;
            lst = await employeeRepository.GetAllDailyLog();
            return View(lst);
        }
        public async Task<ActionResult> Compensatory(Timekeeping timekeeping)
        {
            Debug.WriteLine("Compensatory called"+ timekeeping.EmployeeID+timekeeping.LogDate+timekeeping.LogTime);
            var employeeRepository = HttpContext.ApplicationInstance.Application["EmployeeRepository"] as IEmployeeRepository;
            var a = await employeeRepository.TimekeepingCompensatory(timekeeping);
            if(a>0)
            {
                Debug.WriteLine("Compensatory Successful");

                var b = await employeeRepository.TimeWorking(timekeeping.LogDate);
                if (b > 0)
                {
                    Debug.WriteLine("Calculate Successful");
                    return RedirectToAction("Detail");
                }
            }
            return ViewBag.Message = "Lỗi";

        }
        public async Task<ActionResult> UpdateStatus(Timekeeping timekeeping)
        {
            Debug.WriteLine("Calculate Successful" + timekeeping.LogDate.Year+timekeeping.LogDate.Month);

            var employeeRepository = HttpContext.ApplicationInstance.Application["EmployeeRepository"] as IEmployeeRepository;
            var a = await employeeRepository.UpdateLogMonth(timekeeping.LogDate.Year,timekeeping.LogDate.Month,timekeeping.LogDate.Day);
            if(a>0)
            {
                Debug.WriteLine("Calculate Successful");
                return RedirectToAction("Timekeeping");
            }
            if(a == -1)
            {
                Debug.WriteLine("Lỗi cập nhật");

            }
            return ViewBag.Message = "Không thành công";

        }
        public async Task<ActionResult> Salary()
        {
            var employeeRepository = HttpContext.ApplicationInstance.Application["EmployeeRepository"] as IEmployeeRepository;
            var lst =await employeeRepository.GetSalaries();
            return View(lst);
        }
        public async Task<ActionResult> LogMonthInsert(LogMonth logMonth)
        {
            Debug.WriteLine("Insert Called" + logMonth.Year+logMonth.Month);

            var employeeRepository = HttpContext.ApplicationInstance.Application["EmployeeRepository"] as IEmployeeRepository;
            var res = await employeeRepository.LogMonthInsert(logMonth);
            if (res > 0)
            {
                return Json(new { Success = true, Message = "Thành công" });
            }
            else
            {
                if (res == -10)
                {
                    return Json(new { Success = false, Message = "Dữ liệu không hợp lệ" });
                }
                return Json(new { Success = false, Message = "Lỗi không xác định" });
            }
            throw new Exception("Lỗi");
        }
    }
}