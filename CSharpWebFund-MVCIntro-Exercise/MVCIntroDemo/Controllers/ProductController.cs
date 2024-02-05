using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using MVCIntroDemo.Models.Product;
using Newtonsoft.Json;
using System.Text;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

namespace MVCIntroDemo.Controllers
{
	public class ProductController : Controller
	{
		private IEnumerable<ProductViewModel> products
			= new List<ProductViewModel>()
			{
				new ProductViewModel()
				{
					Id = 3,
					Name = "Cheese",
					Price = 7.00
				},
				new ProductViewModel()
				{
					Id = 2,
					Name = "Ham",
					Price = 4.50
				},
				new ProductViewModel()
				{
					Id = 1,
					Name = "Bread",
					Price = 12.20
				},
			};

		public IActionResult All(string keyword = "")
		{
			if (String.IsNullOrEmpty(keyword))
			{
				return View(products);
			}

			IEnumerable<ProductViewModel> searchedProducts = products.Where(p => p.Name.ToLower().Contains(keyword.ToLower())).ToArray();

			return View(searchedProducts);
		}

		public IActionResult ById(string id)
		{
			ProductViewModel? product = products.FirstOrDefault(p => p.Id.ToString().Equals(id));

			if (product == null)
			{
				return this.RedirectToAction("All");
			}
			return this.View(product);
		}

		public IActionResult AllAsJson()
		{
			string jsonText = JsonConvert.SerializeObject(products, Formatting.Indented);

			return Json(jsonText);
		}

		public IActionResult DownloadProductsinfo()
		{
			StringBuilder text = new StringBuilder();

			foreach (ProductViewModel product in products)
			{
				text.AppendLine($"Product with Id: {product.Id}\nName: {product.Name}\nPrice: {product.Price:f2} \n");
			}

			Response.Headers.Add(HeaderNames.ContentDisposition, "filename=products.txt");

			return File(Encoding.UTF8.GetBytes(text.ToString()), "text/plain");
		}
	}
}
