namespace Cheshan.Collection.Shop.Database.Extensions
{
    public static class StringExtensions
    {
        public static string Replace(this string value, char[] replaces, char replacement)
        {
            foreach (var replacer in replaces)
            {
                value = value.Replace(replacer, replacement);
            }

            return value;
        }
    }
}
