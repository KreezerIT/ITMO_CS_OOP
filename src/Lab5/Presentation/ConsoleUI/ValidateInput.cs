using System.Text.RegularExpressions;

namespace Itmo.ObjectOrientedProgramming.Lab5.Presentation.ConsoleUI;

public static class ValidateInput
{
    public static string GetValidatedInput(string regex, string errorMessage)
    {
        while (true)
        {
            string? input = Console.ReadLine();
            if (!string.IsNullOrEmpty(input) && Regex.IsMatch(input, regex))
            {
                return input;
            }

            Console.WriteLine(errorMessage);
        }
    }
}