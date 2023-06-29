
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using HRM.IRepositories;
using HRM.DTO;
using System.Diagnostics;

namespace HRM.Controllers
{
    public class LoginController : Controller
    {
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        // GET: Login
        [HttpPost]
        public async Task<ActionResult> Login(User user)
        {
            Debug.WriteLine("Login called");
            var employeeRepository = HttpContext.ApplicationInstance.Application["EmployeeRepository"] as IEmployeeRepository;
            var response = await employeeRepository.Login(user);
            Debug.WriteLine("Login info" + response);

            if (response > 0 )
            {
                //Session["EmployeeId"] = user.UserName;
                Session["EmployeeId"] = response;
                //Debug.WriteLine("Login info" + Session["UserName"].ToString());

                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }
        }
        public ActionResult LogOut()
        {
            Session.Remove("EmployeeId");
            return View("Login");
        }
    }
}