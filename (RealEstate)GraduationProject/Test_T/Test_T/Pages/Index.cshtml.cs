using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RealEstate.Core;
using RealEstate.Core.Entities.RealEstateEntities;
using RealEstate.Data;
using RealEstate.Data.IRepos;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
namespace Test_T.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IRealEstateRepo<RealEstate.Core.Entities.RealEstateEntities.RealEstateProperty> data;
        private string Message { get; set; }

        public IndexModel(IRealEstateRepo<RealEstate.Core.Entities.RealEstateEntities.RealEstateProperty> data)
        {
            this.data = data;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        public IActionResult OnPostSearch(RealEstate.Core.Entities.RealEstateEntities.RealEstateProperty searchParameters)
        {
            if (searchParameters == null)
            {
                // Handle null search parameters
                return BadRequest("Search parameters cannot be null.");
            }

            var allProperties = data.GetData();

            if (allProperties == null)
            {
                // Handle null property collection
                return BadRequest("Property data is not available.");
            }

            var searchResults = allProperties
                .Where(p => p.OffertypeLiist == searchParameters.OffertypeLiist
                         && p.PropertytypeList == searchParameters.PropertytypeList
                         && !string.IsNullOrEmpty(p.propertyaddress) &&
                        !string.IsNullOrEmpty(searchParameters.propertyaddress) &&
                        p.propertyaddress.ToUpper().Contains(searchParameters.propertyaddress.ToUpper()))
                .ToList();
            if (searchResults.Any()|| !searchResults.Any())
            {
                TempData["SearchResults"] = JsonSerializer.Serialize(searchResults);

                return RedirectToPage("/SearchResult");
            }
            return RedirectToPage("/Index");

        }

    }
}
