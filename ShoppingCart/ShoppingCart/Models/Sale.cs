using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ShoppingCart.Models
{
	[BsonIgnoreExtraElements]
	public class Sale
	{
		[JsonIgnore]
		public ObjectId Id { get; set; }

		public int SaleNumber { get; private set; }
		public decimal Total { get; set; }

		[JsonIgnore]
		public SaleStatus Status { get; set; } = SaleStatus.New;

		public ICollection<SaleItem> SaleItems { get; set; }

		public decimal GetTotal()
		{
			var total = 0M;

			foreach (var saleItem in this.SaleItems)
			{
				total += (saleItem.UnitPrice * saleItem.Quantity) * (saleItem.DiscountPercentage * 100);
			}

			return total;
		}
	}
}