using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Provider.Polly;

namespace aadb2capigateway
{
	public class Startup
	{
		public IConfiguration _config { get; }
		public static string ScopeRead;
		public static string ScopeWrite;

		public Startup(IHostEnvironment env)
		{
			var builder = new ConfigurationBuilder()
				.SetBasePath(env.ContentRootPath)
				.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
				.AddJsonFile($"appsettings.{env.EnvironmentName.ToLower()}.json", optional: false, reloadOnChange: true)
				.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true)
				.AddEnvironmentVariables();

			if (env.IsDevelopment())
			{
				builder.AddUserSecrets<Startup>();
			}

			_config = builder.Build();
		}

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
			{
				builder.AllowAnyOrigin()
					.AllowAnyMethod()
					.AllowAnyHeader()
					.WithExposedHeaders("Token-Expired");
			}));

			services
				.AddOcelot(_config)
				.AddPolly();

			//JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

			services.AddAuthentication(_config["AzureAdB2C:ProviderKey"].ToString())//JwtBearerDefaults.AuthenticationScheme)
				.AddJwtBearer(_config["AzureAdB2C:ProviderKey"].ToString(), jwtOptions =>
				{
					//jwtOptions.Authority = $"https://{_config["AzureAdB2C:Tenant"]}.b2clogin.com/{_config["AzureAdB2C:TenantDomain"]}/v2.0/";
					jwtOptions.MetadataAddress = $"https://{_config["AzureAdB2C:Tenant"]}.b2clogin.com/{_config["AzureAdB2C:TenantDomain"]}/v2.0/.well-known/openid-configuration?p={_config["AzureAdB2C:Policy"]}";
					jwtOptions.Audience = _config["AzureAdB2C:ClientId"];
					jwtOptions.TokenValidationParameters = new TokenValidationParameters
					{
						NameClaimType = ClaimTypes.NameIdentifier
					};
					jwtOptions.Events = new JwtBearerEvents
					{
						OnAuthenticationFailed = context =>
						{
							if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
							{
								context.Response.Headers.Add("Token-Expired", "true");
							}
							return Task.CompletedTask;
						}
					};
				});

			services.AddControllers();

			//IdentityModelEventSource.ShowPII = true;
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			ScopeRead = _config["AzureAdB2C:ScopeRead"];
			ScopeWrite = _config["AzureAdB2C:ScopeWrite"];

			app.UseCors("MyPolicy");
			app.UseAuthentication();

			app.UseHttpsRedirection();

			app.UseDefaultFiles(
				new DefaultFilesOptions
				{
					DefaultFileNames = new List<string> { "index.html" }
				}
			);

			app.UseStaticFiles();
			app.UseRouting();
			app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
			app.UseOcelot().Wait();
		}
	}
}
