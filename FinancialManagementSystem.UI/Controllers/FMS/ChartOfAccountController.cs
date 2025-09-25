using FinancialManagementSystem.Application;
using FinancialManagementSystem.Core;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FinancialManagementSystem.UI.Controllers
{
    [Authorize]
    public class ChartOfAccountController : BaseController
    {
        private readonly ISender _sender;

        public ChartOfAccountController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int page = 1)
        {
            var result = await _sender.Send(new ChartOfAccountGetAllDataQuery());

            var accounts = result.Match(
                success => success,
                error => new List<ChartOfAccountReadDto>()
            );

            return View(accounts);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await LoadDropdowns();
            return View(new ChartOfAccountCreateDto());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ChartOfAccountCreateDto model)
        {
            if (!ModelState.IsValid)
            {
                await LoadDropdowns();
                return View(model);
            }

            var result = await _sender.Send(new AddChartOfAccountCommand(model));

            if (result.IsError)
            {
                var duplicateError = result.Errors.FirstOrDefault(e => e.Code == "duplicate");
                if (duplicateError != null)
                {
                    ModelState.AddModelError(nameof(model.AccountCode), duplicateError.Description);
                    await LoadDropdowns();
                    return View(model);
                }
            }

            TempData["SuccessMessage"] = "Account created successfully!";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var result = await _sender.Send(new ChartOfAccountGetDataQuery(id));
            if (result.IsError)
                return NotFound();

            var success = result.Value;

            var dto = new ChartOfAccountUpdateDto
            {
                Id = success.Id,
                AccountCode = success.AccountCode,
                AccountName = success.AccountName,
                AccountType = success.AccountType,
                ParentId = success.ParentId,
                IsActive = (bool)success.IsActive
            };

            await LoadDropdowns(dto.Id, dto.ParentId, dto.AccountType);
            return View(dto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ChartOfAccountUpdateDto model)
        {
            if (!ModelState.IsValid)
            {
                await LoadDropdowns(model.Id, model.ParentId, model.AccountType);
                return View(model);
            }

            var result = await _sender.Send(new UpdateChartOfAccountCommand(model));

            if (result.IsError)
            {
                ModelState.AddModelError("", result.Errors.FirstOrDefault().Description ?? "Update failed.");
                await LoadDropdowns(model.Id, model.ParentId, model.AccountType);
                return View(model);
            }

            TempData["SuccessMessage"] = "Account updated successfully!";
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _sender.Send(new DeleteChartOfAccountCommand(id));

            if (response.IsError)
            {
                TempData["ErrorMessage"] = response.FirstError.Description;
            }
            else
            {
                TempData["SuccessMessage"] = "Account deleted successfully.";
            }

            return RedirectToAction(nameof(Index));
        }

        private async Task LoadDropdowns(int? excludeId = null, int? selectedParentId = null, string selectedAccountType = null)
        {
            var result = await _sender.Send(new ChartOfAccountGetAllDataQuery());

            ViewBag.ParentAccounts = result.IsError ? new List<SelectListItem>() :
                result.Value
                    .Where(c => c.Id != excludeId)
                    .Select(c => new SelectListItem
                    {
                        Value = c.Id.ToString(),
                        Text = c.AccountName,
                        Selected = selectedParentId.HasValue && c.Id == selectedParentId.Value
                    }).ToList();

            var accountTypes = new List<string> { "Asset", "Liability", "Income", "Expense" };
            ViewBag.AccountTypes = accountTypes
                .Select(t => new SelectListItem
                {
                    Value = t,
                    Text = t,
                    Selected = t == selectedAccountType
                }).ToList();
        }
    }
}
