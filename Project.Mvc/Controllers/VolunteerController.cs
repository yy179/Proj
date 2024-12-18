﻿using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Abstractions.Services;
using Project.Entities;

namespace Project.Mvc.Controllers
{
    [Authorize]
    public class VolunteerController : Controller
    {
        private readonly IVolunteerOrganizationService _volunteerOrganizationService;
        private readonly IVolunteerService _volunteerService;
        private readonly IOrganizationService _organizationService;

        public VolunteerController(
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
            var volunteers = await _volunteerService.GetAllAsync();

            return View(volunteers);
        }

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Details(int id)
        {
            var volunteer = await _volunteerService.GetByIdAsync(id);
            var organizations = await _volunteerService.GetOrganizationsAsync(id);
            ViewBag.Organizations = organizations;
            var requests = await _volunteerService.GetActiveRequestsAsync(id);
            ViewBag.Requests = requests;
            if (volunteer == null)
            {
                return NotFound($"Volunteer with ID {id} not found.");
            }
            return View(volunteer);
        }

        [Authorize(Roles = "admin")]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(VolunteerEntity volunteer)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _volunteerService.AddAsync(volunteer);
                    return RedirectToAction(nameof(Index));
                }
                catch (ValidationException ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            return View(volunteer);
        }

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(int id)
        {
            var volunteer = await _volunteerService.GetByIdAsync(id);
            if (volunteer == null)
            {
                return NotFound();
            }
            return View(volunteer);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(VolunteerEntity volunteer)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _volunteerService.UpdateAsync(volunteer);
                    return RedirectToAction(nameof(Index));
                }
                catch (ValidationException ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            return View(volunteer);
        }

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var volunteer = await _volunteerService.GetByIdAsync(id);
            if (volunteer == null)
            {
                return NotFound();
            }
            return View(volunteer);
        }

        [Authorize(Roles = "admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _volunteerService.DeleteAsync(id);
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
        public async Task<IActionResult> AddToOrganization(int volunteerId, int organizationId)
        {
            if (volunteerId <= 0 || organizationId <= 0)
            {
                ModelState.AddModelError(string.Empty, "Invalid volunteer or organization ID.");
                return RedirectToAction("Index");
            }
            try
            {
                await _volunteerOrganizationService.AddVolunteerToOrganizationAsync(volunteerId, organizationId);
                return RedirectToAction("Details", "Organization", new { id = organizationId });
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return RedirectToAction("Index");
            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "An unexpected error occurred. Please try again later.");
                return RedirectToAction("Index");
            }
        }

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> AddToOrganization(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            var volunteer = await _volunteerService.GetByIdAsync(id);
            if (volunteer == null)
            {
                return NotFound();
            }

            var organizations = await _organizationService.GetAllAsync();
            ViewBag.Volunteer = volunteer;
            return View(organizations);
        }
    }
}
