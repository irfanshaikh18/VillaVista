using Microsoft.AspNetCore.Mvc;
using VillaVista.Domain.Entities;
using VillaVista.Infrastructure.Data;

namespace VillaVista.Web.Controllers
{
    public class VillaController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public VillaController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            List<Domain.Entities.Villa> villas = _dbContext.Villas.ToList();
            return View(villas);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Villa obj)
        {
            if (obj.Name == obj.Description)
            {
                ModelState.AddModelError("", "The description cannot exactly match the name.");
            }
            
            if (ModelState.IsValid)
            {
                _dbContext.Villas.Add(obj);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }

            return View();
        }
    }
}
