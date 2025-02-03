using LegacyApp.src.Models;

namespace LegacyApp.src.Services.Credit;
public class NoCreditLimitRule : IClientCreditRule
{
    public void Apply(User user)
    {
        user.HasCreditLimit = false;
    }
}