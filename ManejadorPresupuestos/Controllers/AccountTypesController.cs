using Dapper;
using ManejadorPresupuestos.Data.Repositories.Interfaces;
using ManejadorPresupuestos.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Reflection.Metadata.Ecma335;

namespace ManejadorPresupuestos.Controllers
{
    public class AccountTypesController : Controller
    {
        private readonly IAccountTypeRepository accountTypeRepository;
        public AccountTypesController(IAccountTypeRepository accountTypeRepository)
        {
           this.accountTypeRepository = accountTypeRepository;
        }
        // GET: AccountTypesController
        public ActionResult Index()
        {
            return View();
        }

        // GET: AccountTypesController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AccountTypesController/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: AccountTypesController/Create
        [HttpPost]
        public async Task<IActionResult> Create(AccountType accountType)
        {
            if (!ModelState.IsValid)
            {
                return View(accountType);
            }

            accountType.UserId = 1;

            var accountExists = await accountTypeRepository.Exists(accountType.AccountName, accountType.UserId);

            if (accountExists)
            {
                ModelState.AddModelError(nameof(accountType.AccountName), 
                    $"La cuenta {accountType.AccountName} ya existe");
                return View(accountType);
            }

            await accountTypeRepository.Create(accountType);

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> CheckAccountTypeExists(string accountName)
        {
            var UserId = 1;
            var alreadyExists = await accountTypeRepository.Exists(accountName, UserId);

            if (alreadyExists)
            {
                return Json($"La cuenta {accountName} ya existe");
            }

            return Json(true);
        }

        // GET: AccountTypesController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AccountTypesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AccountTypesController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AccountTypesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
