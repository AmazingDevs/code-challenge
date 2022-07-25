using ShoppingCart.Models;

namespace ShoppingCart.Repositories.IRepository
{
	public interface ISaleRepository
	{
		public Task<Sale> GetSaleAsync(int saleNumber);

		public Task CreateSaleAsync(Sale sale);
	}
}