using MongoDB.Driver;

namespace ShoppingCart.Data
{
	public class MongoDbSettings
	{
		public MongoDbSettings(IConfiguration configuration)
		{
			var mongoClient = new MongoClient(configuration.GetConnectionString("DefaultConnection"));
			MongoDatabase = mongoClient.GetDatabase("ShoppingCart");
		}

		public IMongoDatabase MongoDatabase { get; set; }
	}
}