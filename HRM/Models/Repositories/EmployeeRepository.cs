using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRM.DTO;
using HRM.IRepositories;
using System.Data.SqlClient;
using System.Data;
using HRM;
using System.Configuration;
using Dapper;
using Newtonsoft.Json;
using Glimpse.Core.Extensibility;
using System.Diagnostics;

namespace HRM.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;


        //public EmployeeRepository()
        //{
        //    _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        //}

        public async Task<List<Employee>> GetAllEmployee()
        {
            var lst = new List<Employee>();
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    await conn.OpenAsync();
                    using (var command = new SqlCommand("Select * from Employee order by employeeid desc", conn))
                    {
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                var employee = new Employee
                                {
                                    EmployeeId = reader.GetInt32(reader.GetOrdinal("EmployeeId")),
                                    EmployeeName = reader.GetString(reader.GetOrdinal("EmployeeName")),
                                    Gender = reader.GetByte(reader.GetOrdinal("Gender")),
                                    Email = reader.GetString(reader.GetOrdinal("Email")),
                                    PhoneNumber = reader.GetString(reader.GetOrdinal("PhoneNumber")),
                                    Address = reader.GetString(reader.GetOrdinal("Address")),
                                    CCCD = reader.GetString(reader.GetOrdinal("CCCD")),
                                    Birthday = reader.GetDateTime(reader.GetOrdinal("Birthday")),
                                    JobtitleId = reader.GetByte(reader.GetOrdinal("JobtitleId")),
                                    PositionId = reader.GetByte(reader.GetOrdinal("PositionId")),
                                    DepartmentId = reader.GetByte(reader.GetOrdinal("DepartmentId")),
                                    TimeIn = reader.GetDateTime(reader.GetOrdinal("TimeIn")),
                                    TimeOut = reader.GetDateTime(reader.GetOrdinal("TimeOut")),
                                    Status = reader.GetByte(reader.GetOrdinal("Status")),
                                    CreateTime = reader.GetDateTime(reader.GetOrdinal("CreateTime")),
                                    CreateBy = reader.GetString(reader.GetOrdinal("CreateBy")),
                                    ModifiedTime = reader.GetDateTime(reader.GetOrdinal("ModifiedTime")),
                                    ModifiedBy = reader.GetString(reader.GetOrdinal("ModifiedBy"))
                                };
                                lst.Add(employee);
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Error getting employees: " + ex.Message);
            }
            return lst;
        }
        public async Task<int> DeleteEmployee(Employee param)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                var sqlParams = new DynamicParameters();
                sqlParams.Add(name: "EmployeeId", dbType: DbType.Int32, direction: ParameterDirection.Input, value: param.EmployeeId);
                sqlParams.Add(name: "Response", dbType: DbType.Int32, direction: ParameterDirection.Output);
                await conn.ExecuteAsync("SP_Employee_Delete", param: sqlParams, commandTimeout: 5, commandType: CommandType.StoredProcedure);
                int responseStatus = sqlParams.Get<int>("Response");
                return responseStatus;
            }
        }

        public async Task<int> UpdateEmployee(Employee param)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                var sqlParams = new DynamicParameters();
                sqlParams.Add(name: "EmployeeId", dbType: DbType.Int32, direction: ParameterDirection.Input, value: param.EmployeeId);
                sqlParams.Add(name: "EmployeeName", dbType: DbType.String, direction: ParameterDirection.Input, value: param.EmployeeName);
                sqlParams.Add(name: "Gender", dbType: DbType.Byte, direction: ParameterDirection.Input, value: param.Gender);
                sqlParams.Add(name: "Email", dbType: DbType.String, direction: ParameterDirection.Input, value: param.Email);
                sqlParams.Add(name: "PhoneNumber", dbType: DbType.String, direction: ParameterDirection.Input, value: param.PhoneNumber);
                sqlParams.Add(name: "Address", dbType: DbType.String, direction: ParameterDirection.Input, value: param.Address);
                sqlParams.Add(name: "CCCD", dbType: DbType.String, direction: ParameterDirection.Input, value: param.CCCD);
                sqlParams.Add(name: "Birthday", dbType: DbType.Date, direction: ParameterDirection.Input, value: param.Birthday);
                sqlParams.Add(name: "JobtitleId", dbType: DbType.Byte, direction: ParameterDirection.Input, value: param.JobtitleId);
                sqlParams.Add(name: "PositionId", dbType: DbType.Byte, direction: ParameterDirection.Input, value: param.PositionId);
                sqlParams.Add(name: "DepartmentId", dbType: DbType.Byte, direction: ParameterDirection.Input, value: param.DepartmentId);
                //sqlParams.Add(name: "TimeOut", dbType: DbType.Date, direction: ParameterDirection.Input, value: param.TimeOut);
                sqlParams.Add(name: "ModifiedBy", dbType: DbType.String, direction: ParameterDirection.Input, value: param.ModifiedBy);
                sqlParams.Add(name: "Response", dbType: DbType.Int32, direction: ParameterDirection.Output);
                await conn.ExecuteAsync("SP_Employee_UpdateEmployee", param: sqlParams, commandTimeout: 20, commandType: CommandType.StoredProcedure);
                int responseStatus = sqlParams.Get<int>("Response");
                return responseStatus;
            }
        }


        public async Task<Employee> GetEmployeeById(int employeeId)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                var sqlParams = new DynamicParameters();
                sqlParams.Add(name: "EmployeeId", dbType: DbType.Int32, direction: ParameterDirection.Input, value: employeeId);
                var results = await conn.QueryAsync<Employee>("SP_Employee_GetEmployeeById", param: sqlParams, commandTimeout: 5, commandType: CommandType.StoredProcedure);
                return results.FirstOrDefault();
            }
        }

        public async Task<List<TimekeepingEmp>> GetAllLog()
        {
            var lst = new List<TimekeepingEmp>();
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    await conn.OpenAsync();
                    var results = await conn.QueryAsync<TimekeepingEmp>("SP_Timekeeping_GetAllLog", commandTimeout: 5, commandType: CommandType.StoredProcedure);
                    lst = results.ToList();
                }

            }
            catch (SqlException ex)
            {
                Console.WriteLine("Error getting employees: " + ex.Message);
            }
            return lst;
        }

        public async Task<List<EmployeeHistory>> GetEmployeeHistories()
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                {
                    var results = await conn.QueryAsync<EmployeeHistory>("SP_EmploymentHistory_GetAll", commandTimeout: 5, commandType: CommandType.StoredProcedure);
                    return results.ToList();
                }
            }
        }
        public async Task<int> InsertEmployeeHistory(EmployeeHistory param)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var sqlParams = new DynamicParameters();
                sqlParams.Add(name: "EmployeeId", dbType: DbType.Int32, direction: ParameterDirection.Input, value: param.EmployeeId);
                sqlParams.Add(name: "DepartmentNew", dbType: DbType.Byte, direction: ParameterDirection.Input, value: param.DepartmentNew);
                sqlParams.Add(name: "PositionNew", dbType: DbType.Byte, direction: ParameterDirection.Input, value: param.PositionNew);
                sqlParams.Add(name: "JobtitleNew", dbType: DbType.Byte, direction: ParameterDirection.Input, value: param.JobtitleNew);
                sqlParams.Add(name: "StartDate", dbType: DbType.Date, direction: ParameterDirection.Input, value: param.StartDate);
                sqlParams.Add(name: "Decription", dbType: DbType.String, direction: ParameterDirection.Input, value: param.Decription);
                sqlParams.Add(name: "Response", dbType: DbType.Int32, direction: ParameterDirection.Output);
                await conn.ExecuteAsync("SP_EmployeeHistory_Change", param: sqlParams, commandTimeout: 10, commandType: CommandType.StoredProcedure);
                int response = sqlParams.Get<int>("Response");
                return response;
            }
        }

        public async Task<List<Department>> GetDepartments()
        {
            var lst = new List<EmployeeDTO>();
            using (var conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                {
                    var results = await conn.QueryAsync<Department>("SP_Department_GetDepartments", commandTimeout: 5, commandType: CommandType.StoredProcedure);
                    return results.ToList();
                }
            }

        }

        public async Task<int> Insert(Employee param)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                var sqlParams = new DynamicParameters();
                sqlParams.Add(name: "EmployeeName", dbType: DbType.String, direction: ParameterDirection.Input, value: param.EmployeeName);
                sqlParams.Add(name: "Gender", dbType: DbType.Byte, direction: ParameterDirection.Input, value: param.Gender);
                sqlParams.Add(name: "Email", dbType: DbType.String, direction: ParameterDirection.Input, value: param.Email);
                sqlParams.Add(name: "CCCD", dbType: DbType.String, direction: ParameterDirection.Input, value: param.CCCD);
                sqlParams.Add(name: "Birthday", dbType: DbType.Date, direction: ParameterDirection.Input, value: param.Birthday);
                sqlParams.Add(name: "TimeIn", dbType: DbType.Date, direction: ParameterDirection.Input, value: param.TimeIn);
                sqlParams.Add(name: "DepartmentId", dbType: DbType.Byte, direction: ParameterDirection.Input, value: param.DepartmentId);
                sqlParams.Add(name: "PositionId", dbType: DbType.Byte, direction: ParameterDirection.Input, value: param.PositionId);
                sqlParams.Add(name: "JobtitleId", dbType: DbType.Byte, direction: ParameterDirection.Input, value: param.JobtitleId);
                sqlParams.Add(name: "Response", dbType: DbType.Int32, direction: ParameterDirection.Output);
                await conn.ExecuteAsync("SP_Employee_Insert", param: sqlParams, commandTimeout: 10, commandType: CommandType.StoredProcedure);
                int response = sqlParams.Get<int>("Response");
                return response;
            }
        }

        // cho Login
        public async Task<int> Login(User param)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var sqlParams = new DynamicParameters();
                sqlParams.Add(name: "UserName", dbType: DbType.String, direction: ParameterDirection.Input, value: param.UserName);
                sqlParams.Add(name: "PassWord", dbType: DbType.String, direction: ParameterDirection.Input, value: param.PassWord);
                sqlParams.Add(name: "Response", dbType: DbType.Int32, direction: ParameterDirection.Output);
                await conn.ExecuteAsync("SP_User_Login", param: sqlParams, commandTimeout: 10, commandType: CommandType.StoredProcedure);
                int response = sqlParams.Get<int>("Response");
                return response;
            }

        }
        // ket thuc Login

        // cho profile
        public async Task<Employee> GetEmployeeByName(string UserName)
        {
            Employee emp = new Employee();
            using (var conn = new SqlConnection(_connectionString))
            {
                var sqlParams = new DynamicParameters();
                sqlParams.Add(name: "UserName", dbType: DbType.String, direction: ParameterDirection.Input, value: UserName);
                var results = await conn.QueryAsync<Employee>("SP_User_GetEmployeeByName", param: sqlParams, commandTimeout: 5, commandType: CommandType.StoredProcedure);
                return results.FirstOrDefault();
            }
            // kết thúc profile
        }

        // cho dashboard
        public async Task<int> InsertLog(int param)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var sqlParams = new DynamicParameters();
                sqlParams.Add(name: "EmployeeId", dbType: DbType.String, direction: ParameterDirection.Input, value: param);
                sqlParams.Add(name: "Response", dbType: DbType.Int32, direction: ParameterDirection.Output);
                await conn.ExecuteAsync("SP_TimKeeping_Insert", param: sqlParams, commandTimeout: 5, commandType: CommandType.StoredProcedure);
                int response = sqlParams.Get<int>("Response");
                return response;
            }
        }

        // kết thúc dashboard

        //cho Timekeeping
        public async Task<LogMonth> GetById(int EmployeeId)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var sqlParams = new DynamicParameters();
                sqlParams.Add(name: "EmployeeId", dbType: DbType.Int32, direction: ParameterDirection.Input, value: EmployeeId);
                var result =  await conn.QueryAsync<LogMonth>("SP_LogMonth_GetById", param: sqlParams, commandTimeout: 5, commandType: CommandType.StoredProcedure);
                return result.FirstOrDefault();
            }
        }
        public async Task<List<LogMonth>> GetAllLogMonth()
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var sqlParams = new DynamicParameters();
                var result = await conn.QueryAsync<LogMonth>("SP_LogMonth_GetAllLog", commandTimeout: 5, commandType: CommandType.StoredProcedure);
                return result.ToList();
            }
        }

        public async Task<List<DailyLog>> GetAllDailyLog()
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var sqlParams = new DynamicParameters();
                var result = await conn.QueryAsync<DailyLog>("SP_DailyLog_GetAll", commandTimeout: 5, commandType: CommandType.StoredProcedure);
                return result.ToList();
            }
        }
        public async Task<int> TimekeepingCompensatory(Timekeeping param)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var sqlParams = new DynamicParameters();
                sqlParams.Add(name: "EmployeeId", dbType: DbType.Int32, direction: ParameterDirection.Input, value: param.EmployeeID);
                sqlParams.Add(name: "LogDate", dbType: DbType.Date, direction: ParameterDirection.Input, value: param.LogDate);
                sqlParams.Add(name: "LogTime", dbType: DbType.Time, direction: ParameterDirection.Input, value: param.LogTime);
                sqlParams.Add(name: "Response", dbType: DbType.Int32, direction: ParameterDirection.Output);
                await conn.ExecuteAsync("SP_Timekeeping_Compensatory", param: sqlParams, commandTimeout: 5, commandType: CommandType.StoredProcedure);
                int response = sqlParams.Get<int>("Response");
                return response;
            }
        }
        public async Task<int> TimeWorking(DateTime DayLog)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var sqlParams = new DynamicParameters();
                sqlParams.Add(name: "BeginDate", dbType: DbType.Date, direction: ParameterDirection.Input, value: DayLog);
                sqlParams.Add(name: "EndDate", dbType: DbType.Date, direction: ParameterDirection.Input, value: DayLog);
                sqlParams.Add(name: "Response", dbType: DbType.Int32, direction: ParameterDirection.Output);
                await conn.ExecuteAsync("SP_Timekeeping_WorkingTimeXY", param: sqlParams, commandTimeout: 5, commandType: CommandType.StoredProcedure);
                int response = sqlParams.Get<int>("Response");
                return response;
            }
        }
        public async Task<int> UpdateLogMonth(int Year, int Month, int Day)
        {

            using (var conn = new SqlConnection(_connectionString))
            {
                var sqlParams = new DynamicParameters();
                sqlParams.Add(name: "Year", dbType: DbType.Int32, direction: ParameterDirection.Input, value: Year);
                sqlParams.Add(name: "Month", dbType: DbType.Int32, direction: ParameterDirection.Input, value: Month);
                sqlParams.Add(name: "Day", dbType: DbType.Int32, direction: ParameterDirection.Input, value: Day);
                sqlParams.Add(name: "Response", dbType: DbType.Int32, direction: ParameterDirection.Output);
                await conn.ExecuteAsync("SP_LogMonth_UpdateDaily", param: sqlParams, commandTimeout: 5, commandType: CommandType.StoredProcedure);
                int response = sqlParams.Get<int>("Response");
                return response;

            }
        }
        public async Task<int> LogMonthInsert(LogMonth param)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var sqlParams = new DynamicParameters();
                sqlParams.Add(name: "Year", dbType: DbType.Int32, direction: ParameterDirection.Input, value: param.Year);
                sqlParams.Add(name: "Month", dbType: DbType.Int32, direction: ParameterDirection.Input, value: param.Month);
                sqlParams.Add(name: "Response", dbType: DbType.Int32, direction: ParameterDirection.Output);
                await conn.ExecuteAsync("SP_LogMonth_Insert", param: sqlParams, commandTimeout: 5, commandType: CommandType.StoredProcedure);
                int response = sqlParams.Get<int>("Response");
                return response;

            }
        }

        //ket thuc Timekeeping

        //cho Chức vụ
        public async Task<List<Position>> GetPositions()
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var result = await conn.QueryAsync<Position>("SP_Position_GetAll", commandTimeout: 5, commandType: CommandType.StoredProcedure);
                return result.ToList();
            }
        }
        //kết thúc chức vụ
        //cho vị trí làm việc
        public async Task<List<Jobtitle>> GetJobtitles()
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var result = await conn.QueryAsync<Jobtitle>("SP_Jobtitle_GetAll", commandTimeout: 5, commandType: CommandType.StoredProcedure);
                return result.ToList();
            }
        }
        // cho Lương
        public async Task<List<SalaryFix>> GetSalaryFixes()
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var result = await conn.QueryAsync<SalaryFix>("SP_SalaryFix_GetAll", commandTimeout: 5, commandType: CommandType.StoredProcedure);
                return result.ToList();
            }
        }
        public async Task<int> InsertSalaryFix(SalaryFix param)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                var sqlParams = new DynamicParameters();
                sqlParams.Add(name: "STT", dbType: DbType.String, direction: ParameterDirection.Input, value: param.STT);
                sqlParams.Add(name: "EmployeeId", dbType: DbType.String, direction: ParameterDirection.Input, value: param.EmployeeId);
                sqlParams.Add(name: "Money", dbType: DbType.Decimal, direction: ParameterDirection.Input, value: param.Money);
                sqlParams.Add(name: "Bonus", dbType: DbType.Decimal, direction: ParameterDirection.Input, value: param.Bonus);
                sqlParams.Add(name: "StartDate", dbType: DbType.Date, direction: ParameterDirection.Input, value: param.StartDate);
                sqlParams.Add(name: "Decription", dbType: DbType.String, direction: ParameterDirection.Input, value: param.Decription);
                sqlParams.Add(name: "Response", dbType: DbType.Int32, direction: ParameterDirection.Output);
                await conn.ExecuteAsync("SP_SalaryFix_Change", param: sqlParams, commandTimeout: 10, commandType: CommandType.StoredProcedure);
                int response = sqlParams.Get<int>("Response");
                return response;
            }
        }
        public async Task<List<Salary>> GetSalaries()
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var result = await conn.QueryAsync<Salary>("SP_Salary_GetAll", commandTimeout: 5, commandType: CommandType.StoredProcedure);
                return result.ToList();
            }
        }

        public async Task<List<EmployeeHistory>> GetHistoriesById(int param)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                var sqlParams = new DynamicParameters();
                sqlParams.Add(name: "EmployeeId", dbType: DbType.Int32, direction: ParameterDirection.Input, value: param);
                //sqlParams.Add(name: "Response", dbType: DbType.Int32, direction: ParameterDirection.Output);
                var result = await conn.QueryAsync<EmployeeHistory>("SP_EmployeeHistory_GetHistoryById", param: sqlParams, commandTimeout: 5, commandType: CommandType.StoredProcedure);
               
                //lstData.Histories = result ==null? new List<EmployeeHistory>() : result.ToList();
                //lstData.Response = sqlParams.Get<int>("Response");

                return result.ToList();
            }
        }
        
        public async Task<List<SalaryFix>> GetSalariesByEmployeeId(int param)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                var sqlParams = new DynamicParameters();
                sqlParams.Add(name: "EmployeeId",dbType: DbType.Int32, direction: ParameterDirection.Input, value: param);
                var result = await conn.QueryAsync<SalaryFix>("SP_SalaryFix_GetSalaryById",param: sqlParams, commandTimeout: 5, commandType: CommandType.StoredProcedure);
                return result.ToList();
            }
        }
        public async Task<SalaryFix> GetSalaryFixById(int param)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                var sqlParams = new DynamicParameters();
                sqlParams.Add(name: "STT", dbType: DbType.Int32, direction: ParameterDirection.Input, value: param);
                var result = await conn.QueryAsync<SalaryFix>("SP_SalaryFix_GetSalaryFixById", param: sqlParams, commandTimeout: 5, commandType: CommandType.StoredProcedure);
                return result.FirstOrDefault();
            }

        }

        // phòng ban
        public async Task<int> DepartmentInsert(Department param)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var sqlParams = new DynamicParameters();
                sqlParams.Add(name: "DepartmentName", dbType: DbType.Int32, direction: ParameterDirection.Input, value: param.DepartmentName);
                sqlParams.Add(name: "DateOfEstablishment", dbType: DbType.Int32, direction: ParameterDirection.Input, value: param.DateOfEstablishment);
                sqlParams.Add(name: "Response", dbType: DbType.Int32, direction: ParameterDirection.Output);
                await conn.ExecuteAsync("SP_Department_Insert", param: sqlParams, commandTimeout: 5, commandType: CommandType.StoredProcedure);
                int response = sqlParams.Get<int>("Response");
                return response;

            }
        }

    }
}
