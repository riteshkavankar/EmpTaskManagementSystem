using Microsoft.AspNetCore.Mvc;

namespace EmployeeTaskManagementSystem.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
