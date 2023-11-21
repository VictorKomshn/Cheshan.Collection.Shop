using System.Diagnostics.CodeAnalysis;

namespace Cheshan.Collection.Shop.Database.Comparers
{
    public class SearchComparer : IEqualityComparer<string>
    {
        public bool Equals(string? x, string? y)
        {
            return string.Equals(x, y, StringComparison.InvariantCultureIgnoreCase);
        }

        public int GetHashCode([DisallowNull] string obj)
        {
            return obj.GetHashCode();
        }
    }
}
