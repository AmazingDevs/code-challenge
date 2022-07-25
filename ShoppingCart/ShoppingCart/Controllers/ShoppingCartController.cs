using Microsoft.AspNetCore.Mvc;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Driver;
using ShoppingCart.Models;
using ShoppingCart.Repositories.IRepository;
using System.Net;
using ShoppingCart.Services.IService;

namespace ShoppingCart.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ShoppingCartController : ControllerBase
	{
		private readonly ISaleRepository _saleRepository;

		public ShoppingCartController(ISaleRepository saleRepository)
		{
			_saleRepository = saleRepository;
		}

		[HttpGet("{saleNumber:int}")]
		public async Task<IActionResult> GetAsync([FromRoute] int saleNumber)
		{
			var sale = await _saleRepository.GetSaleAsync(saleNumber);

			if (sale == null) return NotFound();

			return Ok(sale);
		}

		/// <summary>
		/// Creates the current sale
		/// </summary>
		/// <param name="sale"></param>
		/// <returns></returns>
		[HttpPost]
		[Produces("application/json")]
		[Consumes("application/json")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<IActionResult> CreateSale([FromBody] Sale sale, [FromServices] IStockService stockService)
		{
			const decimal MAX_SALE_VALUE = 5000M;

			if (sale.GetTotal() > MAX_SALE_VALUE)
			{
				ModelState.AddModelError("", $"Value is not allowed for this sale");
				return StatusCode(422, ModelState);
			}

			try
			{
				if (await stockService.ResquestMovement(sale))
				{
					sale.Status = SaleStatus.WaitingShipping;

					await _saleRepository.CreateSaleAsync(sale);
					return StatusCode(201);
				}

				return Conflict();
			}
			catch (Exception ex)
			{
				ModelState.AddModelError("", $"Something went wrong when creating the sale.");
				return StatusCode(500, ModelState);
			}
		}
	}
}