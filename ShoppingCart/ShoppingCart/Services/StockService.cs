using Newtonsoft.Json;
using ShoppingCart.Models;
using ShoppingCart.Services.IService;
using System.Text;

namespace ShoppingCart.Services
{
	public class StockService : IStockService
	{
		public async Task<bool> ResquestMovement(Sale sale)
		{
			var stockData = sale.SaleItems.Select(x => new ProductStock(x.ProductId, x.Quantity));

			var client = new HttpClient();
			var myContent = JsonConvert.SerializeObject(stockData);
			var httpContent = new StringContent(myContent, Encoding.UTF8, "application/json");

			var result = await client.PatchAsync("https://localhost:7022/api/Stock", httpContent);

			return result.IsSuccessStatusCode;
		}
	}
}