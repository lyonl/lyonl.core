using System;
using System.Runtime.InteropServices;
using System.Security;
using EnsureThat;

namespace LyonL.Extensions
{
    public static class SecureStringExtensions
    {
        public static SecureString AppendString(this SecureString s, string src)
        {
            Ensure.That(src, nameof(src)).IsNotNullOrEmpty();
            foreach (var c in src.ToCharArray()) s.AppendChar(c);

            return s;
        }

        public static string GetString(this SecureString value)
        {
            var valuePtr = IntPtr.Zero;
            try
            {
                valuePtr = Marshal.SecureStringToGlobalAllocUnicode(value);
                return Marshal.PtrToStringUni(valuePtr);
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(valuePtr);
            }
        }
    }
}