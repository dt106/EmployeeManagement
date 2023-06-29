using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using HRM.IRepositories;
using HRM.Repositories;
using HRM.DTO;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace HRM.Controllers
{
    public class EmployeeController : Controller
    {



   
        public async Task<ActionResult> Employee()
        {
            Debug.WriteLine("Employee called");

            var employeeRepository = HttpContext.ApplicationInstance.Application["EmployeeRepository"] as IEmployeeRepository;
            var employees = await employeeRepository.GetAllEmployee();
            var lstPos = await employeeRepository.GetPositions();
            var lstJob = await employeeRepository.GetJobtitles();
            ViewBag.Jobtitles = new SelectList(lstJob, "JobtitleId", "JobtitleName");
            ViewBag.Positions = new SelectList(lstPos, "PositionId", "PositionName");
            ListEmployee lstemp = new ListEmployee { 
                Employees = employees
            };
            
            return View(lstemp.Employees);
        }
        public async Task<ActionResult> Insert(Employee employee)
        {
            Debug.WriteLine("Insert"+employee.EmployeeName+employee.Birthday+employee.TimeIn);
            var employeeRepository = HttpContext.ApplicationInstance.Application["EmployeeRepository"] as IEmployeeRepository;
            var emp = await employeeRepository.Insert(employee);
            Debug.WriteLine("Response Insert " + emp);

            if (emp > 0)
            {
                return Json(new { Success = true, Message = "Thêm employee thành công." });
            }
            else
            {
                if (emp == -60)
                    return Json(new { Success = false, Message = "Dữ liệu cần nhập đầy đủ" });
                else if (emp == -55)
                    return Json(new { Success = false, Message = "CCCD  đã đăng kí cho người khác" });
                else if (emp == -63)
                    return Json(new { Success = false, Message = "Email đã tồn tại" });
                else if (emp == -61)
                    return Json(new { Success = false, Message = "Email sai định dạng" });
                else if (emp == -99)
                    return Json(new { Success = false, Message = "Lỗi không xác định" });

            }
            throw new Exception("Phương thức Insert sẽ không bao giờ thực thi tới đây.");
        }
        public async Task<ActionResult> DeleteEmployee(int EmployeeId)
        {
            var employeeRepository = HttpContext.ApplicationInstance.Application["EmployeeRepository"] as IEmployeeRepository;
            var employee = new Employee { EmployeeId = EmployeeId };
            var result = await employeeRepository.DeleteEmployee(employee);
            
             System.Diagnostics.Debug.WriteLine("Xoá thành công");
 
             return RedirectToAction("Employee");
            
                
        }
        public async Task<ActionResult> GetEmployeeById(int EmployeeId)
        {
            var employeeRepository = HttpContext.ApplicationInstance.Application["EmployeeRepository"] as IEmployeeRepository;
            var lstde = await employeeRepository.GetDepartments();
            var lstPos = await employeeRepository.GetPositions();
            var lstJob = await employeeRepository.GetJobtitles();
            var employee = await employeeRepository.GetEmployeeById(EmployeeId);
            ViewBag.Jobtitles = new SelectList(lstJob, "JobtitleId", "JobtitleName", employee.JobtitleId);
            ViewBag.Positions = new SelectList(lstPos, "PositionId", "PositionName", employee.PositionId);
            ViewBag.Departments = new SelectList(lstde, "DepartmentId", "DepartmentName",employee.DepartmentId);
            return View("Update",employee);  // Trả về view update và truyền đối tượng Employee cho view
        }
        public async Task<ActionResult> EmployeeInfo(int EmployeeId)
        {
            var employeeRepository = HttpContext.ApplicationInstance.Application["EmployeeRepository"] as IEmployeeRepository;
            var lstde = await employeeRepository.GetDepartments();
            var lstPos = await employeeRepository.GetPositions();
            var lstJob = await employeeRepository.GetJobtitles();
            var lstEmpHis = await employeeRepository.GetHistoriesById(EmployeeId);
            var lstSal = await employeeRepository.GetSalariesByEmployeeId(EmployeeId);
            var employee = await employeeRepository.GetEmployeeById(EmployeeId);
            var employeeinfo = new EmployeeInfo
            {
                employee = employee,
                employeeHistories = lstEmpHis,
                salaryFixes = lstSal
            };
            ViewBag.Jobtitles = new SelectList(lstJob, "JobtitleId", "JobtitleName", employee.JobtitleId);
            ViewBag.Positions = new SelectList(lstPos, "PositionId", "PositionName", employee.PositionId);
            ViewBag.Departments = new SelectList(lstde, "DepartmentId", "DepartmentName", employee.DepartmentId);
            return View("EmployeeProfile", employeeinfo);  // Trả về view update và truyền đối tượng Employee cho view
        }
        [HttpPost]
        public async Task<ActionResult> Update(Employee employee)
        {
            Debug.WriteLine("Update method called");
            var employeeRepository = HttpContext.ApplicationInstance.Application["EmployeeRepository"] as IEmployeeRepository;
            var response = await employeeRepository.UpdateEmployee(employee);


            return RedirectToAction("Employee");
        }
        public async Task<ActionResult> WorkPress()
        {
            var employeeinfo = new EmployeeInfo();
            var employee = new Employee();
            var employeeRepository = HttpContext.ApplicationInstance.Application["EmployeeRepository"] as IEmployeeRepository;
            var lstemphis = await employeeRepository.GetEmployeeHistories();
            var lstde = await employeeRepository.GetDepartments();
            var lstPos = await employeeRepository.GetPositions();
            var lstJob = await employeeRepository.GetJobtitles();
            ViewBag.Jobtitles = new SelectList(lstJob, "JobtitleId", "JobtitleName");
            ViewBag.Positions = new SelectList(lstPos, "PositionId", "PositionName");
            ViewBag.Departments = new SelectList(lstde, "DepartmentId", "DepartmentName");
            employeeinfo.employeeHistories = lstemphis;
            employeeinfo.employee = employee;
            return View(employeeinfo);
        }
        public async Task<ActionResult> InsertEmployeeHistory(EmployeeHistory employee)
        {
            Debug.WriteLine("Insert Employee");
            var employeeRepository = HttpContext.ApplicationInstance.Application["EmployeeRepository"] as IEmployeeRepository;
            var emp = await employeeRepository.InsertEmployeeHistory(employee);
            Debug.WriteLine("Response Insert " + emp);

            if (emp > 0)
            {
                return Json(new { Success = true, Message = "Thêm employee thành công." });
            }
            else
            {
                if (emp == -10)
                    return Json(new { Success = false, Message = "Dữ liệu cần nhập đầy đủ" });
                else if (emp == -11)
                    return Json(new { Success = false, Message = "Nhân viên không tồn tại" });
                else if (emp == -20)
                    return Json(new { Success = false, Message = "Nhân viên không thuộc phòng này" });
                else if (emp == -21)
                    return Json(new { Success = false, Message = "Nhân viên đang giữ chức vụ khác" });
                else if (emp == -22)
                    return Json(new { Success = false, Message = "Nhân viên đang giữ vị trí khác" });
            }
            throw new Exception("Phương thức Insert sẽ không bao giờ thực thi tới đây.");
        }
        public async Task<ActionResult> ChangeSalary()
        {
            var salaryinfo = new SalaryFixInfo();
            var sal = new SalaryFix();
            var employeeRepository = HttpContext.ApplicationInstance.Application["EmployeeRepository"] as IEmployeeRepository;
            var lstsa = await employeeRepository.GetSalaryFixes();
            salaryinfo.salaryFixes = lstsa;
            salaryinfo.salaryfix = sal;
            return View(salaryinfo);
        }
        public async Task<ActionResult> InsertSalaryFix(SalaryFix employee)
        {
            Debug.WriteLine("Response Insert " + employee.StartDate);

            var employeeRepository = HttpContext.ApplicationInstance.Application["EmployeeRepository"] as IEmployeeRepository;
            var emp = await employeeRepository.InsertSalaryFix(employee);
            Debug.WriteLine("Response Insert " + emp);

            if (emp > 0)
            {
                return Json(new { Success = true, Message = "Thành công" });
            }
            else
            {
                if (emp == -1)
                {
                    return Json(new { Success = false, Message = "Dữ liệu không hợp lệ" });
                }
                else if(emp == -20)
                {
                    return Json(new { Success = false, Message = "Chỉ được phép cập nhật lương khả dụng" });
                }
                return Json(new { Success = false, Message = "Không tồn tại nhân viên" });
            }
            throw new Exception("Lỗi");
        }
        public async Task<ActionResult> DeleteSalaryFix(int SalaryFixId)
        {
            var employeeRepository = HttpContext.ApplicationInstance.Application["EmployeeRepository"] as IEmployeeRepository;
            return RedirectToAction("ChangeSalary");
        }
        public async Task<ActionResult> GetSalaFixById(int STT)
        {
            var salaryinfo = new SalaryFixInfo();
            var employeeRepository = HttpContext.ApplicationInstance.Application["EmployeeRepository"] as IEmployeeRepository;
            var sal =await employeeRepository.GetSalaryFixById(STT);
            var lst = await employeeRepository.GetSalaryFixes();
            salaryinfo.salaryFixes = lst;
            salaryinfo.salaryfix = sal;
            return View("ChangeSalary", salaryinfo);
        }
        public  async Task<ActionResult> UpdateSalaryFix(int SalaryFixId)
        {
            return RedirectToAction("ChangeSalary");
        }
        public async Task<ActionResult> Search(int EmployeeId)
        {
            Debug.WriteLine("Call " + EmployeeId);
            
            var employeeinfo = new EmployeeInfo();
            var employeeRepository = HttpContext.ApplicationInstance.Application["EmployeeRepository"] as IEmployeeRepository;
            var employee = await employeeRepository.GetEmployeeById(EmployeeId);
            var lst = await employeeRepository.GetEmployeeHistories();
            if( employee == null)
            {
                employeeinfo.employee = new Employee();
            }
            else
            {
            employeeinfo.employee = employee;
            }
            employeeinfo.employeeHistories = lst;
            var lstde = await employeeRepository.GetDepartments();
            var lstPos = await employeeRepository.GetPositions();
            var lstJob = await employeeRepository.GetJobtitles();
            ViewBag.Jobtitles = new SelectList(lstJob, "JobtitleId", "JobtitleName");
            ViewBag.Positions = new SelectList(lstPos, "PositionId", "PositionName");
            ViewBag.Departments = new SelectList(lstde, "DepartmentId", "DepartmentName");
            
            return View("WorkPress", employeeinfo);
        }
    }
}