using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using COMPUTINGNEA.Models;
using System.Web;

namespace COMPUTINGNEA.Controllers
{
    
    public class HomeController : Controller

    {
        User user = new User();
        Investment investment = new Investment();

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Registration()
        {
            return View();
        }

        public IActionResult Home()
        { 
            return View();
        }

        public IActionResult AddInvestment()
        {
            return View();
        }

        public IActionResult ViewInvestments()
        {
            return View();
        }

        public IActionResult Account()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        // Getting user input from forms and allowing them to register onto the database
        [HttpPost]
        public IActionResult GetUserDetails()
        {
            // Retrieve user input from forms and assign to variables

            user.FirstName = HttpContext.Request.Form["FirstName"];
            user.LastName = HttpContext.Request.Form["LastName"];
            user.Email = HttpContext.Request.Form["Email"];
            user.Username = HttpContext.Request.Form["Username"];
            investment.Username = HttpContext.Request.Form["Username"];
            user.Userpassword = HttpContext.Request.Form["Userpassword"];
            string confirmationPassword = HttpContext.Request.Form["Confirmation"];

            // check if password entered matches confirmed password entered
            if (user.Userpassword != confirmationPassword)
            {
                return View("ConfirmationError");
            }

            int result1 = user.CheckEmailExists();
            int result2 = user.CheckUsernameExists();
            if (result1 > 0 || result2 > 0)
            {
                return View("RegistrationError");
            }
            // attemps to save user details into database
            try
            {
                int result = user.SaveDetails();
                return View("Home");
            }
            catch
            {
                return View("Index");
            }
            
        }

        [HttpPost]
        public IActionResult Login()
        {
            user.Username = HttpContext.Request.Form["Username"];
            user.Userpassword = HttpContext.Request.Form["Userpassword"];

            try
            {
                int result2 = user.GetPassword();
                
                if (result2 == 1)
                {
                  //  FormsAuthentication.SerAuthCookie(user.Username, false);
                    return View("Home");
                }
                else if (result2 <= 0 )
                {
                    return View("LoginError");
                }
                else
                {
                    return View("IncorrectPasswordError");
                }
            }
            catch
            {
                return View("LoginError");
            }      
        }

        [HttpPost]
        public IActionResult GetInvestmentDetails()
        {

            investment.InvestmentName = HttpContext.Request.Form["InvestmentName"];
            investment.Industry = HttpContext.Request.Form["Industry"];
            investment.AmountInvested = HttpContext.Request.Form["AmountInvested"];
            investment.Revenue = HttpContext.Request.Form["Revenue"];
            investment.Profit = HttpContext.Request.Form["Profit"];
            investment.Username = user.Username;
            
                int result = investment.SaveDetails();
                return View("Home");
            
        }

        [HttpPost]
        public IActionResult Search()
        {


            string SearchInput = HttpContext.Request.Form["SearchInput"];


            string[] fakenames = (investment.SearchName(SearchInput)).ToArray();
            if (fakenames.Contains(SearchInput))
            {
                return View("ViewInvestments");
            }
            else
            {
                return View("Home");
            }
            // perform a sort
            //   investment.MainMerge(fakenames, 0, 0 ,0);
            //   investment.SortMerge(fakenames, 0, 0);
            /*  bool found = investment.binarySearch(fakenames, SearchInput);

              if (found)
              {
                  return View("Search");
              }
              else
              {
                  return View("ViewInvestments");
              }
          } */
        }

        public IActionResult Logout()
        {
            return View("Home");
        }

        public string OpenModelPopup()
        {
            //can send some data also.  
            return "<h1>This is Modal Popup Window</h1>";
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
