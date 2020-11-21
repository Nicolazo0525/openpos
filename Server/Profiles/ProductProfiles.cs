using AutoMapper;
using openpos.Server.ModelData;
using Category = openpos.Shared.Category;
using Product = openpos.Shared.Product;


namespace openpos.Server.Profiles
{
    public class ProductProfiles: Profile
    {
        
        public ProductProfiles()
        {
            CreateMap<ModelData.Category,  Category>();
            CreateMap<ModelData.Product, Product>()
                .ForMember(dest => dest.Categories, opt => opt.MapFrom(src => src.CategoriesList));
        }
        
    }
}