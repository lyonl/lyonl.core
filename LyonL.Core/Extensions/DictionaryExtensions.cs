using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace LyonL.Extensions
{
    public static class DictionaryExtensions
    {
        public static ReadOnlyDictionary<TKey, TValue> AsReadOnly<TKey, TValue>(
            this IDictionary<TKey, TValue> dictionary)
        {
            return new ReadOnlyDictionary<TKey, TValue>(dictionary);
        }

        public static T MergeLeft<T, TK, TV>(this T me, params IDictionary<TK, TV>[] others)
            where T : IDictionary<TK, TV>, new()
        {
            var newMap = new T();
            foreach (var src in
                new List<IDictionary<TK, TV>> {me}.Concat(others))
            foreach (var p in src)
                newMap[p.Key] = p.Value;

            return newMap;
        }
    }
}