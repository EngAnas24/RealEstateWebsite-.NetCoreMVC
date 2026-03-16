using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstate.Core;
using RealEstate.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using RealEstate.Data.IRepos;
using RealEstate.Core.Entities.ContactEntity;

namespace RealEstateWibsite.Controllers
{
    public class ContactController : Controller
    {
        private readonly IRealEstateRepo<Contact> data;
        private readonly IAuthorizationService authorizationService;
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private string UserId;

        public ContactController(IRealEstateRepo<Contact> data, IAuthorizationService authorizationService,
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager)
        {
            this.data = data;
            this.authorizationService = authorizationService;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        [Authorize]
        // GET: ContactController
        public ActionResult AllMessages()
        {
            return View(data.GetData());
        }
        [Authorize]
        // GET: ContactController
        public async Task<ActionResult> Index()
        {
            await SetUser();
            return View(data.GetDataByUserId(UserId));
        }

        // GET: ContactController/Details/5
        public ActionResult Details(int id)
        {
            return View(data.GetDataByUserId(UserId));
        }
        [Authorize]
        // GET: ContactController/Create
        public ActionResult Create()
        {
            return View();
        }
        [Authorize]

        // POST: ContactController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Contact collection)
        {
            try
            {
                await SetUser();
                collection.UserId=UserId;
                data.Add(collection);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ContactController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ContactController/Edit/5
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

        // GET: ContactController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ContactController/Delete/5
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
        private async Task SetUser()
        {
            var user = await userManager.GetUserAsync(User);
            if (user != null)
            {
                UserId = user.Id;
            }
        }
    }
}
