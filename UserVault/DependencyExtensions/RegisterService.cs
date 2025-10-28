using UserVault.Application.Interfaces;
using UserVault.Application.Services;
using UserVault.Cache.Redis;
using UserVault.Domain.Interfaces;
using UserVault.Infrastructure.Repositories;
namespace UserVault.DependencyExtensions
{
    public static class RegisterService
    {
        public static void AddServices(this IServiceCollection services, AppSettings appSettings)
        {
            #region A
            services.AddScoped<IUserRepo, UserRepo>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICacheService, CacheService>();
            #endregion A

            #region Caching

            services.AddMemoryCache();
            if (string.IsNullOrEmpty(appSettings.RedisSessionUrl) == false)
            {
                services.AddStackExchangeRedisCache(options =>
                {
                    options.Configuration = appSettings.RedisSessionUrl;
                });
            }

            #endregion

            #region Session

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromHours(12);
                options.Cookie.Name = "user.Session.Id";
            });

            #endregion
        }
    }
}
