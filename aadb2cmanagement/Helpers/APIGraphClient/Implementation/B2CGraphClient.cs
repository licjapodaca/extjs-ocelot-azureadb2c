#region Usings
using System;
using System.Threading.Tasks;
using aadb2cmanagement.Helpers.APIGraphClient.Interface;
using Microsoft.Extensions.Configuration;
using MICA = Microsoft.IdentityModel.Clients.ActiveDirectory;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using Newtonsoft.Json.Linq;
#endregion

namespace aadb2cmanagement.Helpers.APIGraphClient.Implementation
{
	public class B2CGraphClient : IB2CGraphClient
	{
		#region Private Variables
		private readonly IConfiguration _config;
		private MICA.AuthenticationContext _authContext;
		private MICA.ClientCredential _credential;
		private HttpClient _httpClient;
		private string _authority;
		private string _graphApiUrl;
		private string _tenant;
		private string _aadGraphVersion;
		#endregion

		#region Constructor
		public B2CGraphClient(IConfiguration config, IHttpClientFactory clientFactory)
		{
			_config = config;
			_httpClient = clientFactory.CreateClient();
			_authority = _config.GetValue<string>("AzureAdB2C:Authority");
			_graphApiUrl = _config.GetValue<string>("AzureAdB2C:GraphApiUrl");
			_tenant = _config.GetValue<string>("AzureAdB2C:Tenant");
			_aadGraphVersion = _config.GetValue<string>("AzureAdB2C:AADGraphVersion");

			_authContext = new MICA.AuthenticationContext(
				authority: $"{_authority}/{_tenant}"
			);

			_credential = new MICA.ClientCredential(
				clientId: _config.GetValue<string>("AzureAdB2C:ClientId"),
				clientSecret: _config.GetValue<string>("AzureAdB2C:ClientSecret")
			);
		}
		#endregion

		public async Task<(object,int)> GetUserByObjectIdAsync(string objectId)
		{
			try
			{
				return await SendGraphGetRequest(api: $"users/{objectId}", query: null);
			}
			catch (Exception)
			{
				throw;
			}
		}

		public async Task<(object,int)> GetAllUsersAsync(string query)
		{
			try
			{
				return await SendGraphGetRequest(api: "users", query: query);
			}
			catch (Exception)
			{
				throw;
			}
		}

		public async Task<(object,int)> CreateUserAsync(string json)
		{
			try
			{
				return await SendGraphPostRequest(api: "users", json: json);
			}
			catch (Exception)
			{
				throw;
			}
		}

		public async Task<(object,int)> UpdateUserAsync(string objectId, string json)
		{
			try
			{
				return await SendGraphPatchRequest(api: $"users/{objectId}", json: json);
			}
			catch (Exception)
			{
				throw;
			}
		}

		public async Task<(object,int)> DeleteUserAsync(string objectId)
		{
			try
			{
				return await SendGraphDeleteRequest(api: $"users/{objectId}");
			}
			catch (Exception)
			{
				throw;
			}
		}

		#region private methods

		private async Task<(object,int)> SendGraphGetRequest(string api, string query)
		{
			try
			{
				// First, use ADAL to acquire a token using the app's identity (the credential)
				// The first parameter is the resource we want an access_token for; in this case, the Graph API.
				MICA.AuthenticationResult result = await _authContext.AcquireTokenAsync(
					resource: _graphApiUrl,
					clientCredential: _credential
				);

				// For B2C user managment, be sure to use the 1.6 Graph API version.
				string url = $"{_graphApiUrl}/{_tenant}/{api}?{_aadGraphVersion}";

				if (!string.IsNullOrEmpty(query))
				{
					url += "&" + query;
				}

				// Append the access token for the Graph API to the Authorization header of the request, using the Bearer scheme.
				HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, url);
				request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", result.AccessToken);
				HttpResponseMessage response = await _httpClient.SendAsync(request);

				if (!response.IsSuccessStatusCode)
				{
					string error = await response.Content.ReadAsStringAsync();
					object formatted = JsonConvert.DeserializeObject(error);
					return (formatted, (int)response.StatusCode);
				}

				return (JsonConvert.DeserializeObject(await response.Content.ReadAsStringAsync()), (int)response.StatusCode);
			}
			catch (Exception)
			{
				throw;
			}
		}

		private async Task<(object,int)> SendGraphPostRequest(string api, string json)
		{
			try
			{
				// NOTE: This client uses ADAL v2, not ADAL v4
				MICA.AuthenticationResult result = await _authContext.AcquireTokenAsync(
					resource: _graphApiUrl,
					clientCredential: _credential
				);

				string url = $"{_graphApiUrl}/{_tenant}/{api}?{_aadGraphVersion}";

				HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, url);
				request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", result.AccessToken);
				request.Content = new StringContent(json, Encoding.UTF8, "application/json");
				HttpResponseMessage response = await _httpClient.SendAsync(request);

				if (!response.IsSuccessStatusCode)
				{
					string error = await response.Content.ReadAsStringAsync();
					object formatted = JsonConvert.DeserializeObject(error);
					return (formatted,(int)response.StatusCode);
				}

				return (JsonConvert.DeserializeObject(await response.Content.ReadAsStringAsync()), (int)response.StatusCode);
			}
			catch (Exception)
			{
				throw;
			}
		}

		private async Task<(object,int)> SendGraphPatchRequest(string api, string json)
		{
			try
			{
				// NOTE: This client uses ADAL v2, not ADAL v4
				MICA.AuthenticationResult result = await _authContext.AcquireTokenAsync(
					resource: _graphApiUrl,
					clientCredential: _credential
				);

				string url = $"{_graphApiUrl}/{_tenant}/{api}?{_aadGraphVersion}";

				HttpRequestMessage request = new HttpRequestMessage(new HttpMethod("PATCH"), url);
				request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", result.AccessToken);
				request.Content = new StringContent(json, Encoding.UTF8, "application/json");
				HttpResponseMessage response = await _httpClient.SendAsync(request);

				if (!response.IsSuccessStatusCode)
				{
					string error = await response.Content.ReadAsStringAsync();
					object formatted = JsonConvert.DeserializeObject(error);
					return (formatted, (int)response.StatusCode);
				}

				return (JsonConvert.DeserializeObject(await response.Content.ReadAsStringAsync()), (int)response.StatusCode);
			}
			catch (Exception)
			{
				throw;
			}
		}

		private async Task<(object,int)> SendGraphDeleteRequest(string api)
		{
			try
			{
				// NOTE: This client uses ADAL v2, not ADAL v4
				MICA.AuthenticationResult result = await _authContext.AcquireTokenAsync(
					resource: _graphApiUrl,
					clientCredential: _credential
				);

				string url = $"{_graphApiUrl}/{_tenant}/{api}?{_aadGraphVersion}";

				HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Delete, url);
				request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", result.AccessToken);
				HttpResponseMessage response = await _httpClient.SendAsync(request);

				if (!response.IsSuccessStatusCode)
				{
					string error = await response.Content.ReadAsStringAsync();
					object formatted = JsonConvert.DeserializeObject(error);
					return (formatted, (int)response.StatusCode);
				}

				return (JsonConvert.DeserializeObject(await response.Content.ReadAsStringAsync()), (int)response.StatusCode);
			}
			catch (Exception)
			{
				throw;
			}
		}

		#endregion
	}
}