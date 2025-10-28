namespace UserVault.Infrastructure.Helper
{
    public static class Helper
    {
        public static DateTimeOffset CreateCollectoCacheOptions(double expirationInMinutes = 360)
        {
            return DateTime.Now.AddMinutes(expirationInMinutes);
        }
    }
}
