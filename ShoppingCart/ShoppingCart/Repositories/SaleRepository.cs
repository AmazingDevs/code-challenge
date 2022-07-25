using MongoDB.Driver;
using ShoppingCart.Data;
using ShoppingCart.Models;
using ShoppingCart.Repositories.IRepository;

namespace ShoppingCart.Repositories
{
	public class SaleRepository : ISaleRepository
	{
		private readonly IMongoDatabase _mongoDatabase;

		public SaleRepository(MongoDbSettings mongoDatabaseSettings)
		{
			_mongoDatabase = mongoDatabaseSettings.MongoDatabase;
		}

		public async Task CreateSaleAsync(Sale sale)
		{
			await _mongoDatabase.GetCollection<Sale>("Sales").InsertOneAsync(sale);
		}

		public async Task<Sale> GetSaleAsync(int saleNumber)
		{
			var saleList = await _mongoDatabase.GetCollection<Sale>("Sales").FindAsync(x => x.SaleNumber == saleNumber);

			return saleList.SingleOrDefault();
		}
	}
}