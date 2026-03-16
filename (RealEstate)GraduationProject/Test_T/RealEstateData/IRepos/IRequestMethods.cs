using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Data.IRepos
{
    public interface IRequestMethods<Table>
    {
        public void AddSenderRequestsInfo(string UserName, string Email, string Phonenumber);
        public void DeleteSenderRequestsInfo(string Userid);
        public void AddRequest(string userId, int postId, string posterId, bool isRequestsReceived, bool isRequestsSent);
        public int ReceivedPostsCounts(string PosterId);
        public int PostsCounts(string UserId);

    }
}
