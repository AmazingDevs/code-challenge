using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using OnlineStore.Models;

namespace OnlineStore.Pages
{
	public class IndexModel : PageModel
	{
		private readonly ILogger<IndexModel> _logger;
		public const int PAGE_SIZE = 12;
		public IList<Product> Products { get; set; }

		public int CurrentPage { get; set; }
		public int PageQuantity { get; set; }

		public IndexModel(ILogger<IndexModel> logger)
		{
			_logger = logger;
			Products = new List<Product>();
		}

		public async Task OnGetAsync([FromQuery(Name = "p")] int? page = 1)
		{
			this.CurrentPage = page.Value;
			var client = new HttpClient();
			var result = await client.GetAsync("https://localhost:7022/api/Product");
			var content = result.Content.ReadAsStringAsync();

			Products = JsonConvert.DeserializeObject<List<Product>>(content.Result);

			var productCount = Products.Count();

			this.PageQuantity = Convert.ToInt32(Math.Ceiling(productCount * 1M / PAGE_SIZE));

			Products = Products.Skip(PAGE_SIZE * (CurrentPage - 1)).Take(PAGE_SIZE).ToList();
		}
	}
}