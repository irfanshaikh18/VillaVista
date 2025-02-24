using Microsoft.AspNetCore.Mvc;
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
    }
}
