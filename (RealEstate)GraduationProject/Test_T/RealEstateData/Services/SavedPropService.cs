
using Microsoft.EntityFrameworkCore;
using RealEstate.Core.Entities.SavedPropEntity;
using RealEstate.Data.IRepos;
using RealEstate.Data.SqlDBContext;

namespace RealEstate.Data.Services
{
    public class SavedPropService : ISavedPropertiesRepo<SavedProp>
    { 
      private  DBData dB;

        public SavedPropService()
        {
            dB = new DBData();
        }

        public void SavePost(string userId, int postId)
        {
            // Check if post is already saved by the user
            if (!dB.savedProps.Any(sp => sp.UserId == userId && sp.PostId == postId))
            {
                dB.savedProps.Add(new SavedProp { UserId = userId,PostId = postId });
                dB.SaveChanges();
            }
        }

        public void UnsavePost(string userId, int postId)
        {
            var savedPost = dB.savedProps.FirstOrDefault(sp => sp.UserId == userId && sp.PostId == postId);
            if (savedPost != null)
            {
                dB.savedProps.Remove(savedPost);
                dB.SaveChanges();
            }
        }



        public bool CheckIfUserHasSavedProperty(string userId, int propertyId)
        {
            // Check if there's any record matching the user ID and property ID
            return dB.savedProps.Any(x => x.UserId == userId && x.PostId == propertyId&&x.IsSaved==true);
        }




        public SavedProp Find(int id)
        {
            return dB.savedProps.Where(x => x.Id == id).First();
        }

        public List<SavedProp> GetData()
        {
            return dB.savedProps.Include(sp => sp.Post).ToList();
        }


        public void Delete(int id)
        {
            var user = Find(id);

            if (dB.Database.CanConnect())
            {
                dB = new DBData();

                dB.savedProps.Remove(user);
                dB.SaveChanges();

            }
            else
            {
                // Throw an exception indicating database connection failure
                throw new Exception("Database connection failed while trying to add the entity.");
            }
        }

        public void TogglePostStatus(string userId, int postId, bool isSave)
        {
            var savedPost = dB.savedProps.FirstOrDefault(sp => sp.UserId == userId && sp.PostId == postId);
            if (savedPost != null)
            {
                savedPost.IsSaved = isSave;
                dB.SaveChanges();
            }
            else if (isSave)
            {
                dB.savedProps.Add(new SavedProp { UserId = userId, PostId = postId, IsSaved = true });
                dB.SaveChanges();
            }
        }

        public int SavedCount(string userid)
        {
            return dB.savedProps.Where(x=>x.UserId==userid&&x.IsSaved).Count();
        }

        public List<SavedProp> GetById(int id)
        {
            return dB.savedProps.Where(x=>x.Id==id).ToList();
        }

    }
}
