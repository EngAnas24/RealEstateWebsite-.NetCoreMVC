using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using RealEstate.Core;
using RealEstate.Core.Entities.RequestsEntites;
using RealEstate.Data.IRepos;
using RealEstate.Data.SqlDBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Data.Services
{
    public class RequestService : IRealEstateRepo<Requests>,IRequestMethods<Requests>
    {
       private DBData dB;
        Requests Requests;
        public RequestService()
        {
            dB = new DBData();
        }

        public void Add(Requests table)
        {
            dB.Requests.Add(table);
            dB.SaveChanges();
        }

        public void Delete(int id)
        {
            Requests = Find(id);
            dB.Requests.Remove(Requests);
            dB.SaveChanges();

        }

        public List<Requests> GetData()
        {
            return dB.Requests.Include(sp => sp.Post).ToList();
        }

        public void Update(Requests table, int id)
        {
            Requests = Find(id);
            dB.Requests.Update(Requests);
            dB.SaveChanges();

        }

        public Requests Find(int id)
        {
            return dB.Requests.Where(x => x.Id == id).First();
        }

        public List<Requests> GetDataByUserId(string UserId)
        {
            if (dB.Database.CanConnect())
            {
                return dB.Requests.Where(x => x.UserId == UserId).ToList();
            }
            else
            {
                return null;
            }
        }

        public int PostsCounts(string UserId)
        {
            return dB.Requests.Where(x => x.UserId == UserId).Count();

        }

     
        public void AddRequest(string userId, int postId, string posterId, bool isRequestsReceived, bool isRequestsSent)
        {
            try
            {
                var existingRequest = dB.Requests.FirstOrDefault(r => r.UserId == userId && r.Post.Id == postId && r.PosterId == posterId);

                if (existingRequest != null)
                {
                    // Update the existing request
                    existingRequest.IsRequestsReceived = isRequestsReceived;
                    existingRequest.IsRequestsSent = isRequestsSent;
                }
                else
                {
                    // Add a new request
                    dB.Requests.Add(new Requests
                    {
                        UserId = userId,
                        PostId = postId,
                        PosterId = posterId,
                        IsRequestsReceived = isRequestsReceived,
                        IsRequestsSent = isRequestsSent
                    });
                  
                }
                dB.SaveChanges();

            }
            catch (Exception ex)
            {
                // Handle any exceptions
                Console.WriteLine($"Exception in AddRequest: {ex.Message}");
                throw; // Optionally rethrow the exception for further handling
            }
        }

        public int ReceivedPostsCounts(string PosterId)
        {
                   return dB.Requests.Where(x => x.PosterId == PosterId).Count();

        }

      
        public void AddSenderRequestsInfo(string UserName,string Email,string Phonenumber)
        {
            dB.SenderRequestsInfos.Add(new SenderRequestsInfo
            {
                Username = UserName,
                Email = Email,
                Phonenumber = Phonenumber
            });
            dB.SaveChanges();
        }

        public void DeleteSenderRequestsInfo(string Userid)
        {
            var user = dB.Requests.Find(Userid);
            dB.Requests.Remove(user);
            dB.SaveChanges();
        }

        public List<Requests> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public List<Requests> SearchItem(string searchItem)
        {
            throw new NotImplementedException();
        }

  
    }
}
