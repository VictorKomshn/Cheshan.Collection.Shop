namespace Cheshan.Collection.Shop.Core.Extensions
{
    internal static class StringExntensions
    {
        public static string ReplaceAll(this string inputString, IDictionary<string,string> replacements)
        {
            foreach (var replacement in replacements)
            {
                inputString =  inputString.Replace(replacement.Key, replacement.Value);
            }

            return inputString;
        }
    }
}
