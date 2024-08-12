
namespace iVertion.Domain.Account
{
    public interface IGoogleAuthService<TGoogleUserInfo> where TGoogleUserInfo : class
    {
    Task<string> GetGoogleAuthTokenAsync(string code);
    Task<TGoogleUserInfo> GetGoogleUserInfoAsync(string token);
    }
}