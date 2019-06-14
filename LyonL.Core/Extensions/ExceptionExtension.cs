using System;
using System.Linq;

namespace LyonL.Extensions
{
    public static class ExceptionExtension
    {
        public static string GetExceptionDetails(this Exception exception)
        {
            var properties = exception.GetType()
                .GetProperties();
            var fields = properties
                .Select(property => new
                {
                    property.Name,
                    Value = property.GetValue(exception, null)
                })
                .Select(x => $"{x.Name}: {x.Value?.ToString() ?? string.Empty}");
            return string.Join(Environment.NewLine, fields);
        }
    }
}