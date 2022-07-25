namespace ShoppingCart.Models
{
	public class SaleItem
	{
		public int ItemNumber { get; set; }
		public int ProductId { get; set; }
		public int Quantity { get; set; }
		public decimal UnitPrice { get; set; }
		public decimal DiscountPercentage { get; set; }
	}
}