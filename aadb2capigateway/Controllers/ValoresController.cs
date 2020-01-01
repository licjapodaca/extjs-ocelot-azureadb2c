using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace aadb2capigateway.Controllers
{
	[Route("api/valores")]
	[ApiController]
	[Authorize]
	public class ValoresController : ControllerBase
	{
		// GET api/values
		[HttpGet]
		public IActionResult Get()
		{
			var scopes = HttpContext.User.FindFirst("http://schemas.microsoft.com/identity/claims/scope")?.Value;
			if (!string.IsNullOrEmpty(Startup.ScopeRead) && scopes != null
					&& scopes.Split(' ').Any(s => s.Equals(Startup.ScopeRead)))
				return Ok(new { Usuario = User.Identity });
			else
				return Unauthorized();
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
