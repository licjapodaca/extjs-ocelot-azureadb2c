using System;
using System.Threading.Tasks;
using aadb2cmanagement.Helpers.APIGraphClient.Interface;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace aadb2cmanagement.Controllers
{
	[Route("api/account")]
	[ApiController]
	public class AccountController : ControllerBase
	{
		private IB2CGraphClient _graphClient;

		public AccountController(IB2CGraphClient graphClient)
		{
			_graphClient = graphClient;
		}

		[HttpGet("getallusers")]
		public async Task<IActionResult> GetAllUsers()
		{
			try
			{
				var query = String.IsNullOrEmpty(Request.QueryString.Value) ? null : Request.QueryString.Value.Split('?')[1];
				var result = await _graphClient.GetAllUsersAsync(query);

				return new ContentResult()
				{
					Content = JsonConvert.SerializeObject(result.Item1),
					StatusCode = result.Item2,
					ContentType = "application/json"
				};
			}
			catch (Exception)
			{
				throw;
			}
		}

		[HttpGet("getuserbyid/{id}")]
		public async Task<IActionResult> GetUserById(string id)
		{
			try
			{
				var result = await _graphClient.GetUserByObjectIdAsync(id);
				return new ContentResult()
				{
					Content = JsonConvert.SerializeObject(result.Item1),
					StatusCode = result.Item2,
					ContentType = "application/json"
				};
			}
			catch (Exception)
			{
				throw;
			}
		}

		[HttpPost("createuser")]
		public async Task<IActionResult> CreateUser([FromBody] dynamic content)
		{
			try
			{
				string text = JsonConvert.SerializeObject(content);
				var result = await _graphClient.CreateUserAsync(text);
				return new ContentResult()
				{
					Content = JsonConvert.SerializeObject(result.Item1),
					StatusCode = result.Item2,
					ContentType = "application/json"
				};
			}
			catch (Exception)
			{
				throw;
			}
		}

		[HttpDelete("deleteuser/{id}")]
		public async Task<IActionResult> Delete(string id)
		{
			try
			{
				var result = await _graphClient.DeleteUserAsync(id);
				return new ContentResult()
				{
					Content = JsonConvert.SerializeObject(result.Item1),
					StatusCode = result.Item2,
					ContentType = "application/json"
				};
			}
			catch (Exception)
			{
				throw;
			}
		}

		[HttpPatch("updateuser/{id}")]
		public async Task<IActionResult> UpdateUser(string id, [FromBody] dynamic content)
		{
			try
			{
				string text = JsonConvert.SerializeObject(content);
				var result = await _graphClient.UpdateUserAsync(id, text);
				return new ContentResult()
				{
					Content = JsonConvert.SerializeObject(result.Item1),
					StatusCode = result.Item2,
					ContentType = "application/json"
				};
			}
			catch (Exception)
			{
				throw;
			}
		}
	}
}