using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProductManagement.Models;
using ProductManagement.Models.Dtos;
using ProductManagement.Repository.IRepository;

namespace ProductManagement.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductController : Controller
	{
		private readonly IMapper _mapper;
		private readonly IProductRepository _productRepository;

		public ProductController(IMapper mapper, IProductRepository productRepository)
		{
			_mapper = mapper;
			_productRepository = productRepository;
		}

		[HttpGet]
		public IActionResult GetAllProducts()
		{
			var allProducts = _productRepository.GetAllProducts();
			var allProductsDto = new List<ProductDto>();

			foreach (var product in allProducts)
			{
				allProductsDto.Add(_mapper.Map<ProductDto>(product));
			}

			return Ok(allProductsDto);
		}

		[HttpGet("{productId:int}", Name = "GetProduct")]
		public IActionResult GetProduct(int productId)
		{
			var product = _productRepository.GetProduct(productId);

			if (product == null) return NotFound();

			var productDto = _mapper.Map<ProductDto>(product);

			return Ok(productDto);
		}

		[HttpPost]
		public IActionResult CreateProducts([FromBody] ProductDto[] productDtos)
		{
			if (productDtos.Length == 0 || productDtos == null) return BadRequest(ModelState);

			foreach (var productDto in productDtos)
			{
				if (_productRepository.ProductExists(productDto.Title))
				{
					ModelState.AddModelError("", $"Poduct {productDto.Title} already exists.");
					return StatusCode(400, ModelState);
				}

				if (!CreateProduct(productDto))
				{
					ModelState.AddModelError("", $"Something went wrong when saving the record {productDto.Title}.");
					return StatusCode(500, ModelState);
				}
			}

			return StatusCode(201, ModelState);
		}

		[HttpPatch]
		public IActionResult UpdateProduct([FromBody] ProductDto productDto)
		{
			if (productDto == null) return BadRequest(ModelState);

			if (!_productRepository.ProductExists(productDto.Title))
			{
				ModelState.AddModelError("", $"Couldn't find the product: {productDto.Title}.");
				return StatusCode(400, ModelState);
			}

			var product = _mapper.Map<Product>(productDto);

			if (!_productRepository.UpdateProduct(product))
			{
				ModelState.AddModelError("", $"Something went wrong when updating the record {productDto.Title}.");
				return StatusCode(500, ModelState);
			}

			return StatusCode(204, ModelState);
		}

		[ApiExplorerSettings(IgnoreApi = true)]
		public bool CreateProduct(ProductDto productDto)
		{
			var product = _mapper.Map<Product>(productDto);

			return _productRepository.CreateProduct(product);
		}
	}
}