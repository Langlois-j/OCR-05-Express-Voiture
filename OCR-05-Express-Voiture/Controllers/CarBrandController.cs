using Microsoft.AspNetCore.Mvc;
using OCR_05_Express_Voiture.Models.Entities;
using OCR_05_Express_Voiture.Models.Repositories;
using System.Threading.Tasks;

namespace OCR_05_Express_Voiture.Controllers
{

    public class CarBrandController : Controller
    {
        private readonly ICarBrandRepository _repository;

        public CarBrandController(ICarBrandRepository repository)
        {
            _repository = repository;
        }

        // GET: CarBrand
        // Liste de toutes les marques
        public async Task<IActionResult> Index()
        {
            var brands = await _repository.GetAllAsync();
            return View(brands);

        }

        // GET: CarBrand/Details/5
        // Détails d'une marque
        public async Task<IActionResult> Details(int id)
        {
            var brand = await _repository.GetByIdAsync(id);
            if (brand == null)
            {
                return NotFound();
            }
            return View(brand);
        }

        // GET: CarBrand/Create
        // Formulaire de création
        public IActionResult CarBrand()
        {
            return View();
        }

        // POST: CarBrand/Create
        // Traiter l'ajout
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CarBrand([Bind("Name")] CarBrand carBrand)
        {
            if (ModelState.IsValid)
            {
                // Vérifier si la marque existe déjà
                var existingBrand = await _repository.GetByNameAsync(carBrand.Name);
                if (existingBrand != null)
                {
                    ModelState.AddModelError("Name", "Cette marque existe déjà.");
                    return View(carBrand);
                }

                await _repository.AddAsync(carBrand);
                TempData["SuccessMessage"] = "Marque ajoutée avec succès !";
                return RedirectToAction(nameof(Index));
            }
            return View(carBrand);
        }

        // GET: CarBrand/Edit/5
        // Formulaire de modification
        public async Task<IActionResult> Edit(int id)
        {
            var brand = await _repository.GetByIdAsync(id);
            if (brand == null)
            {
                return NotFound();
            }
            return View(brand);
        }

        // POST: CarBrand/Edit/5
        // Traiter la modification
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] CarBrand carBrand)
        {
            if (id != carBrand.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var existingBrand = await _repository.GetByNameAsync(carBrand.Name);
                if (existingBrand != null && existingBrand.Id != id)
                {
                    ModelState.AddModelError("Name", "Cette marque existe déjà.");
                    return View(carBrand);
                }

                await _repository.UpdateAsync(carBrand);
                TempData["SuccessMessage"] = "Marque modifiée avec succès !";
                return RedirectToAction(nameof(Index));
            }
            return View(carBrand);
        }

        // GET: CarBrand/Delete/5
        // Confirmation de suppression
        public async Task<IActionResult> Delete(int id)
        {
            var brand = await _repository.GetByIdAsync(id);
            if (brand == null)
            {
                return NotFound();
            }
            return View(brand);
        }

        // POST: CarBrand/Delete/5
        // Traiter la suppression
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var result = await _repository.DeleteAsync(id);
            if (result)
            {
                TempData["SuccessMessage"] = "Marque supprimée avec succès !";
            }
            else
            {
                TempData["ErrorMessage"] = "Erreur lors de la suppression.";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}