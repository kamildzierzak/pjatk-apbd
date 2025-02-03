using LegacyApp.src.Models;
using LegacyApp.src.Repositories;
using LegacyApp.src.Services.Credit;
using LegacyApp.src.Services.Validation;
using System;

namespace LegacyApp;
public class UserService
{
    private readonly IUserValidator _userValidator;
    private readonly IClientRepository _clientRepository;
    private readonly IUserRepository _userRepository;
    private readonly ClientCreditRuleFactory _creditRuleFactory;

    public UserService()
    {
        _userValidator = new UserValidator();
        _clientRepository = new ClientRepository();
        _userRepository = new UserRepository();
        _creditRuleFactory = new ClientCreditRuleFactory(new CreditLimitService());
    }

    public UserService(
        IUserValidator userValidator,
        IClientRepository clientRepository,
        IUserRepository userRepository,
        ClientCreditRuleFactory creditRuleFactory)
    {
        _userValidator = userValidator;
        _clientRepository = clientRepository;
        _userRepository = userRepository;
        _creditRuleFactory = creditRuleFactory;
    }



    public bool AddUser(string firstName, string lastName, string email, DateTime dateOfBirth, int clientId)
    {
        if (!_userValidator.Validate(firstName, lastName, email, dateOfBirth))
        {
            return false;
        }

        var client = _clientRepository.GetById(clientId);
        if (client == null)
        {
            return false;
        }

        var user = new User
        {
            Client = client,
            DateOfBirth = dateOfBirth,
            EmailAddress = email,
            FirstName = firstName,
            LastName = lastName
        };

        var creditRule = _creditRuleFactory.GetRule(client);
        creditRule.Apply(user);

        if (user.HasCreditLimit && user.CreditLimit < 500)
        {
            return false;
        }

        _userRepository.AddUser(user);
        return true;
    }
}
