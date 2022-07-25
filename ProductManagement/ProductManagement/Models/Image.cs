using System.ComponentModel.DataAnnotations;

namespace ProductManagement.Models
{
	public class Image
	{
		[Key]
		public int Id { get; set; }

		[Required]
		public string Url { get; set; }

		[Required]
		public Product Product { get; set; }
	}
}