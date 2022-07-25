namespace OnlineStore.Models
{
	public class Sale
	{
		public int SaleNumber { get; private set; }
		public decimal Total { get; set; }
		public ICollection<SaleItem> SaleItems { get; set; }
	}
}