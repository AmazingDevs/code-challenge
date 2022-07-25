namespace ShoppingCart.Models
{
	public class ProductStock
	{
		public int ProductId { get; private set; }
		public int Quantity { get; private set; }

		public ProductStock(int productId, int quantity)
		{
			this.ProductId = productId;
			this.Quantity = quantity;
		}
	}
}