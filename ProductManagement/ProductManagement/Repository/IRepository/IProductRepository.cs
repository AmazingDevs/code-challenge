using ProductManagement.Models;

namespace ProductManagement.Repository.IRepository
{
	public interface IProductRepository
	{
		ICollection<Product> GetAllProducts();

		Product GetProduct(int productId);

		bool ProductExists(int productId);

		bool ProductExists(string title);

		bool CreateProduct(Product product);

		bool UpdateProduct(params Product[] product);

		public IEnumerable<Product> GetProductsById(int[] productIds);

		bool Save();
	}
}