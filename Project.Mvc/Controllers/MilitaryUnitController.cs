using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Abstractions.Services;
using Project.Business.Services;
using Project.Entities;

namespace Project.Mvc.Controllers
{
    [Authorize]
    public class MilitaryUnitController : Controller
    {
        private readonly IMilitaryUnitService _militaryUnitService;
        private readonly IContactPersonService _contactPersonService;
        private readonly IMilitaryUnitContactPersonService _militaryUnitContactPersonService;
        public MilitaryUnitController(IMilitaryUnitService militaryUnitService, IContactPersonService contactPersonService, IMilitaryUnitContactPersonService militaryUnitContactPersonService)
        {
            _militaryUnitService = militaryUnitService;
            _contactPersonService = contactPersonService;
            _militaryUnitContactPersonService = militaryUnitContactPersonService;
        }

        public async Task<IActionResult> Index()
        {
            var militaryUnits = await _militaryUnitService.GetAllAsync();
            return View(militaryUnits);
        }

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Details(int id)
        {
            var contactPersons = await _militaryUnitService.GetContactPersonsAsync(id);
            var militaryUnit = await _militaryUnitService.GetByIdAsync(id);
            var requests = await _militaryUnitService.GetActiveRequestsAsync(id);
            ViewBag.Requests = requests;
            ViewBag.ContactPersons = contactPersons;
            if (militaryUnit == null)
            {
                return NotFound();
            }
            return View(militaryUnit);
        }

        [Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MilitaryUnitEntity militaryUnit)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _militaryUnitService.AddAsync(militaryUnit);
                    return RedirectToAction(nameof(Index));
                }
                catch (ValidationException ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            return View(militaryUnit);
        }

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(int id)
        {
            var militaryUnit = await _militaryUnitService.GetByIdAsync(id);
            if (militaryUnit == null)
            {
                return NotFound();
            }
            return View(militaryUnit);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(MilitaryUnitEntity militaryUnit)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _militaryUnitService.UpdateAsync(militaryUnit);
                    return RedirectToAction(nameof(Index));
                }
                catch (ValidationException ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            return View(militaryUnit);
        }

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var militaryUnit = await _militaryUnitService.GetByIdAsync(id);
            if (militaryUnit == null)
            {
                return NotFound();
            }
            return View(militaryUnit);
        }

        [Authorize(Roles = "admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _militaryUnitService.DeleteAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View();
            }
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveContactPerson(int contactPersonId, int militaryUnitId)
        {
            try
            {
                await _militaryUnitContactPersonService.RemoveContactPersonFromMilitaryUnit(contactPersonId, militaryUnitId);
                return RedirectToAction(nameof(Details), new { id = militaryUnitId });
            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                var organization = await _militaryUnitService.GetByIdAsync(militaryUnitId);
                var contactPersons = await _militaryUnitService.GetContactPersonsAsync(militaryUnitId);
                ViewBag.ContactPersons = contactPersons;
                var requests = await _militaryUnitService.GetActiveRequestsAsync(militaryUnitId);
                ViewBag.Requests = requests;
                return View("Details", organization);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return RedirectToAction(nameof(Details), new { id = militaryUnitId });
            }
        }
    }
}
