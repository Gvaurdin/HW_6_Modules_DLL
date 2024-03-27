using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextModule
{
    public static class Text
    {
        public static bool IsPalindrome(string text)
        {
            string cleanedText = new string(text.Where(char.IsLetterOrDigit).ToArray());
            cleanedText = cleanedText.ToLower();
            return cleanedText.SequenceEqual(cleanedText.Reverse());
        }

        public static int CountSentences(string text)
        {
            int count = text.Count(c => c == '.' || c == '!' || c == '?');
            return count;
        }

        public static string ReverseString(string text)
        {
            char[] charArray = text.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }
    }
}
