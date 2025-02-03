using LegacyApp.src.Models;

namespace LegacyApp.src.Services.Credit;
public interface IClientCreditRule
{
    void Apply(User user);
}