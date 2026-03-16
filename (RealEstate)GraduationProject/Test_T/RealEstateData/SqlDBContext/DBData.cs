using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using RealEstate.Core.Entities.RealEstateEntities;
using RealEstate.Core.Entities.ContactEntity;
using RealEstate.Core.Entities.SavedPropEntity;
using RealEstate.Core.Entities.RequestsEntites;
namespace RealEstate.Data.SqlDBContext
{
    public class DBData:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-SPO32LR\\SQLEXPRESS;Database=RealEstateDb;Trusted_Connection=True;TrustServerCertificate=True;");
        }

        //Server=DESKTOP-SPO32LR\\SQLEXPRESS; Database=RealEstateDb;Trusted_Connection=True;TrustServerCertificate=True

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<offertype> offertype { get; set; }
        public DbSet<propertytype> propertytype { get; set; }
        public DbSet<propertystatus> propertystatus { get; set; }
        public DbSet<furnishedstatus> furnishedstatus { get; set; }
        public DbSet<Bedrooms> Bedrooms { get; set; }
        public DbSet<Bathrooms> Bathrooms { get; set; }
        public DbSet<Balconys> Balconys { get; set; }
        public DbSet<RealEstateProperty> RealEstateProperty { get; set; }
        public DbSet<Contact> Contact { get; set; }
        public DbSet<SavedProp> savedProps { get; set; }
        public DbSet<Requests> Requests { get; set; }
        public DbSet<SenderRequestsInfo> SenderRequestsInfos { get; set; }






    }
}
