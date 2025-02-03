using LegacyApp.src.Models;

namespace LegacyApp.src.Services.Credit;

public class DoubleLimitCreditRule : IClientCreditRule
{
    private readonly ICreditLimitService _creditLimitService;

    public DoubleLimitCreditRule(ICreditLimitService creditLimitService)
    {
        _creditLimitService = creditLimitService;
    }

    public void Apply(User user)
    {
        user.CreditLimit = _creditLimitService.GetCreditLimit(user.LastName, user.DateOfBirth) * 2;
    }
}