using Microsoft.AspNetCore.Mvc.RazorPages;
using RealEstate.Core;
using RealEstate.Core.Entities.RealEstateEntities;
using System.Collections.Generic;
using System.Text.Json;

namespace Test_T.Pages
{
    public class SearchResultModel : PageModel
    {
        public List<RealEstate.Core.Entities.RealEstateEntities.RealEstateProperty> SearchResults { get; set; }
        public string Message { get; set; }

        public void OnGet()
        {
            if (TempData["SearchResults"] != null)
            {
                SearchResults = JsonSerializer.Deserialize<List<RealEstate.Core.Entities.RealEstateEntities.RealEstateProperty>>(TempData["SearchResults"] as string);
                if (SearchResults.Count == 0)
                {
                    Message = "No information available for your search.";
                }
            }
            else
            {
                Message = "No information available for your search.";
            }
        }
    }
}
