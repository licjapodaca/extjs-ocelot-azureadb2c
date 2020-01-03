using System.Threading.Tasks;

namespace aadb2cmanagement.Helpers.APIGraphClient.Interface
{
	public interface IB2CGraphClient
	{
		Task<(object,int)> GetUserByObjectIdAsync(string objectId);
		Task<(object,int)> GetAllUsersAsync(string query);
		Task<(object,int)> CreateUserAsync(string json);
		Task<(object,int)> UpdateUserAsync(string objectId, string json);
		Task<(object,int)> DeleteUserAsync(string objectId);
	}
}