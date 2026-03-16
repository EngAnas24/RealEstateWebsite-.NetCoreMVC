using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using RealEstate.Data.IRepos;
using RealEstate.Core.Entities.RealEstateEntities;
using RealEstate.Core.Entities.SavedPropEntity;
using RealEstate.Data.SqlDBContext;
using RealEstate.Core.Entities.RealEstatePropertyViewModels;

namespace RealEstateWibsite.Controllers
{
    public class RealEstatePropertyController : Controller
    {
        private readonly IRealEstateRepo<RealEstateProperty> data;
        private readonly IBaseHelper<offertype> offerType;
        private readonly IBaseHelper<propertytype> propertytype;
        private readonly IBaseHelper<propertystatus> propertystatus;
        private readonly IWebHostEnvironment webHost;
        private readonly IAuthorizationService authorizationService;
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private DBData dB;
        private readonly IBaseHelper<furnishedstatus> furnishedstatus;
        private readonly IBaseHelper<Bedrooms> bedrooms;
        private readonly IBaseHelper<Bathrooms> bathrooms;
        private readonly IBaseHelper<Balconys> balconys;
        private readonly ISavedPropertiesRepo<SavedProp> savedprop;
        private int pageItem;
        private Task<AuthorizationResult> result;
        private string UserId;
        private SavedProp SavedProp;
        private RealEstateProperty real;


        public RealEstatePropertyController(
            IRealEstateRepo<RealEstateProperty> data,
            IBaseHelper<offertype> OfferType,
            IBaseHelper<propertytype> Propertytype,
            IBaseHelper<propertystatus> Propertystatus,
            IBaseHelper<furnishedstatus> Furnishedstatus,
            IBaseHelper<Bedrooms> bedrooms,
            IBaseHelper<Bathrooms> bathrooms,
            IBaseHelper<Balconys> balconys,
            ISavedPropertiesRepo<SavedProp> savedprop,
            IWebHostEnvironment webHost,
            IAuthorizationService authorizationService,
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,DBData dB)
        {
            this.data = data;
            offerType = OfferType;
            propertytype = Propertytype;
            propertystatus = Propertystatus;
            this.webHost = webHost;
            this.authorizationService = authorizationService;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.dB = dB;
            furnishedstatus = Furnishedstatus;
            this.bedrooms = bedrooms;
            this.bathrooms = bathrooms;
            this.balconys = balconys;
            this.savedprop = savedprop;
            pageItem = 4 * 3;
        }

        [HttpGet]
        public IActionResult FilterSearch()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> FilterSearch(RealEstateProperty real)
        {
            try
            {
                if (real == null)
                {
                    return View();
                }

                var filtersearch = data.GetData();
                if(filtersearch == null)
                    return RedirectToPage("/FilterSearch");

                var searchResults = filtersearch
                    .Where(p => p.propertyaddress.ToUpper().Contains(real.propertyaddress.ToUpper())
                             && p.OffertypeLiist == real.OffertypeLiist
                             && p.PropertytypeList == real.PropertytypeList
                             && p.PropertystatusList == real.PropertystatusList
                             && p.FurnishedstatusList == real.FurnishedstatusList)
                    .ToList();

                TempData["SearchResults"] = JsonSerializer.Serialize(searchResults);

                return RedirectToPage("/SearchResult");
            }
            catch (Exception ex)
            {
                // Log the exception (ex) here using your preferred logging framework
                ViewBag.ErrorMessage = "An error occurred while processing your request.";
                return View(new List<RealEstateProperty>());
            }
        }




        public IActionResult GerneralIndex(int?id)
        {

            if (id == 0 || id == null)
            {
                dB = new DBData();

                var saleProperties = data.GetData().ToList();
                return View(saleProperties.Take(pageItem));
            }
            else
            {
                dB = new DBData();
                var saleProperties = data.GetData().ToList();
                return View(saleProperties.Where(x => x.Id > id).Take(pageItem));
            }
        }

