using AutoMapper;
using Web_API.Dtos.Genre;
using Web_API.Dtos.Platform;
using Web_API.Dtos.Product;
using Web_API.Dtos.Screenshot;
using Web_API.Dtos.Store;
using Web_API.Dtos.Trailer;
using Web_API.Models;

namespace Web_API.MappingProfiles
{
    public class StoreSystemProfile : Profile
    {
        public StoreSystemProfile() 
        { 
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<Product, ProductDto>()
                .ForMember(dest => dest.Background_Image, opt => opt.MapFrom(src => src.BackgroundImage))
                .ForMember(dest => dest.Rating_Top, opt => opt.MapFrom(src => src.RatingTop))
                .ReverseMap();
            CreateMap<CreateProductRequestDto, Product>()
                .ForMember(dest => dest.BackgroundImage, opt => opt.MapFrom(src => src.Background_Image))
                .ForMember(dest => dest.RatingTop, opt => opt.MapFrom(src => src.Rating_Top))
                .ReverseMap();
            CreateMap<PlatformDto, Platform>().ReverseMap();
            CreateMap<GenreDto, Genre>().ReverseMap();
            CreateMap<CreateGenreDto, Genre>().ReverseMap();
            CreateMap<StoreDto, Store>().ReverseMap();
            CreateMap<ScreenshotDto, Screenshot>().ReverseMap();
            CreateMap<CreateScreenshotDto, Screenshot>().ReverseMap();
            CreateMap<TrailerDto, Trailer>().ReverseMap();
        }
    }
}
