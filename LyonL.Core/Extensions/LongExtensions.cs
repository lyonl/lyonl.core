using System;
using System.Collections.Generic;
using LyonL.Properties;

namespace LyonL.Extensions
{
    public static class LongExtensions
    {
        private const string Base62CharList = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";

        public static string ToBase62(this long input)
        {
            if (input < 0)
                throw new ArgumentOutOfRangeException(nameof(input), input,
                    Resources.LongExtensions_ToBase62_RangeExceptionMessage);

            var clistarr = Base62CharList.ToCharArray();
            var result = new Stack<char>();
            while (input != 0)
            {
                result.Push(clistarr[input % 62]);
                input /= 62;
            }

            return new string(result.ToArray());
        }
    }
}