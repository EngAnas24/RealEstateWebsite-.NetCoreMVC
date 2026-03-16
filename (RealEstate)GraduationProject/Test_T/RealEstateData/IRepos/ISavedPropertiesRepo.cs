using RealEstate.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Data.IRepos
{
    public interface ISavedPropertiesRepo<T>
    {
        public List<T> GetById(int id);
        public int SavedCount(string userid);
        public void TogglePostStatus(string userId, int postId, bool isSave);
        public void Delete(int id);
        public List<T> GetData();
        public T Find(int id);
        public bool CheckIfUserHasSavedProperty(string userId, int propertyId);
        public void UnsavePost(string userId, int postId);
        public void SavePost(string userId, int postId);
    }
}
