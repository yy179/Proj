using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Project.Abstractions.Services;
using Project.Business.Services;
using Project.Entities;

namespace Project.Mvc.Controllers
{
    public class ContactPersonController : Controller
    {
        private readonly IMilitaryUnitService _militaryUnitService;
        private readonly IContactPersonService _contactPersonService;
        private readonly IMilitaryUnitContactPersonService _militaryUnitContactPersonService;
        public ContactPersonController(IMilitaryUnitService militaryUnitService, IContactPersonService contactPersonService, IMilitaryUnitContactPersonService militaryUnitContactPersonService)
        {
            _militaryUnitContactPersonService = militaryUnitContactPersonService;
            _militaryUnitService = militaryUnitService;
            _contactPersonService = contactPersonService;
        }

        public async Task<IActionResult> Index()
        {
            var contactPersons = await _contactPersonService.GetAllAsync();
            return View(contactPersons);
        }


        public async Task<IActionResult> Details(int id)
        {
            var contactPerson = await _contactPersonService.GetByIdAsync(id);
            if (contactPerson == null)
            {
                return NotFound();
            }

            return View(contactPerson);
        }



        public async Task<ActionResult> Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ContactPersonEntity contactPerson)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _contactPersonService.AddAsync(contactPerson);
                    return RedirectToAction(nameof(Index));
                }
                catch (ValidationException ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            return View(contactPerson);
        }



        public async Task<IActionResult> Edit(int id)
        {
            var contactPerson = await _contactPersonService.GetByIdAsync(id);
            if (contactPerson == null)
            {
                return NotFound();
            }
            return View(contactPerson);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ContactPersonEntity contactPerson)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _contactPersonService.UpdateAsync(contactPerson);
                    return RedirectToAction(nameof(Index));
                }
                catch (ValidationException ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            return View(contactPerson);
        }


        public async Task<IActionResult> Delete(int id)
        {
            var contactPerson = await _contactPersonService.GetByIdAsync(id);
            if (contactPerson == null)
            {
                return NotFound();
            }
            return View(contactPerson);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _contactPersonService.DeleteAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View();
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddToMilitaryUnit(int contactPersonId, int militaryUnitId)
        {
            try
            {
                await _militaryUnitContactPersonService.AddContactPersonToMilitaryUnit(contactPersonId, militaryUnitId);
                return RedirectToAction("Details", "MilitaryUnit", new { id = militaryUnitId });
            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return RedirectToAction("Index");
            }
        }
        public async Task<IActionResult> AddToMilitaryUnit(int id)
        {
            var contactPerson = await _contactPersonService.GetByIdAsync(id);
            if (contactPerson == null)
            {
                return NotFound();
            }

            var militaryUnit = await _militaryUnitService.GetAllAsync();
            ViewBag.ContactPerson = contactPerson;
            return View(militaryUnit);
        }
    }
}
