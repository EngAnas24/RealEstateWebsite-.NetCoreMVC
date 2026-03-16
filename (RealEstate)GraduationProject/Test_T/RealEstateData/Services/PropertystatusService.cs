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
    public class PropertystatusService: IBaseHelper<propertystatus>
    {
        private DBData dB;

        public PropertystatusService()
        {
            dB = new DBData();
        }

       
        public propertystatus Find(int id)
        {
            return dB.propertystatus.Where(x => x.id == id).First();
        }

        public List<propertystatus> GetData()
        {
            return dB.propertystatus.ToList();
        }

      
    }
}