        public async Task<IActionResult> MyPosts(int? id)
        {

            await SetUser();
          
            if (signInManager.IsSignedIn(User))
            {
                if (id == 0 || id == null)
                {
                    return View(data
                        .GetDataByUserId(UserId).Take(pageItem));
                }
                else
                {
                    var getdata = data
                        .GetDataByUserId(UserId).Where(x => x.Id > id);

                    return View(getdata.Take(pageItem));
                }
            }
            else
            {
                return Redirect("/Identity/Account/Login");
            }

        }




        public ActionResult BuyingIndex(int? id)
        {
            if (id == 0 || id == null)
            {
                dB = new DBData();

                var saleProperties = data.GetData()
                    .Where(property => property.OffertypeLiist == "sale").ToList();

                return View(saleProperties.Take(pageItem));
            }
            else
            {
                dB = new DBData();

                var saleProperties = data.GetData()
                    .Where(property => property.OffertypeLiist == "sale").ToList();

                return View(saleProperties.Where(x => x.Id > id).Take(pageItem));
            }

        }

        [Authorize]
        public async Task<ActionResult> SavedProperty(int? id)
        {
            await SetUser();
            if (id == 0 || id == null)
            {
                dB = new DBData();

                var savedProperties = savedprop.
                    GetData().Where(x=>x.IsSaved&&x.UserId==UserId);

                return View(savedProperties.Take(pageItem));
            }
            else
            {
                dB = new DBData();

                var savedProperties = savedprop
                    .GetData().Where(x => x.IsSaved && x.UserId == UserId);

                return View(savedProperties.Where(x => x.Id > id).Take(pageItem));
            }

        }


        public ActionResult RentingIndex(int? id)
        {

            if (id == 0 || id == null)
            {
                dB = new DBData();

                var rentProperties = data.GetData()
                    .Where(property => property.OffertypeLiist == "rent").ToList();

                return View(rentProperties.Take(pageItem));
            }
            else
            {
                dB = new DBData();

                var rentProperties = data
                    .GetData().Where(property => property.OffertypeLiist == "rent").ToList();

                return View(rentProperties.Where(x => x.Id > id).Take(pageItem));
            }

        }



        [AllowAnonymous]
        // GET: HomeController/Details/5
        public ActionResult Details(int id)
        {

            return View(data.Find(id));

        }

