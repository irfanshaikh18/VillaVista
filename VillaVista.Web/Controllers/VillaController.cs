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
            List<Villa> villas = _dbContext.Villas.ToList();
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
                ModelState.AddModelError("Name", "The description cannot exactly match the name");
            }

            if (ModelState.IsValid)
            {
                _dbContext.Villas.Add(obj);
                _dbContext.SaveChanges();
                TempData["success"] = "The villa has been created successfully";
                return RedirectToAction("Index");
            }

            TempData["error"] = "The villa could not be created";
            return View();
        }

        public IActionResult Update(int villaId)
        {
            Villa? obj = _dbContext.Villas.FirstOrDefault(u => u.Id == villaId);
            // Villa? obj = _dbContext.Villas.Find(villaId); // If you are working on primary key like villaId,
            // Villa? obj = _dbContext.Villas.Where(u => u.Price > 50 && u.Occupancy > 0);
            // Above couple examples shows the multiple ways to retrieve data using ef core

            if (obj is null)
            {
                return RedirectToAction("Error", "Home");
            }

            return View(obj);
        }

        [HttpPost]
        public IActionResult Update(Villa obj)
        {
            if (ModelState.IsValid && obj.Id > 0)
            {
                _dbContext.Villas.Update(obj);
                _dbContext.SaveChanges();
                TempData["success"] = "The villa has been updated successfully";
                return RedirectToAction("Index");
            }

            TempData["error"] = "The villa could not be updated";
            return View();
        }

        public IActionResult Delete(int villaId)
        {
            Villa? obj = _dbContext.Villas.FirstOrDefault(u => u.Id == villaId);

            if (obj is null)
            {
                return RedirectToAction("Error", "Home");
            }

            return View(obj);
        }

        [HttpPost]
        public IActionResult Delete(Villa obj)
        {
            Villa? objFromDb = _dbContext.Villas.FirstOrDefault(u => u.Id == obj.Id);

            if (objFromDb is not null)
            {
                _dbContext.Villas.Remove(objFromDb);
                _dbContext.SaveChanges();
                TempData["success"] = "The villa has been deleted successfully";
                return RedirectToAction("Index");
            }

            TempData["error"] = "The villa could not be deleted";
            return View();
        }
    }
}
