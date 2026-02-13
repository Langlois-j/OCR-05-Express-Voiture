using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OCR_05_Express_Voiture.Data;
using OCR_05_Express_Voiture.Models.Entities;

namespace OCR_05_Express_Voiture.Controllers
{
    [Authorize(Roles = "Admin")]
    public class SettingsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SettingsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Settings
        public async Task<IActionResult> Index()
        {
            var brands = await _context.CarBrand.OrderBy(b => b.Name).ToListAsync();
            var models = await _context.CarModel.Include(m => m.CarBrand).OrderBy(m => m.Name).ToListAsync();

            ViewBag.Brands = brands;
            ViewBag.Models = models;

            return View();
        }

        // POST: Settings/AddBrand
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddBrand(string brandName)
        {
            if (!string.IsNullOrWhiteSpace(brandName))
            {
                var brand = new CarBrand { Name = brandName.Trim() };
                _context.Add(brand);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        // POST: Settings/DeleteBrand
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteBrand(int id)
        {
            var brand = await _context.CarBrand.FindAsync(id);
            if (brand != null)
            {
                // Vérifier s'il y a des modèles associés
                var hasModels = await _context.CarModel.AnyAsync(m => m.CarBrandId == id);
                if (hasModels)
                {
                    TempData["Error"] = "Impossible de supprimer cette marque car elle contient des modèles.";
                }
                else
                {
                    _context.CarBrand.Remove(brand);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "Marque supprimée avec succès.";
                }
            }
            return RedirectToAction(nameof(Index));
        }

        // POST: Settings/AddModel
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddModel(string modelName, int brandId)
        {
            if (!string.IsNullOrWhiteSpace(modelName) && brandId > 0)
            {
                var model = new CarModel
                {
                    Name = modelName.Trim(),
                    CarBrandId = brandId
                };
                _context.Add(model);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        // POST: Settings/DeleteModel
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteModel(int id)
        {
            var model = await _context.CarModel.FindAsync(id);
            if (model != null)
            {
                // Vérifier s'il y a des voitures avec ce modèle
                var hasCars = await _context.Car.AnyAsync(c => c.ModelId == id);
                if (hasCars)
                {
                    TempData["Error"] = "Impossible de supprimer ce modèle car il est utilisé par des voitures.";
                }
                else
                {
                    _context.CarModel.Remove(model);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "Modèle supprimé avec succès.";
                }
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
