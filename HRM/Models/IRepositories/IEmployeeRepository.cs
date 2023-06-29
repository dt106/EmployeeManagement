 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using HRM.DTO;
using HRM.Repositories;

namespace HRM.IRepositories
{
    public interface IEmployeeRepository
    {
        Task<List<Employee>> GetAllEmployee();
        Task<int> DeleteEmployee(Employee param);
        Task<int> UpdateEmployee(Employee param);
        Task<Employee> GetEmployeeById(int employeeId);
        Task<List<TimekeepingEmp>> GetAllLog();
        Task<int> Insert(Employee param);
        Task<List<Department>> GetDepartments();
        Task<int> Login(User param);
        Task<Employee> GetEmployeeByName(string UserName);
        Task<int> InsertLog(int param);
        Task<List<LogMonth>> GetAllLogMonth();
        Task<List<DailyLog>> GetAllDailyLog();
        Task<int> UpdateLogMonth(int Year, int Month, int Day);
        Task<List<EmployeeHistory>> GetEmployeeHistories();
        Task<int> InsertEmployeeHistory(EmployeeHistory param);
        Task<List<SalaryFix>> GetSalaryFixes();
        Task<int> InsertSalaryFix(SalaryFix param);
        Task<List<Salary>> GetSalaries();
        // Position
        Task<List<Position>> GetPositions();
        //Jobtitle
        Task<List<Jobtitle>> GetJobtitles();
        //Quá trình công tác và quá trình lương
        Task<List<EmployeeHistory>> GetHistoriesById(int param);
        Task<List<SalaryFix>> GetSalariesByEmployeeId(int param);
        Task<SalaryFix> GetSalaryFixById(int param);
        //Timekeeping
        Task<LogMonth> GetById(int param);
        Task<int> LogMonthInsert(LogMonth param);
        Task<int> TimekeepingCompensatory(Timekeeping param);
        Task<int> TimeWorking(DateTime DayLog);
        //Department
        Task<int> DepartmentInsert(Department param);
    }
}