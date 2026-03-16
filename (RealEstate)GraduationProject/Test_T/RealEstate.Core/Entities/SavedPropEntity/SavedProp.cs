using Microsoft.AspNetCore.Identity;
using RealEstate.Core.Entities.RealEstateEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace RealEstate.Core.Entities.SavedPropEntity
{
    public class SavedProp
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public int PostId { get; set; }
        public virtual RealEstateProperty Post { get; set; }
        [NotMapped]
        public virtual List<RealEstateProperty> PostList { get; set; }

        public bool IsSaved { get; set; }


    }
}
