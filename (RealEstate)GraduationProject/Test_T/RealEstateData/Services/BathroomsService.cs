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
    public class BathroomsService : IBaseHelper<Bathrooms>
    {
        private DBData dB;

        public BathroomsService()
        {
            dB = new DBData();
        }

        public Bathrooms Find(int id)
        {
            return dB.Bathrooms.Where(x => x.id == id).First();
        }

        public List<Bathrooms> GetData()
        {
            return dB.Bathrooms.ToList();
        }

    
    }
}
