using Microsoft.AspNetCore.Mvc;
using ProductManagement.Models;
using ProductManagement.Repository.IRepository;

namespace ProductManagement.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class StockController : Controller
	{
		private readonly IProductRepository _productRepository;

		public StockController(IProductRepository productRepository)
		{
			_productRepository = productRepository;
		}

		[HttpPatch]
		public IActionResult MovementStock([FromBody] ProductStockData[] productStockData)
		{
			var products = _productRepository.GetProductsById(productStockData.Select(x => x.ProductId).ToArray());

			try
			{
				foreach (var product in products)
				{
					var productStockItem = productStockData.Single(x => x.ProductId == product.Id);
					product.DecrementStock(productStockItem.Quantity);
				}

				_productRepository.UpdateProduct(products.ToArray());

				return Ok();
			}
			catch
			{
				return Conflict();
			}
		}
	}
}