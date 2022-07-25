using ShoppingCart.Models;

namespace ShoppingCart.Services.IService
{
	public interface IStockService
	{
		public Task<bool> ResquestMovement(Sale sale);
	}
}