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
    public class PropertytypServicee: IBaseHelper<propertytype>
    {
        private DBData dB;

        public PropertytypServicee()
        {
            dB = new DBData();
        }

      
        public propertytype Find(int id)
        {
            return dB.propertytype.Where(x => x.id == id).First();
        }

        public List<propertytype> GetData()
        {
            return dB.propertytype.ToList();
        }
    }
}
