namespace Cheshan.Collection.Shop.Core.Extensions
{
    public static class StringExntensions
    {
        /// <summary>
        /// Заменить все ключи на значения
        /// </summary>
        /// <param name="inputString"></param>
        /// <param name="replacements"></param>
        /// <returns>Текст, где все ключи заменены на значения</returns>
        public static string ReplaceAll(this string inputString, IDictionary<string, string> replacements)
        {
            foreach (var replacement in replacements)
            {
                inputString = inputString.Replace(replacement.Key, replacement.Value);
            }

            return inputString;
        }
    }
}
