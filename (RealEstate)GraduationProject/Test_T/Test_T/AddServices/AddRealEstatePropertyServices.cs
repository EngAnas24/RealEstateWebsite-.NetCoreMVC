using RealEstate.Core;
using RealEstate.Data;
using RealEstate.Data.Contacts;
using RealEstate.Data.IRepos;
using RealEstate.Data.Services;
using RealEstate.Core.Entities.RealEstateEntities;
using RealEstate.Core.Entities.ContactEntity;
using RealEstate.Core.Entities.SavedPropEntity;
using RealEstate.Core.Entities.RequestsEntites;

namespace RealEstateWibsite.AddServices
{
    public static class AddRealEstatePropertyServices
    {
        public static IServiceCollection AddRealEstateServices(this IServiceCollection services)
        {
           services.AddSingleton<IRealEstateRepo<RealEstateProperty>, RealEstatePropertyService>();
           services.AddSingleton<IBaseHelper<offertype>, OfferTypeService>();
           services.AddSingleton<IBaseHelper<propertytype>, PropertytypServicee>();
           services.AddSingleton<IBaseHelper<propertystatus>, PropertystatusService>();
           services.AddSingleton<IBaseHelper<furnishedstatus>, FurnishedstatusService>();
           services.AddSingleton<IBaseHelper<Bedrooms>, BedroomsService>();
           services.AddSingleton<IBaseHelper<Bathrooms>, BathroomsService>();
           services.AddSingleton<IBaseHelper<Balconys>, BalconysService>();
           services.AddSingleton<IRealEstateRepo<Contact>, ContactService>();
           services.AddSingleton<ISavedPropertiesRepo<SavedProp>, SavedPropService>();
           services.AddSingleton<IRequestMethods<Requests>, RequestService>();

           services.AddSingleton<SavedProp>();
           services.AddSingleton<RealEstateProperty>();
           services.AddSingleton<SavedPropService>();
           services.AddSingleton<Requests>();

            return services;
        }
    }
}
