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
        public class FurnishedstatusService:IBaseHelper<furnishedstatus>
        {
        private DBData dB;

        public FurnishedstatusService()
        {
            dB = new DBData();
        }

       
        public furnishedstatus Find(int id)
        {
            return dB.furnishedstatus.Where(x => x.id == id).First();
        }

        public List<furnishedstatus> GetData()
        {
            return dB.furnishedstatus.ToList();
        }

       
    }
 }
