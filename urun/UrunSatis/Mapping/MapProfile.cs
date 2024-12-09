using AutoMapper;
using urun.Models;
using urun.ViewModel;

namespace urun.Mapping
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<User,RegisterModel>().ReverseMap();
            CreateMap<Category,CategoryModel>().ReverseMap();
            CreateMap<Contact, ContactModel>().ReverseMap();
            CreateMap<Product, ProductModel>().ReverseMap();
        }
    }
}
