namespace FastkartAPI.Infrastructure.Password
{
    public class JwtOption
    {
        public string SecretKey { get; set; } = string.Empty;

        public int ExpiresHours { get; set; }
    }
}
