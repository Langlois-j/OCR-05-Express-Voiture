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

        // GET: Cars/Details/X
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
            return View("Upsert", car);
        }

        // POST: Cars/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,VinCode,BrandId,ModelId,TrimLevel,ConstructionYear,Mileage,ForSell,Sold,RepairAmount,ImagePath,RepairDescription")] Car car, IFormFile? imageFile)
        {
            ModelState.Remove("ImagePath");
            ModelState.Remove("Brand");
            ModelState.Remove("Model");

       
            if (imageFile != null && imageFile.Length > 0)
            {
                try
                {
                    // Créer le dossier s'il n'existe pas
                    var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", "user");
                    if (!Directory.Exists(uploadsFolder))
                    {
                        Directory.CreateDirectory(uploadsFolder);
                    }

                    
                    var timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
                    var extension = Path.GetExtension(imageFile.FileName);
                    var fileName = $"{timestamp}{extension}";
                    var filePath = Path.Combine(uploadsFolder, fileName);

                   
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await imageFile.CopyToAsync(stream);
                    }

                   
                    car.ImagePath = $"/img/user/{fileName}";
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Erreur lors de l'upload de l'image: {ex.Message}");
                    ModelState.AddModelError("ImagePath", "Erreur lors de l'upload de l'image.");
                }
            }

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

                  
                    var createdCar = await _context.Car
                        .Include(c => c.Brand)
                        .Include(c => c.Model)
                        .FirstOrDefaultAsync(c => c.Id == car.Id);

                  
                    ViewBag.Action = "Ajout";
                    ViewBag.Message = "La voiture a été ajoutée avec succès à votre inventaire.";
                    return View("Confirmation", createdCar);
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
            return View("Upsert", car);
        }

        // GET: Cars/Edit/X
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
            return View("Upsert", car);
        }

        // POST: Cars/Edit/X
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,VinCode,BrandId,ModelId,TrimLevel,ConstructionYear,Mileage,ForSell,Sold,RepairAmount,ImagePath,RepairDescription")] Car car, IFormFile? imageFile)
        {
            if (id != car.Id)
            {
                return NotFound();
            }

            ModelState.Remove("ImagePath");
            ModelState.Remove("Brand");
            ModelState.Remove("Model");

            
            var existingCar = await _context.Car.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
            if (existingCar != null && imageFile == null)
            {
                car.ImagePath = existingCar.ImagePath;
            }

           
            if (imageFile != null && imageFile.Length > 0)
            {
                try
                {
                    
                    if (existingCar?.ImagePath != null && !string.IsNullOrEmpty(existingCar.ImagePath))
                    {
                        var oldImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", existingCar.ImagePath.TrimStart('/'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            try
                            {
                                System.IO.File.Delete(oldImagePath);
                                System.Diagnostics.Debug.WriteLine($"Ancienne image supprimée: {oldImagePath}");
                            }
                            catch (Exception ex)
                            {
                                System.Diagnostics.Debug.WriteLine($"Erreur lors de la suppression de l'ancienne image: {ex.Message}");
                            }
                        }
                    }

                  
                    var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", "user");
                    if (!Directory.Exists(uploadsFolder))
                    {
                        Directory.CreateDirectory(uploadsFolder);
                    }

                   
                    var timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
                    var extension = Path.GetExtension(imageFile.FileName);
                    var fileName = $"{timestamp}{extension}";
                    var filePath = Path.Combine(uploadsFolder, fileName);

                    
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await imageFile.CopyToAsync(stream);
                    }

                    
                    car.ImagePath = $"/img/user/{fileName}";
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Erreur lors de l'upload de l'image: {ex.Message}");
                    ModelState.AddModelError("ImagePath", "Erreur lors de l'upload de l'image.");
                }
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(car);
                    await _context.SaveChangesAsync();

                    
                    var updatedCar = await _context.Car
                        .Include(c => c.Brand)
                        .Include(c => c.Model)
                        .FirstOrDefaultAsync(c => c.Id == car.Id);

                    
                    ViewBag.Action = "Modification";
                    ViewBag.Message = "Les informations de la voiture ont été modifiées avec succès.";
                    return View("Confirmation", updatedCar);
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
            }
            ViewData["BrandId"] = new SelectList(_context.CarBrand, "Id", "Name", car.BrandId);
            ViewData["ModelId"] = new SelectList(_context.CarModel, "Id", "Name", car.ModelId);
            return View("Upsert", car);
        }

        // GET: Cars/Delete/X
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

        // POST: Cars/Delete/X
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var car = await _context.Car
                .Include(c => c.Brand)
                .Include(c => c.Model)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (car != null)
            {
                
                if (!string.IsNullOrEmpty(car.ImagePath))
                {
                    var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", car.ImagePath.TrimStart('/'));
                    if (System.IO.File.Exists(imagePath))
                    {
                        try
                        {
                            System.IO.File.Delete(imagePath);
                            System.Diagnostics.Debug.WriteLine($"Image supprimée du serveur: {imagePath}");
                        }
                        catch (Exception ex)
                        {
                            System.Diagnostics.Debug.WriteLine($"Erreur lors de la suppression de l'image: {ex.Message}");
                           
                        }
                    }
                }

                _context.Car.Remove(car);
                await _context.SaveChangesAsync();

                
                ViewBag.Action = "Suppression";
                ViewBag.Message = "La voiture a été supprimée avec succès de votre inventaire.";
                return View("Confirmation", car);
            }

            return RedirectToAction(nameof(Index));
        }

        private bool CarExists(int id)
        {
            return _context.Car.Any(e => e.Id == id);
        }

        
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
