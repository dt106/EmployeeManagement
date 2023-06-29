using Dapper;
using HRM.DTO;
using HRM.Repositories;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace HRM
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        protected void Application_Start()
        {
           
         
            var employeeRepository = new EmployeeRepository();
            HttpContext.Current.ApplicationInstance.Application["EmployeeRepository"] = employeeRepository;
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
           
        }
        //public async Task<List<Employee>> GetAllEmployee()
        //{
        //    var lst = new List<Employee>();
        //    try
        //    {
        //        using (var conn = new SqlConnection(_connectionString))
        //        {
        //            await conn.OpenAsync();
        //            using (var command = new SqlCommand("Select top(1) * from Employee", conn))
        //            {
        //                using (var reader = await command.ExecuteReaderAsync())
        //                {
        //                    while (await reader.ReadAsync())
        //                    {
        //                        var employee = new Employee
        //                        {
        //                            EmployeeId = reader.GetInt32(reader.GetOrdinal("EmployeeId")),
        //                            EmployeeName = reader.GetString(reader.GetOrdinal("EmployeeName")),
        //                            Gender  = reader.GetByte(reader.GetOrdinal("Gender")),
        //                            Email = reader.GetString(reader.GetOrdinal("Email")),
        //                            PhoneNumber = reader.GetString(reader.GetOrdinal("PhoneNumber")),
        //                            Address = reader.GetString(reader.GetOrdinal("Address")),
        //                            CCCD = reader.GetString(reader.GetOrdinal("CCCD")),
        //                            Birthday = reader.GetDateTime(reader.GetOrdinal("Birthday")),
        //                            JobtitleId = reader.GetByte(reader.GetOrdinal("JobtitleId")),
        //                            PositionId = reader.GetByte(reader.GetOrdinal("PositionId")),
        //                            TimeIn = reader.GetDateTime(reader.GetOrdinal("TimeIn")),
        //                            TimeOut = reader.GetDateTime(reader.GetOrdinal("TimeOut")),
        //                            Salary = reader.GetDecimal(reader.GetOrdinal("Salary")),
        //                            Status = reader.GetByte(reader.GetOrdinal("Status")),
        //                            CreateTime = reader.GetDateTime(reader.GetOrdinal("CreateTime")),
        //                            CreateBy = reader.GetString(reader.GetOrdinal("CreateBy")),
        //                            ModifiedTime = reader.GetDateTime(reader.GetOrdinal("ModifiedTime")),
        //                            ModifiedBy = reader.GetString(reader.GetOrdinal("ModifiedBy")),
        //                        };
        //                        lst.Add(employee);
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    catch (SqlException ex)
        //    {
        //        Console.WriteLine("Error getting employees: " + ex.Message);
        //    }
        //    return lst;
        //}
        //public async Task<List<TimekeepingEmp>> GetAllLog()
        //{
        //    var lst = new List<TimekeepingEmp>();
        //    try
        //    {
        //        using (var conn = new SqlConnection(_connectionString))
        //        {
        //            await conn.OpenAsync();
        //            var results = await conn.QueryAsync<TimekeepingEmp>("SP_Timekeeping_GetAllLog", commandTimeout: 5, commandType: CommandType.StoredProcedure);
        //            lst = results.ToList();
        //        }

        //    }
        //    catch (SqlException ex)
        //    {
        //        Console.WriteLine("Error getting employees: " + ex.Message);
        //    }
        //    return lst;
        //}
    }
}
