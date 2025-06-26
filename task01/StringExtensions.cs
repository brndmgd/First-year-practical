namespace task01;

public static class StringExtensions
{
    public static bool IsPalindrome(this string input)
    {
        if (string.IsNullOrEmpty(input)) return false;

        input = input.ToLower();
        string formattedInput = "";

        foreach (char c in input)
        {
            if (!char.IsPunctuation(c) && !char.IsWhiteSpace(c))
            {
                formattedInput += c;
            }
        }

        char[] charArray = formattedInput.ToCharArray();
        Array.Reverse(charArray);
        string reversedInput = string.Join("", charArray);

        return formattedInput == reversedInput;
    }
}
