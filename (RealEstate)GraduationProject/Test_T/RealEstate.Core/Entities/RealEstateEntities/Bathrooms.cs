using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Core.Entities.RealEstateEntities
{
    public class Bathrooms
    {
        public int id { get; set; }
        public string bathrooms { get; set; }
        public virtual List<RealEstateProperty> RealEstateProperties { get; set; }


    }
}
