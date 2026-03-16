using Microsoft.EntityFrameworkCore.Metadata.Internal;
using RealEstate.Core;
using RealEstate.Core.Entities.RealEstateEntities;
using RealEstate.Data.IRepos;
using RealEstate.Data.SqlDBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Data.Services
{
    public class OfferTypeService : IBaseHelper<offertype>
    {
        private DBData dB;

        public OfferTypeService()
        {
            dB = new DBData();
        }
        public offertype Find(int id)
        {
            return dB.offertype.Where(x => x.id == id).First();
        }

        public List<offertype> GetData()
        {
           return dB.offertype.ToList();
        }

 
    }
}
