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
    public class BedroomsService : IBaseHelper<Bedrooms>
    {
        private DBData dB;

        public BedroomsService()
        {
            dB = new DBData();
        }

      
        public Bedrooms Find(int id)
        {
            return dB.Bedrooms.Where(x => x.id == id).First();
        }

        public List<Bedrooms> GetData()
        {
            return dB.Bedrooms.ToList();
        }

    }
}
