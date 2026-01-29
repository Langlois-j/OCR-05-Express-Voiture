using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OCR_05_Express_Voiture.Data;
using OCR_05_Express_Voiture.Models.Entities;

namespace OCR_05_Express_Voiture.Controllers
{
    public class CarsController(ApplicationDbContext context) : Controller
    {
        private readonly ApplicationDbContext _context = context;

        // GET: Cars
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Car.Include(c => c.Brand).Include(c => c.Model);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Cars/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id is null)
            {
                return NotFound();
            }

            var car = await _context.Car
                .Include(c => c.Brand)
                .Include(c => c.Model)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (car is null)
            {
                return NotFound();
            }

            return View(car);
        }

        // GET: Cars/Create
        public IActionResult Create()
        {
            var car = new Car();
            ViewData["BrandId"] = new SelectList(_context.CarBrand, "Id", "Name");
            ViewData["ModelId"] = new SelectList(_context.CarModel, "Id", "Name");
            return View("Upsert", car);  // ✅ Utilise Upsert.cshtml
        }

        // POST: Cars/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,VinCode,BrandId,ModelId,TrimLevel,ConstructionYear,Mileage,ForSell,Sold,RepairAmount,ImagePath,RepairDescription")] Car car)
        {
            ModelState.Remove("ImagePath");
            ModelState.Remove("Brand");      
            ModelState.Remove("Model");      
            
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                foreach (var error in errors)
                {
                    System.Diagnostics.Debug.WriteLine($"Erreur ModelState: {error.ErrorMessage}");
                }
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(car);
                    await _context.SaveChangesAsync();
                    System.Diagnostics.Debug.WriteLine($"Voiture créée avec succès: {car.VinCode}");
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Erreur lors de la création: {ex.Message}");
                    System.Diagnostics.Debug.WriteLine($"Stack trace: {ex.StackTrace}");
                    ModelState.AddModelError("", $"Erreur: {ex.Message}");
                }
            }

            ViewData["BrandId"] = new SelectList(_context.CarBrand, "Id", "Name", car.BrandId);
            ViewData["ModelId"] = new SelectList(_context.CarModel, "Id", "Name", car.ModelId);
            return View(car);
        }

        // GET: Cars/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = await _context.Car.FindAsync(id);
            if (car == null)
            {
                return NotFound();
            }
            ViewData["BrandId"] = new SelectList(_context.CarBrand, "Id", "Name", car.BrandId);
            ViewData["ModelId"] = new SelectList(_context.CarModel, "Id", "Name", car.ModelId);
            return View("Upsert", car);  // ✅ Utilise Upsert.cshtml
        }

        // POST: Cars/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,VinCode,BrandId,ModelId,TrimLevel,ConstructionYear,Mileage,ForSell,Sold,RepairAmount,ImagePath,RepairDescription")] Car car)
        {
            if (id != car.Id)
            {
                return NotFound();
            }

            ModelState.Remove("ImagePath");
            ModelState.Remove("Brand");      
            ModelState.Remove("Model");

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(car);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarExists(car.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["BrandId"] = new SelectList(_context.CarBrand, "Id", "Name", car.BrandId);
            ViewData["ModelId"] = new SelectList(_context.CarModel, "Id", "Name", car.ModelId);
            return View(car);
        }

        // GET: Cars/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = await _context.Car
                .Include(c => c.Brand)
                .Include(c => c.Model)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }

        // POST: Cars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var car = await _context.Car.FindAsync(id);
            if (car != null)
            {
                _context.Car.Remove(car);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CarExists(int id)
        {
            return _context.Car.Any(e => e.Id == id);
        }

        // Méthode AJAX pour récupérer les modèles selon la marque
        [HttpGet]
        public JsonResult GetModelsByBrand(int brandId)
        {
            var models = _context.CarModel
                .Where(m => m.CarBrandId == brandId)
                .Select(m => new
                {
                    id = m.Id,
                    name = m.Name
                })
                .ToList();

            return Json(models);
        }
    }
}