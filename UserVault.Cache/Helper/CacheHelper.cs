using System.Configuration;
using Microsoft.Extensions.Configuration;
using StackExchange.Redis;

namespace UserVault.Application.Helper
{
    internal static class CacheConfiguration
    {
        internal static string RedisUrl { get; }
        static CacheConfiguration()
        {
            try
            {
                IConfiguration configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();

                RedisUrl = configuration.GetSection("AppSettings").Get<AppSettings>().RedisCacheUrl;
            }
            catch (FileNotFoundException e)
            {
                throw new ConfigurationErrorsException("The configuration file 'appsettings.json' was not found.", e);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }
    }
    internal class CacheConnection
    {
        private static readonly string redisUrl;
        private static readonly Lazy<ConnectionMultiplexer> lazyConnection;
        static CacheConnection()
        {
            try
            {
                redisUrl = CacheConfiguration.RedisUrl;
                if (string.IsNullOrEmpty(redisUrl) == false)
                {
                    lazyConnection = new Lazy<ConnectionMultiplexer>(() =>
                    {
                        return ConnectionMultiplexer.Connect(redisUrl);
                    });
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        internal static ConnectionMultiplexer Connection => lazyConnection?.Value;
        internal static bool RedisCacheEnbled => string.IsNullOrEmpty(redisUrl) == false;
    }
}
