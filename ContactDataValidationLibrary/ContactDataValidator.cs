using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ContactDataValidationLibrary
{
    public static class ContactDataValidator
    {
        // проверка, содержит ли указанное полное имя только буквы.
        public static bool IsValidFullName(string fullName)
        {
            return Regex.IsMatch(fullName, @"^[A-Za-zА-Яа-яЁё\s]+$");
        }

        // проверка, содержит ли указанный возраст только цифры.
        public static bool IsValidAge(string age)
        {
            return Regex.IsMatch(age, @"^\d+$");
        }

        // проверука, соответствует ли указанный номер телефона заданному формату.
        public static bool IsValidPhoneNumber(string phoneNumber)
        {
            return Regex.IsMatch(phoneNumber, @"^\+\d{1,3}\d{3}\s?\d{3}\d{2}\d{2}$");
        }

        // проверка, соответствует ли указанный email заданному формату.
        public static bool IsValidEmail(string email)
        {
            return Regex.IsMatch(email, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$");
        }
    }
}
