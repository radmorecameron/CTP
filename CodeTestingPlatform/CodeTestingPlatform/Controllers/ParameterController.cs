using CodeTestingPlatform.DatabaseEntities.Local;
using CodeTestingPlatform.Models;
using CodeTestingPlatform.Models.Validation;
using CodeTestingPlatform.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeTestingPlatform.Controllers {
    [Authorize(Roles = "TE")]
    [SessionTimeout]
    public class ParameterController : Controller {
        private readonly IParameterService _parameterService;
        public ParameterController(IParameterService parameterService) {
            _parameterService = parameterService;
        }
        public IActionResult Index() {
            return View();
        }

        // GET : Parameter/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int id) {
            Parameter parameterObj = await _parameterService.FindByIdAsync(id);
            if (parameterObj == null) {
                return NotFound();
            }
            return View(new ParameterDTO(parameterObj));
        }

        // POST : TestCase/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ParameterDTO parameter) {
            if (id != parameter.ParameterId) {
                return NotFound();
            }

            if (ModelState.IsValid) {
                await _parameterService.UpdateAsync(parameter.GetParameter());
                return RedirectToAction("Edit", "TestCase", new { id = parameter.TestCaseId });
            }
            return View(parameter);
        }
    }
}
