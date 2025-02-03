using System;

namespace LegacyApp.src.Services.Credit;
public interface ICreditLimitService
{
    int GetCreditLimit(string lastName, DateTime dateOfBirthday);
}