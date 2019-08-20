using System;
using System.Numerics;
using System.Text;
using MandateThat;
using LyonL.Properties;

namespace LyonL.Extensions
{
    public static class BaseConverter
    {
        public static string ConvertToBaseString(this byte[] valueAsArray, string digits, int pad)
        {
            Mandate.That(digits, nameof(digits)).IsNotNullOrEmpty();

            if (digits.Length < 2)
                throw new ArgumentOutOfRangeException(nameof(digits), Resources.BaseConverter_OutOfRangeMessage);

            var value = new BigInteger(valueAsArray);
            var isNeg = value < 0;
            value = isNeg ? -value : value;

            var sb = new StringBuilder(pad + (isNeg ? 1 : 0));

            do
            {
                BigInteger rem;
                value = BigInteger.DivRem(value, digits.Length, out rem);
                sb.Append(digits[(int) rem]);
            } while (value > 0);

            // pad it
            if (sb.Length < pad) sb.Append(digits[0], pad - sb.Length);

            // if the number is negative, add the sign.
            if (isNeg) sb.Append('-');

            // reverse it
            for (int i = 0, j = sb.Length - 1; i < j; i++, j--)
            {
                var t = sb[i];
                sb[i] = sb[j];
                sb[j] = t;
            }

            return sb.ToString();
        }

        public static BigInteger ConvertFromBaseString(this string s, string digits)
        {
            switch (Parse(s, digits, out var result))
            {
                case ParseCode.FormatError:
                    throw new FormatException(Resources.BaseConverter_FormatExceptionMessage);
                case ParseCode.NullString:
                    throw new ArgumentNullException(nameof(s));
                case ParseCode.NullDigits:
                    throw new ArgumentNullException(nameof(digits));
                case ParseCode.InsufficientDigits:
                    throw new ArgumentOutOfRangeException(nameof(digits), Resources.BaseConverter_OutOfRangeMessage);
                case ParseCode.Overflow:
                    throw new OverflowException();
            }

            return result;
        }

        public static bool TryConvertFromBase62String(this string s, string digits, out BigInteger result)
        {
            return Parse(s, digits, out result) == ParseCode.Success;
        }

        private static ParseCode Parse(this string s, string digits, out BigInteger result)
        {
            result = 0;

            if (s == null) return ParseCode.NullString;

            if (digits == null) return ParseCode.NullDigits;

            if (digits.Length < 2) return ParseCode.InsufficientDigits;

            // skip leading white space
            var i = 0;
            while (i < s.Length && char.IsWhiteSpace(s[i])) ++i;

            if (i >= s.Length) return ParseCode.FormatError;

            // get the sign if it's there.
            BigInteger sign = 1;
            switch (s[i])
            {
                case '+':
                    ++i;
                    break;

                case '-':
                    ++i;
                    sign = -1;
                    break;
            }

            // Make sure there's at least one digit
            if (i >= s.Length) return ParseCode.FormatError;

            // Parse the digits.
            while (i < s.Length)
            {
                var n = digits.IndexOf(s[i]);
                if (n < 0) return ParseCode.FormatError;

                var oldResult = result;
                result = result * digits.Length + n;
                if (result < oldResult) return ParseCode.Overflow;

                ++i;
            }

            // skip trailing white space
            while (i < s.Length && char.IsWhiteSpace(s[i])) ++i;

            // and make sure there's nothing else.
            if (i < s.Length) return ParseCode.FormatError;

            if (sign < 0) result = -result;

            return ParseCode.Success;
        }

        private enum ParseCode
        {
            Success,
            NullString,
            NullDigits,
            InsufficientDigits,
            Overflow,
            FormatError
        }
    }
}