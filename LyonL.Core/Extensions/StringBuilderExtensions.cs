using System.Text;
using static System.Char;

namespace LyonL.Extensions
{
    public static class StringBuilderExtensions
    {
        public static void CompactWhitespaces(this StringBuilder sb)
        {
            if (sb.Length == 0) return;

            var start = 0;

            while (start < sb.Length)
                if (IsWhiteSpace(sb[start]))
                    start++;
                else
                    break;

            if (start == sb.Length)
            {
                sb.Length = 0;
                return;
            }

            var end = sb.Length - 1;

            while (end >= 0)
                if (IsWhiteSpace(sb[end]))
                    end--;
                else
                    break;

            // compact string

            var dest = 0;
            var previousIsWhitespace = false;

            for (var i = start; i <= end; i++)
                if (IsWhiteSpace(sb[i]))
                {
                    if (previousIsWhitespace) continue;

                    previousIsWhitespace = true;
                    sb[dest] = ' ';
                    dest++;
                }
                else
                {
                    previousIsWhitespace = false;
                    sb[dest] = sb[i];
                    dest++;
                }

            sb.Length = dest;
        }
    }
}