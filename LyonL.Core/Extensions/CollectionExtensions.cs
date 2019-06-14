using System.Collections.Generic;
using System.Linq;
using static System.String;

namespace LyonL.Extensions
{
    public static class CollectionExtensions
    {
        public static void AddCommaSeparatedValues(this ICollection<string> collection, string commaSeparatedValues)
        {
            var values = commaSeparatedValues.Split(',');
            foreach (var value in values.Select(t => t.Trim()).Where(value => !IsNullOrEmpty(value)))
                collection.Add(value);
        }
    }
}