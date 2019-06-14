using System;
using System.Linq;
using System.Security;
using System.Text;

namespace LyonL.Extensions
{
    public static class StringExtensions
    {
        private const string Base62CharList = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";

        public static string CompactWhitespaces(this string s)
        {
            var sb = new StringBuilder(s);

            sb.CompactWhitespaces();
            return sb.ToString().Trim();
        }

        public static long FromBase62(this string input)
        {
            var reversed = input.Reverse();
            long result = 0;
            var pos = 0;
            foreach (var c in reversed)
            {
                result += Base62CharList.IndexOf(c) * (long) Math.Pow(62, pos);
                pos++;
            }

            return result;
        }

        public static SecureString ToSecureString(this string s)
        {
            return s.Aggregate(new SecureString(), AppendChar, MakeReadOnly);
        }

        private static SecureString MakeReadOnly(SecureString secureString)
        {
            secureString.MakeReadOnly();
            return secureString;
        }

        private static SecureString AppendChar(SecureString secureString, char c)
        {
            secureString.AppendChar(c);
            return secureString;
        }
    }
}