using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Abstractions.Services;
using Project.Entities;

namespace Project.Mvc.Controllers
{
    [Authorize]
    public class OrganizationController : Controller
    {
        private readonly IVolunteerOrganizationService _volunteerOrganizationService;
        private readonly IVolunteerService _volunteerService;
        private readonly IOrganizationService _organizationService;

        public OrganizationController(
            IVolunteerOrganizationService volunteerOrganizationService,
            IVolunteerService volunteerService,
            IOrganizationService organizationService)
        {
            _volunteerOrganizationService = volunteerOrganizationService;
            _volunteerService = volunteerService;
            _organizationService = organizationService;
        }

        public async Task<IActionResult> Index()
        {
            var organizations = await _organizationService.GetAllAsync();
            return View(organizations);
        }

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Details(int id)
        {
            var organization = await _organizationService.GetByIdAsync(id);
            var volunteers = await _organizationService.GetVolunteersAsync(id);
            ViewBag.Volunteers = volunteers;
            var requests = await _organizationService.GetActiveRequestsAsync(id);
            ViewBag.Requests = requests;
            if (organization == null)
            {
                return NotFound();
            }
            return View(organization);
        }

        [Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(OrganizationEntity organization)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _organizationService.AddAsync(organization);
                    return RedirectToAction(nameof(Index));
                }
                catch (ValidationException ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            return View(organization);
        }

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(int id)
        {
            var organization = await _organizationService.GetByIdAsync(id);
            if (organization == null)
            {
                return NotFound();
            }
            return View(organization);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(OrganizationEntity organization)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _organizationService.UpdateAsync(organization);
                    return RedirectToAction(nameof(Index));
                }
                catch (ValidationException ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            return View(organization);
        }

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var organization = await _organizationService.GetByIdAsync(id);
            if (organization == null)
            {
                return NotFound();
            }
            return View(organization);
        }

        [Authorize(Roles = "admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _organizationService.DeleteAsync(id);
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
        public async Task<IActionResult> RemoveVolunteer(int volunteerId, int organizationId)
        {
            try
            {
                await _volunteerOrganizationService.RemoveVolunteerFromOrganizationAsync(volunteerId, organizationId);
                return RedirectToAction(nameof(Details), new { id = organizationId });
            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                var organization = await _organizationService.GetByIdAsync(organizationId);
                var volunteers = await _organizationService.GetVolunteersAsync(organizationId);
                ViewBag.Volunteers = volunteers;
                var requests = await _organizationService.GetActiveRequestsAsync(organizationId);
                ViewBag.Requests = requests;
                return View("Details", organization);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return RedirectToAction(nameof(Details), new { id = organizationId });
            }
        }
    }
}
