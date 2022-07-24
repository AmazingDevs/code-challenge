using AutoMapper;
using ProductManagement.Models;
using ProductManagement.Models.Dtos;

namespace ProductManagement.Mapper
{
	public class ProductMappings : Profile
	{
		public ProductMappings()
		{
			CreateMap<Product, ProductDto>().ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.Images.Select(dest => dest.Url)));
			CreateMap<ProductDto, Product>().ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.Images.Select(dest => new Image() { Url = dest })));
		}
	}
}