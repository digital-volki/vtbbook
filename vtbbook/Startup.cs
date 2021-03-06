using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using vtbbook.Application.Coupons;
using vtbbook.Application.Domain;
using vtbbook.Application.Service;
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
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void CommonRegistration(IServiceCollection services, ISettings settings)
        {
            var authOptions = settings.GetSection<AuthOptions>($"{nameof(AuthOptions)}");
            services.AddControllers();
            services.AddHttpContextAccessor()
                .AddCors()
                .AddAuthentication(auth =>
                {
                    auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    auth.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
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
            services
                .AddScoped<IUserService, UserService>()
                .AddScoped<IStoreService, StoreService>()
                .AddScoped<IGameService, GameService>()
                .AddScoped<IUserDomain, UserDomain>()
                .AddScoped<IStoreDomain, StoreDomain>()
                .AddScoped<ICouponDomain, CouponDomain>()
                .AddScoped<ICouponFactory, CouponFactory>();
        }
    }
}
