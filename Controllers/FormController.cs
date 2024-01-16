// Controllers/HomeController.cs
using Microsoft.AspNetCore.Mvc;

namespace MyDotNetServer.Controllers
{
    public class FormController : Controller
    {
        public IActionResult Index()
        {
            return Content("Hello, ASP.NET Core Server!");
        }
    }
}
