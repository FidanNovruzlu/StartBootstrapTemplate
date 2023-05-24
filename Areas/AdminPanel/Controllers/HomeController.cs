using Microsoft.AspNetCore.Mvc;
using StartBootstrapTemplate.Models;

namespace StartBootstrapTemplate.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            
            return View();
        }
    }
}
