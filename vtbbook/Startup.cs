using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using vtbbook.Core.Common;
using vtbbook.Core.DataAccess;

namespace vtbbook
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var settings = new Settings();
            services.AddSingleton(typeof(ISettings), settings);
            
            CommonRegistration(services, settings);
            DbRegistration(services, settings);
            ServicesRegistration(services);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors(builder => builder
                 .AllowAnyOrigin()
                 .AllowAnyHeader()
                 .AllowAnyMethod());

            app.UseWebSockets();
            app.UseRouting();
            app.UseAuthorization();
            app.UseAuthentication();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
        }

        private void CommonRegistration(IServiceCollection services, ISettings settings)
        {
            var authOptions = settings.GetSection<AuthOptions>($"{typeof(AuthOptions)}");
            services.AddHttpContextAccessor()
                .AddCors()
                .AddAuthorization()
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                }).AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = authOptions.Issuer,
                        ValidateAudience = true,
                        ValidAudience = authOptions.Audience,
                        ValidateLifetime = true,
                        IssuerSigningKey = authOptions.GetSymmetricSecurityKey(),
                        ValidateIssuerSigningKey = true
                    };
                });
        }

        private void DbRegistration(IServiceCollection services, ISettings settings)
        {
            var connectionString = settings.GetValue("ConnectionStrings:DefaultConnection");

            services
                .AddDbContext<IDataContext, DataContext>(options =>
                    options.UseNpgsql(connectionString), ServiceLifetime.Scoped);
        }

        private void ServicesRegistration(IServiceCollection services)
        {

        }
    }
}
