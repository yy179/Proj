using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Project.Abstractions.Services;
using Project.Entities;

namespace Project.Mvc.Controllers
{
    [Authorize]
    public class RequestController : Controller
    {
        private readonly IRequestService _requestService;
        private readonly IMilitaryUnitService _militaryUnitService;
        private readonly IVolunteerService _volunteerService;
        private readonly IOrganizationService _organizationService;

        public RequestController(
            IRequestService requestService,
            IMilitaryUnitService militaryUnitService,
            IVolunteerService volunteerService,
            IOrganizationService organizationService)
        {
            _requestService = requestService;
            _militaryUnitService = militaryUnitService;
            _volunteerService = volunteerService;
            _organizationService = organizationService;
        }

        public async Task<IActionResult> Index()
        {
            var requests = await _requestService.GetAllAsync();
            return View(requests);
        }

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Details(int id)
        {
            var request = await _requestService.GetByIdAsync(id);
            if (request == null)
            {
                return NotFound();
            }
            var volunteers = await _volunteerService.GetAllAsync();
            ViewBag.Volunteers = new SelectList(volunteers, "Id", "LastName");
            var organizations = await _organizationService.GetAllAsync();
            ViewBag.Organizations = new SelectList(organizations, "Id", "Name");
            var militaryUnit = await _militaryUnitService.GetByIdAsync(request.MilitaryUnitId);
            ViewBag.MilitaryUnit = militaryUnit;

            if (request.TakenByVolunteerId.HasValue)
            {
                ViewBag.TakenByVolunteer = await _volunteerService.GetByIdAsync(request.TakenByVolunteerId.Value);
            }

            if (request.OrganizationTakenById.HasValue)
            {
                ViewBag.TakenByOrganization = await _organizationService.GetByIdAsync(request.OrganizationTakenById.Value);
            }

            if (request.CompletedByVolunteerId.HasValue)
            {
                ViewBag.CompletedByVolunteer = await _volunteerService.GetByIdAsync(request.CompletedByVolunteerId.Value);
            }

            if (request.OrganizationCompletedById.HasValue)
            {
                ViewBag.CompletedByOrganization = await _organizationService.GetByIdAsync(request.OrganizationCompletedById.Value);
            }

            return View(request);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> TakeRequestAsVolunteer(int id, int volunteerId)
        {
            var request = await _requestService.GetByIdAsync(id);
            if (request == null)
            {
                return NotFound();
            }

            request.TakenByVolunteerId = volunteerId;
            await _requestService.UpdateAsync(request);

            return RedirectToAction(nameof(Details), new { id });
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> TakeRequestAsOrganization(int id, int organizationId)
        {
            var request = await _requestService.GetByIdAsync(id);
            if (request == null)
            {
                return NotFound();
            }

            request.OrganizationTakenById = organizationId;
            await _requestService.UpdateAsync(request);

            return RedirectToAction(nameof(Details), new { id });
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> CompleteRequestAsVolunteer(int id, int volunteerId)
        {
            var request = await _requestService.GetByIdAsync(id);
            if (request == null)
            {
                return NotFound();
            }

            request.CompletedByVolunteerId = volunteerId;
            request.IsActive = false;
            await _requestService.UpdateAsync(request);

            return RedirectToAction(nameof(Details), new { id });
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> CompleteRequestAsOrganization(int id, int organizationId)
        {
            var request = await _requestService.GetByIdAsync(id);
            if (request == null)
            {
                return NotFound();
            }

            request.OrganizationCompletedById = organizationId;
            request.IsActive = false;
            await _requestService.UpdateAsync(request);

            return RedirectToAction(nameof(Details), new { id });
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var militaryUnits = await _militaryUnitService.GetAllAsync();
            ViewBag.MilitaryUnits = new SelectList(militaryUnits, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Create(RequestEntity request)
        {
            request.IsActive = true;
            await _requestService.AddAsync(request);
            return RedirectToAction(nameof(Index));

        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var request = await _requestService.GetByIdAsync(id);
            if (request == null)
            {
                return NotFound();
            }

            await _requestService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> Complete(int id)
        {
            var request = await _requestService.GetByIdAsync(id);
            if (request == null)
            {
                return NotFound();
            }

            request.IsActive = false;
            await _requestService.UpdateAsync(request);

            return RedirectToAction(nameof(Index));
        }
    }
}
