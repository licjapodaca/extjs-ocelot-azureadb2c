using System.Threading.Tasks;
using aadb2cmicroservice.Helpers.UserContext.Interface;
using Microsoft.AspNetCore.Mvc;

namespace aadb2cmicroservice.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ValuesController : ControllerBase
	{
		private readonly IUserContext _user;
		
		public ValuesController(IUserContext user)
		{
			_user = user;
		}

		// GET api/values
		[HttpGet]
		public async Task<IActionResult> Get()
		{
			// var claimsIdentity = User.Identity as ClaimsIdentity;
			// var name = claimsIdentity.FindFirst("Name").ToString();
			return await Task.Run<IActionResult>(() =>
			{
				return Ok(new {
					ID = 1,
					UserId = _user.UserId,
					UserName = _user.UserName,
					Email = _user.Email,
					Tecnologia = ".Net Core 2.2.207",
					DatosExtra = new {
						WebServer = "Kestrel",
						SistemaOperativo = "Windows"
					}
				});
			});
		}

		// GET api/values/5
		[HttpGet("{id}")]
		public ActionResult<string> Get(int id)
		{
			return "value";
		}

		// POST api/values
		[HttpPost]
		public void Post([FromBody] string value)
		{
		}

		// PUT api/values/5
		[HttpPut("{id}")]
		public void Put(int id, [FromBody] string value)
		{
		}

		// DELETE api/values/5
		[HttpDelete("{id}")]
		public void Delete(int id)
		{
		}
	}
}
