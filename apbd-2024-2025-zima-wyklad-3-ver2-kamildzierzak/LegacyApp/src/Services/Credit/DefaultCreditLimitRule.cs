using LegacyApp.src.Models;

namespace LegacyApp.src.Services.Credit;
public class DefaultCreditLimitRule : IClientCreditRule
{
    private readonly ICreditLimitService _creditLimitService;

    public DefaultCreditLimitRule(ICreditLimitService creditLimitService)
    {
        _creditLimitService = creditLimitService;
    }

    public void Apply(User user)
    {
        user.HasCreditLimit = true;
        user.CreditLimit = _creditLimitService.GetCreditLimit(user.LastName, user.DateOfBirth);
    }
}