using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RealEstate.Core;
using RealEstate.Core.Entities.RealEstateEntities;
using RealEstate.Core.Entities.RequestsEntites;
using RealEstate.Data;
using RealEstate.Data.IRepos;
using RealEstate.Data.SqlDBContext;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace RealEstateWebsite.Controllers
{
    [Authorize]
    public class RequestsController : Controller
    {
        private readonly IRequestMethods<Requests> helper;
        private readonly IAuthorizationService authorizationService;
        private readonly IRealEstateRepo<RealEstateProperty> real;
        private readonly UserManager<IdentityUser> userManager;
        private string UserId = "";
        public RequestsController(
            IRequestMethods<Requests> helper,
            IAuthorizationService authorizationService,
            IRealEstateRepo<RealEstateProperty> real,
            UserManager<IdentityUser> userManager)
        {
            this.helper = helper;
            this.authorizationService = authorizationService;
            this.real = real;
            this.userManager = userManager;
        }
        [Authorize]
        [HttpPost("PostRequests")]
        public async Task<IActionResult> PostRequests(int postId, string posterId, string userId, bool isRequestsSent, bool isRequestsReceived)
        {
            try
            {
                await SetUser(); // Ensure this method sets the user context properly
                userId = UserId;

                var data = real.GetData().FirstOrDefault(x => x.Id == postId);

                if (data == null)
                {
                    return BadRequest(new { success = false, message = "Data not found." });
                }
                if (isRequestsSent)
                {
                    isRequestsReceived = true; // Ensure isRequestsReceived is set to true
                }
                // Add or update the request
                helper.AddRequest(userId, postId, posterId, isRequestsReceived, isRequestsSent);

                // Check if we need to remove the request
                if (!isRequestsSent)
                {
                    // Remove the request if isRequestsSent is false
                    using (var dbContext = new DBData())
                    {
                        var request = await dbContext.Requests.FirstOrDefaultAsync(p =>
                            p.PostId == postId &&
                            p.UserId == userId &&
                            p.IsRequestsSent == false && // Check for false to remove the request
                            p.PosterId == posterId);

                        if (request != null)
                        {
                            dbContext.Requests.Remove(request);
                            await dbContext.SaveChangesAsync();
                            return Ok(new { success = true, message = "Request removed successfully." });
                        }
                        else
                        {
                            return BadRequest(new { success = false, message = "Request not found for removal." });
                        }
                    }
                }

                // If isRequestsSent is true, just return success message for request sent
                return Ok(new { success = true, message = "Request sent successfully." });
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Exception in PostRequests: {ex.Message}");
                return BadRequest(new { success = false, message = ex.Message });
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
