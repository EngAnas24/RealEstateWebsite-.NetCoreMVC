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
    public class BalconysService: IBaseHelper<Balconys>
    { 
        private DBData dB;

        public BalconysService()
        {
            dB = new DBData();
        }

     
        public Balconys Find(int id)
        {
            return dB.Balconys.Where(x => x.id == id).First();
        }

        public List<Balconys> GetData()
        {
            return dB.Balconys.ToList();
        }

    }
}
