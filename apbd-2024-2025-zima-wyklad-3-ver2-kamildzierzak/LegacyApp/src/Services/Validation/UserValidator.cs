using LegacyApp.src.Services.Common;
using System;

namespace LegacyApp.src.Services.Validation;

public class UserValidator : IUserValidator
{
    public bool Validate(string firstName, string lastName, string email, DateTime dateOfBirth)
    {
        if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName))
        {
            return false;
        }

        if (!email.Contains("@") && !email.Contains("."))
        {
            return false;
        }

        int age = DateUtils.CalculateAge(dateOfBirth);

        return age >= 21;
    }
}