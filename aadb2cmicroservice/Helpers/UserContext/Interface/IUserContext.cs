namespace aadb2cmicroservice.Helpers.UserContext.Interface
{
	public interface IUserContext
	{
		string UserId { get; }
		string UserName { get; }
		string Email { get; }
	}
}