using System.IO;
using Newtonsoft.Json;

namespace LyonL.Extensions
{
    public static class StreamExtensions
    {
        private static readonly JsonSerializer JsonSerializer;

        static StreamExtensions()
        {
            JsonSerializer = new JsonSerializer
            {
                DateTimeZoneHandling = DateTimeZoneHandling.Utc
            };
        }

        public static T Deserialize<T>(this Stream stream)
        {
            using (var sr = new StreamReader(stream))
            using (var jsonTextReader = new JsonTextReader(sr))
            {
                return JsonSerializer.Deserialize<T>(jsonTextReader);
            }
        }
    }
}