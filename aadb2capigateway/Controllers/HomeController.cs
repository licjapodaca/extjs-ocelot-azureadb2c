using Microsoft.AspNetCore.Mvc;

namespace aadb2capigateway.Controllers
{
	public class HomeController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}

}