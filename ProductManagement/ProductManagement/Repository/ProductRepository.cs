using Microsoft.EntityFrameworkCore;
using ProductManagement.Data;
using ProductManagement.Models;
using ProductManagement.Repository.IRepository;

namespace ProductManagement.Repository
{
	public class ProductRepository : IProductRepository
	{
		private readonly ApplicationDbContext context;

		public ProductRepository(ApplicationDbContext applicationDbContext)
		{
			this.context = applicationDbContext;
		}

		public bool CreateProduct(Product product)
		{
			context.Products.Add(product);
			return Save();
		}

		public Product GetProduct(int productId)
		{
			return context.Products
				.Include(p => p.Images)
				.FirstOrDefault(p => p.Id == productId);
		}

		public ICollection<Product> GetAllProducts()
		{
			return context.Products
				.Include(p => p.Images)
				.AsNoTracking()
				.OrderBy(p => p.Description).ToList();
		}

		public bool ProductExists(int productId)
		{
			return context.Products.Any(p => p.Id == productId);
		}

		public bool ProductExists(string title)
		{
			return context.Products.Any(p => p.Title == title);
		}

		public bool UpdateProduct(params Product[] product)
		{
			context.Products.UpdateRange(product);
			return Save();
		}

		public IEnumerable<Product> GetProductsById(int[] ids)
		{
			return context.Products.Where(x => ids.Contains(x.Id));
		}

		public bool Save()
		{
			return context.SaveChanges() >= 0 ? true : false;
		}
	}
}