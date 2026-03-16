using RealEstate.Core.Entities.RealEstateEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Core.Entities.RequestsEntites
{
    public class Requests
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int PostId { get; set; }
        public string PosterId { get; set; }
        public virtual RealEstateProperty Post { get; set; }
        public bool IsRequestsReceived { get; set; }
        public bool IsRequestsSent { get; set; }
    }
}
