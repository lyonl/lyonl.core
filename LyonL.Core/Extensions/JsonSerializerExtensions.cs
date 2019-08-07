using System;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Bson;

namespace LyonL.Extensions
{
    public static class JsonSerializerExtensions
    {
        public static byte[] SerializeToBson<TValue>(this JsonSerializer jsonSerializer, TValue value)
            where TValue : class
        {
            if (value == null) return null;

            using (var memoryStream = new MemoryStream())
            {
                var writer = new BsonDataWriter(memoryStream);
                jsonSerializer.Serialize(writer, value);

                var objectDataAsStream = memoryStream.ToArray();
                return objectDataAsStream;
            }
        }

        public static TValue DeserializeFromBson<TValue>(this JsonSerializer jsonSerializer, byte[] stream,
            DateTimeKind dateTimeKindHandling = DateTimeKind.Utc) where TValue : class
        {
            if (stream == null) return default;

            using (var memoryStream = new MemoryStream(stream))
            {
                var reader = new BsonDataReader(memoryStream) {DateTimeKindHandling = dateTimeKindHandling};

                var result = jsonSerializer.Deserialize<TValue>(reader);
                return result;
            }
        }
    }
}