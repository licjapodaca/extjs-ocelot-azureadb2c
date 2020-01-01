using aadb2cmicroservice.Helpers.UserContext.Interface;
using Microsoft.AspNetCore.Http;

namespace aadb2cmicroservice.Helpers.UserContext.Implementation
{
	public sealed class UserContextAdapter : IUserContext
	{
		private readonly IHttpContextAccessor _accessor;
		
		public UserContextAdapter(IHttpContextAccessor accessor)
		{
			this._accessor = accessor;
		}

		public string UserId
		{
			get
			{
				return this._accessor.HttpContext.Request.Headers["UserId"];
			}
		}

		public string UserName
		{
			get
			{
				return this._accessor.HttpContext.Request.Headers["UserName"];
			}
		}

		public string Email
		{
			get
			{
				return this._accessor.HttpContext.Request.Headers["Email"];
			}
		}
	}
}