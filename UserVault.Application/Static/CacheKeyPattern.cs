namespace UserVault.Application.Static
{
    public class CacheKeyPattern
    {
        public static string All => "users:all";
        public static string ById(string userId) => $"users:id:{userId}";
        public static string ByEmail(string email) => $"users:email:{email}";
        public static string Count => "users:count";
        public static string Clear => "users";
    }
}
