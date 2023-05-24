using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StartBootstrapTemplate.DAL;
using StartBootstrapTemplate.Models;

namespace StartBootstrapTemplate.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
       

        public HomeController(AppDbContext context)
        {
            _context = context;
           
        }
        public IActionResult Index()
        {
            List<Product> products = _context.Products.Include(p=>p.Category).ToList();
            return View(products);
        }
    }
}
