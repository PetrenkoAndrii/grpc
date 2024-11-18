using System.Text.RegularExpressions;

namespace HttpService.Extensions;

public static class ValidationDataExtensions
{
    public static bool ValidateEmail(this string email)
    {
        const string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
        return Regex.IsMatch(email, pattern);
    }
}
