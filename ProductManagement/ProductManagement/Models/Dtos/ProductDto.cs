using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductManagement.Models.Dtos
{
	public class ProductDto
	{
		public int Id { get; set; }

		[Required]
		public string Title { get; set; }

		[Required]
		public string Description { get; set; }

		[Required]
		[Column(TypeName = "decimal(10,2)")]
		public decimal Price { get; set; }

		[Required]
		[Column(TypeName = "decimal(5,2)")]
		public decimal DiscountRate { get; set; }

		[Column(TypeName = "decimal(4,2")]
		public decimal Rating { get; set; }

		[Required]
		public int Stock { get; set; }

		public string Brand { get; set; }
		public string Category { get; set; }
		public string Thumbnail { get; set; }
		public string[] Images { get; set; }
	}
}