        [Authorize]
        [HttpPost("ToggleStatus")]
        public async Task <IActionResult> ToggleStatus(int id, string userId, bool isSave)
        {
            try
            {
                await SetUser();
                UserId = userId;
                // Assuming savedprop is an instance of SavedPropEntity
                // Use the TogglePostStatus method from SavedPropEntity
                savedprop.TogglePostStatus(userId, id, isSave);
          
                string message = isSave ? "Property saved successfully" : "Property unsaved successfully";
                if (!isSave)
                {
                    using (var dbContext = new DBData())
                    {
                        var property = await  dbContext.savedProps
                            .FirstOrDefaultAsync(p => p.PostId == id && p.UserId == userId&&p.IsSaved==isSave);
                        if (property != null)
                        {
                            dbContext.savedProps.Remove(property);
                         await    dbContext.SaveChangesAsync();
                        }
                    }
                }
                return Ok(new { message = message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Error toggling property status: " + ex.Message });
            }
        }

        [Authorize]
        // GET: HomeController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HomeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(RealEstatePropertyViewModel use, string OffertypeLiist)
        {
            try
            {
                int counter = 0;

                // Get the currently logged-in user
                var user = await userManager.GetUserAsync(User);

                // Use the user's Id and UserName
                var realEstate = new RealEstateProperty
                {
                    Id = use.Id,
                    UserId = user.Id,
                    UserName = user.UserName,
                    UserPhonenumber=user.PhoneNumber??"",
                    propertyname = use.propertyname,
                    propertyprice = use.propertyprice,
                    depositamount = use.depositamount,
                    propertyaddress = use.propertyaddress,
                    OffertypeLiist = use.OffertypeLiist,
                    PropertytypeList = use.PropertytypeList,
                    PropertystatusList = use.PropertystatusList,
                    FurnishedstatusList = use.FurnishedstatusList,
                    bedroomsList = use.bedroomsList,
                    bathroomsList = use.bathroomsList,
                    balconysList = use.balconysList,

                    OffertypeId = offerType.GetData()
                    .Where(x => x.Offertype == use.OffertypeLiist)
                    .Select(x => x.id).FirstOrDefault(),
                    Offertype = use.Offertype,

                    PropertytypeId = propertytype.GetData()
                    .Where(x => x.Propertytype == use.PropertytypeList)
                    .Select(x => x.id).FirstOrDefault(),
                    Propertytype = use.Propertytype,

                    PropertystatusId = propertystatus.GetData()
                    .Where(x => x.Propertystatus == use.PropertystatusList)
                    .Select(x => x.id).FirstOrDefault(),
                    Propertystatus = use.Propertystatus,

                    FurnishedstatusId = furnishedstatus.GetData()
                    .Where(x => x.Furnishedstatus == use.FurnishedstatusList)
                    .Select(x => x.id).FirstOrDefault(),
                    Furnishedstatus = use.Furnishedstatus,

                    bedroomsId = bedrooms.GetData()
                    .Where(x => x.bedrooms == use.bedroomsList)
                    .Select(x => x.id).FirstOrDefault(),
                    bedrooms = use.bedrooms,

                    bathroomsId = bathrooms.GetData()
                    .Where(x => x.bathrooms == use.bathroomsList)
                    .Select(x => x.id).FirstOrDefault(),
                    bathrooms = use.bathrooms,

                    balconysId = balconys.GetData()
                    .Where(x => x.balconys == use.balconysList)
                    .Select(x => x.id).FirstOrDefault(),
                    balconys = use.balconys,

                    carpetarea = use.carpetarea ?? "",
                    propertyage = use.propertyage ?? "",
                    totalfloors = use.totalfloors ?? "",
                    floorroom = use.floorroom ?? "",
                    propertydescription = use.propertydescription ?? "",

                    ImageUrl1 = UploadFile(use.File1, "uploads"),
                    ImageUrl2 = UploadFile(use.File2, "uploads"),
                    ImageUrl3 = UploadFile(use.File3, "uploads"),
                    ImageUrl4 = UploadFile(use.File4, "uploads"),
                    ImageUrl5 = UploadFile(use.File5, "uploads"),
                    VideoUrl = await UploadVideoFile(use.VideoFile, "videos"),

                    AddedDate = DateTime.Now,
                    IsSaved=use.IsSaved
                    
                  
                };

                // Increment the counter for each uploaded image
                if (!string.IsNullOrEmpty(realEstate.ImageUrl1))
                    counter++;
                if (!string.IsNullOrEmpty(realEstate.ImageUrl2))
                    counter++;
                if (!string.IsNullOrEmpty(realEstate.ImageUrl3))
                    counter++;
                if (!string.IsNullOrEmpty(realEstate.ImageUrl4))
                    counter++;
                if (!string.IsNullOrEmpty(realEstate.ImageUrl5))
                    counter++;

                // Set the total number of uploaded images
                realEstate.ImagesCounts = counter;

                // Add the real estate property to the data repository
                data.Add(realEstate);

                // Redirect based on offer type
                if (!string.IsNullOrEmpty(OffertypeLiist))
                {
                    if (OffertypeLiist == "sale")
                    {
                        return RedirectToAction("BuyingIndex", "RealEstateProperty");
                    }
                    else if (OffertypeLiist == "rent")
                    {
                        return RedirectToAction("RentingIndex", "RealEstateProperty");
                    }
                }

                // Default redirect
                return RedirectToAction("BuyingIndex", "RealEstateProperty");
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                return View(); // Consider returning an error view or message
            }
        }

        // GET: HomeController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var test = data.Find(id);
            RealEstatePropertyViewModel realEstate = new RealEstatePropertyViewModel
            {
                   // Get the currently logged-in user
              
                 Id =test.Id,
                propertyname = test.propertyname,
                propertyprice = test.propertyprice,
                depositamount = test.depositamount,
                propertyaddress = test.propertyaddress,
                OffertypeLiist = test.OffertypeLiist,
                PropertytypeList = test.PropertytypeList,
                PropertystatusList = test.PropertystatusList,
                FurnishedstatusList = test.FurnishedstatusList,
                bedroomsList = test.bedroomsList,
                bathroomsList = test.bathroomsList,
                balconysList = test.balconysList,

                OffertypeId = offerType.GetData()
                .Where(x => x.Offertype == test.OffertypeLiist)
                .Select(x => x.id).FirstOrDefault(),

                PropertytypeId = propertytype.GetData()
                .Where(x => x.Propertytype == test.PropertytypeList)
                .Select(x => x.id).FirstOrDefault(),
                Propertytype = test.Propertytype,

                PropertystatusId = propertystatus.GetData()
                .Where(x => x.Propertystatus == test.PropertystatusList)
                .Select(x => x.id).FirstOrDefault(),
                Propertystatus = test.Propertystatus,

                FurnishedstatusId = furnishedstatus.GetData()
                .Where(x => x.Furnishedstatus == test.FurnishedstatusList)
                .Select(x => x.id).FirstOrDefault(),
                Furnishedstatus = test.Furnishedstatus,

                bedroomsId = bedrooms.GetData()
                .Where(x => x.bedrooms == test.bedroomsList)
                .Select(x => x.id).FirstOrDefault(),
                bedrooms = test.bedrooms,

                bathroomsId = bathrooms.GetData()
                .Where(x => x.bathrooms == test.bathroomsList)
                .Select(x => x.id).FirstOrDefault(),
                bathrooms = test.bathrooms,

                balconysId = balconys.GetData()
                .Where(x => x.balconys == test.balconysList)
                .Select(x => x.id).FirstOrDefault(),
                balconys = test.balconys,

                carpetarea = test.carpetarea,
                propertyage = test.propertyage,
                totalfloors = test.totalfloors,
                floorroom = test.floorroom,
                propertydescription = test.propertydescription,



            };
            return View(realEstate);
        }

        // POST: HomeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, RealEstatePropertyViewModel use, string OffertypeList)
        {
            try
            {
                var user = await userManager.GetUserAsync(User);

                // Retrieve existing property from the database
                var existingProperty = data.Find(id);

                // Map the changes from the view model
                existingProperty.UserPhonenumber = user.PhoneNumber ?? "";
                existingProperty.propertyname = use.propertyname;
                existingProperty.propertyprice = use.propertyprice;
                existingProperty.depositamount = use.depositamount;
                existingProperty.propertyaddress = use.propertyaddress;
                existingProperty.OffertypeLiist = use.OffertypeLiist; // Corrected property name
                existingProperty.PropertytypeList = use.PropertytypeList;
                existingProperty.PropertystatusList = use.PropertystatusList;
                existingProperty.FurnishedstatusList = use.FurnishedstatusList;
                existingProperty.bedroomsList = use.bedroomsList;
                existingProperty.bathroomsList = use.bathroomsList;
                existingProperty.balconysList = use.balconysList;

                existingProperty.OffertypeId = offerType.GetData()
                    .FirstOrDefault(x => x.Offertype == use.OffertypeLiist)?.id??0;
                existingProperty.PropertytypeId = propertytype.GetData()
                    .FirstOrDefault(x => x.Propertytype == use.PropertytypeList)?.id ?? 0;
                existingProperty.PropertystatusId = propertystatus.GetData()
                    .FirstOrDefault(x => x.Propertystatus == use.PropertystatusList)?.id ?? 0;
                existingProperty.FurnishedstatusId = furnishedstatus.GetData()
                    .FirstOrDefault(x => x.Furnishedstatus == use.FurnishedstatusList)?.id ?? 0;
                existingProperty.bedroomsId = bedrooms.GetData()
                    .FirstOrDefault(x => x.bedrooms == use.bedroomsList)?.id ?? 0;
                existingProperty.bathroomsId = bathrooms.GetData()
                    .FirstOrDefault(x => x.bathrooms == use.bathroomsList)?.id ?? 0;
                existingProperty.balconysId = balconys.GetData()
                    .FirstOrDefault(x => x.balconys == use.balconysList)?.id ?? 0;

                existingProperty.Offertype = use.Offertype;
                existingProperty.Propertytype = use.Propertytype;
                existingProperty.Propertystatus = use.Propertystatus;
                existingProperty.Furnishedstatus = use.Furnishedstatus;
                existingProperty.bedrooms = use.bedrooms;
                existingProperty.bathrooms = use.bathrooms;
                existingProperty.balconys = use.balconys;



                existingProperty.IsSaved = use.IsSaved;
                existingProperty.carpetarea = use.carpetarea ?? "";
                existingProperty.propertyage = use.propertyage ?? "";
                existingProperty.totalfloors = use.totalfloors ?? "";
                existingProperty.floorroom = use.floorroom ?? "";
                existingProperty.propertydescription = use.propertydescription ?? "";
                existingProperty.AddedDate = DateTime.Now ;
                // Check and handle file uploads
                if (use.File1 != null)
                {
                    existingProperty.ImageUrl1 = UploadImage(use.File1, existingProperty.ImageUrl1);
                }
                if (use.File2 != null)
                {
                    existingProperty.ImageUrl2 = UploadImage(use.File2, existingProperty.ImageUrl2);
                }
                if (use.File3 != null)
                {
                    existingProperty.ImageUrl3 = UploadImage(use.File3, existingProperty.ImageUrl3);
                }
                if (use.File4 != null)
                {
                    existingProperty.ImageUrl4 = UploadImage(use.File4, existingProperty.ImageUrl4);
                }
                if (use.File5 != null)
                {
                    existingProperty.ImageUrl5 = UploadImage(use.File5, existingProperty.ImageUrl5);
                }
                if (use.VideoFile != null)
                {
                    existingProperty.VideoUrl =  UploadImage(use.VideoFile, existingProperty.VideoUrl);
                }
                // Count the total number of uploaded images
                int counter = 0;
                if (!string.IsNullOrEmpty(existingProperty.ImageUrl1))
                    counter++;
                if (!string.IsNullOrEmpty(existingProperty.ImageUrl2))
                    counter++;
                if (!string.IsNullOrEmpty(existingProperty.ImageUrl3))
                    counter++;
                if (!string.IsNullOrEmpty(existingProperty.ImageUrl4))
                    counter++;
                if (!string.IsNullOrEmpty(existingProperty.ImageUrl5))
                    counter++;

                existingProperty.ImagesCounts = counter;

                // Update the property in the database
                data.Update(existingProperty, id);

                if (!string.IsNullOrEmpty(OffertypeList))
                {
                    if (OffertypeList == "sale" || OffertypeList == "resale") // Combined conditions for sale and resale
                    {
                        return RedirectToAction("BuyingIndex", "RealEstateProperty");
                    }
                    else if (OffertypeList == "rent")
                    {
                        return RedirectToAction("RentingIndex", "RealEstateProperty");
                    }
                }

                return RedirectToAction("BuyingIndex", "RealEstateProperty");
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Error updating user: " + ex.Message });
            }
        }

        public ActionResult Delete(int id)
        {
            return View(data.Find(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteAjax(int id, string OffertypeLiist)
        {
            try
            {
                var prop = data.Find(id);
                if (prop == null) {
                    return NotFound();
                }
                DeleteImage(prop.ImageUrl1) ;
                DeleteImage(prop.ImageUrl2);
                DeleteImage(prop.ImageUrl3);
                DeleteImage(prop.ImageUrl4);
                DeleteImage(prop.ImageUrl5);
                DeleteImage(prop.VideoUrl);


                data.Delete(id);
              
                string redirectUrl = Url.Action("GerneralIndex", "RealEstateProperty");

                if (!string.IsNullOrEmpty(OffertypeLiist))
                {
                    if (OffertypeLiist == "sale")
                    {
                        redirectUrl = Url.Action("BuyingIndex", "RealEstateProperty");
                    }
                    else if (OffertypeLiist == "rent")
                    {
                        redirectUrl = Url.Action("RentingIndex", "RealEstateProperty");
                    }
                }

                return Json(new { success = true, redirectUrl });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Error deleting property: {ex.Message}" });
            }
        }

        public ActionResult DeleteMyPosts(int id)
        {
            return View(data.Find(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteMyPostsAjax(int id, string OffertypeLiist)
        {
            try
            {
                var prop = data.Find(id);
                if (prop == null)
                {
                    return NotFound();
                }
                if (prop.ImageUrl1 != null)
                {
                    DeleteImage(prop.ImageUrl1);
                }
                if (prop.ImageUrl2 != null)
                {
                    DeleteImage(prop.ImageUrl2);
                }
                if (prop.ImageUrl3 != null)
                {
                    DeleteImage(prop.ImageUrl3);
                }
                if (prop.ImageUrl4 != null)
                {
                    DeleteImage(prop.ImageUrl4);
                }
                if (prop.ImageUrl5 != null)
                {
                    DeleteImage(prop.ImageUrl5);
                }
                if (prop.VideoUrl != null)
                {
                    DeleteImage(prop.VideoUrl);
                }
                data.Delete(id);

                string redirectUrl = Url.Action("MyPosts", "RealEstateProperty");

                return Json(new { success = true, redirectUrl });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Error deleting property: {ex.Message}" });
            }
        }
     
        public async Task<string> UploadVideoFile(IFormFile file, string folder)
        {
            if (file != null)
            {
                var fileDir = Path.Combine(webHost.WebRootPath, folder);
                var fileName = Guid.NewGuid() + "-" + file.FileName;
                var filePath = Path.Combine(fileDir, fileName);

                using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }

                return fileName;
            }
            else
            {
                return string.Empty;
            }
        }


        public string UploadFile(IFormFile file, string folder)
        {
            if (file != null)
            {
                var fileDir = Path.Combine(webHost.WebRootPath, folder);
                var fileName = Guid.NewGuid() + "-" + file.FileName;
                var filePath = Path.Combine(fileDir, fileName);

                using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                    return fileName;
                }

            }
            else
            {
                return string.Empty;
            }
        }
        public string UploadImage(IFormFile file, string imageUrl)
        {

            if (file != null)
            {
                string uploads = Path.Combine(webHost.WebRootPath, "uploads");
                string filname = Guid.NewGuid() + "-" + file.FileName;
                string newPath = Path.Combine(uploads, filname);
                string oldPath = Path.Combine(uploads, imageUrl);

                if (oldPath != newPath)
                {
                    if (System.IO.File.Exists(oldPath))
                    {
                        System.IO.File.Delete(oldPath);
                    }
                    file.CopyTo(new FileStream(newPath, FileMode.Create));
                }

                return filname;
            }

            return imageUrl;
        }
       public void DeleteImage(string imageUrl)
        {
            string uploadsFolder = Path.Combine(webHost.WebRootPath, "uploads");
            string imagePath = Path.Combine(uploadsFolder, imageUrl);

            // Delete the image file if it exists
            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
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
