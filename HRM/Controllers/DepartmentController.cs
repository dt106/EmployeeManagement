using HRM.DTO;
using HRM.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace HRM.Controllers
{
    public class DepartmentController : Controller
    {
        // GET: Department
        public async Task<ActionResult> Department()
        {
            var employeeRepository = HttpContext.ApplicationInstance.Application["EmployeeRepository"] as IEmployeeRepository;
            var empde = await employeeRepository.GetDepartments();
            return View(empde);
        }
        public async Task<ActionResult> DepartmentInsert(Department department)
        {
            var employeeRepository = HttpContext.ApplicationInstance.Application["EmployeeRepository"] as IEmployeeRepository;
            var result = await employeeRepository.DepartmentInsert(department);
            if (result > 0)
            {
                return Json(new { Success = true, Message = "Thêm phòng ban thành công." });
            }
            else
            {
               
                    return Json(new { Success = false, Message = "Không thành công" });

            }
            throw new Exception("Phương thức Insert sẽ không bao giờ thực thi tới đây.");
        }
    }
}