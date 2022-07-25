namespace OnlineStore.Models
{
	public class Product
	{
		public int ProductId { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public int Stock { get; set; }
		public decimal Price { get; set; }
		public decimal discountPercentage { get; set; }
		public string Thumbnail { get; set; }
	}
}