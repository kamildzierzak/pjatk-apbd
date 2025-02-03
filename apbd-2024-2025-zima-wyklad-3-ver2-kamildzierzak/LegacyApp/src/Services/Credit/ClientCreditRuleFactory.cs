using LegacyApp.src.Models;

namespace LegacyApp.src.Services.Credit;
public class ClientCreditRuleFactory
{
    private readonly ICreditLimitService _creditLimitService;

    public ClientCreditRuleFactory(ICreditLimitService creditLimitService)
    {
        _creditLimitService = creditLimitService;
    }

    public IClientCreditRule GetRule(Client client)
    {
        return client.Type switch
        {
            "VeryImportantClient" => new NoCreditLimitRule(),
            "ImportantClient" => new DoubleLimitCreditRule(_creditLimitService),
            _ => new DefaultCreditLimitRule(_creditLimitService)
        };
    }
}