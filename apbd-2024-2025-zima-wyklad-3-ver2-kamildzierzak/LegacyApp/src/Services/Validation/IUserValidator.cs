using System;

namespace LegacyApp.src.Services.Validation;
public interface IUserValidator
{
    bool Validate(string firstName, string lastName, string email, DateTime dateOfBirth);
}
