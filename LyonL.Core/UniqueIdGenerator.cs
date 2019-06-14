using System;

namespace LyonL
{
    /// <summary>
    ///     To generate unique id's - based on twitter snowflake
    /// </summary>
    public sealed class UniqueIdGenerator : IUniqueIdGenerator
    {
        private const long MaxWorkerId = -1 ^ (-1 << WorkerIdBits);
        private const int SequenceBits = 6;
        private const long SequenceMask = -1L ^ (-1L << SequenceBits);
        private const int TimestampLeftShift = WorkerIdBits + SequenceBits;
        private const int WorkerIdBits = 10;
        private static readonly DateTime Epoch = new DateTime(2017, 8, 12);
        private readonly object _lock = new object();
        private readonly long _shiftedWorkerId;
        private long _lastTimestamp = -1L;
        private long _sequence;

        public UniqueIdGenerator(long workerId)
        {
            if (workerId > MaxWorkerId && workerId < 0)
                throw new ArgumentException($"worker Id can't be greater than {MaxWorkerId} or less than 0");

            WorkerId = workerId;

            _shiftedWorkerId = (WorkerId & MaxWorkerId) << SequenceBits;
        }

        public long WorkerId { get; }

        public long[] BlockOfIds(int count)
        {
            var ids = new long[count];

            for (var i = 0; i < count; i++) ids[i] = NextId();

            return ids;
        }

        public long NextId()
        {
            lock (_lock)
            {
                return GetId(GenTimestamp());
            }
        }

        private static long GetTimestamp()
        {
            return (long) (DateTime.UtcNow - Epoch).TotalMilliseconds;
        }

        private void ClockCheck(long timestamp)
        {
            if (timestamp < _lastTimestamp)
                throw new InvalidSystemClock(
                    $"Clock moved backwards.  Refusing to generate id for {_lastTimestamp - timestamp} milliseconds"
                );
        }

        private long GenerateId(long timestamp)
        {
            return (timestamp << TimestampLeftShift) | _shiftedWorkerId | _sequence;
        }

        private long GenTimestamp()
        {
            var timestamp = GetTimestamp();

            ClockCheck(timestamp);
            return timestamp;
        }

        private long GetId(long timestamp)
        {
            if (_lastTimestamp == timestamp)
            {
                _sequence = NextSequence();

                if (_sequence == 0) timestamp = TillNextTimestamp(_lastTimestamp);
            }
            else
            {
                _sequence = 0;
            }

            _lastTimestamp = timestamp;

            return GenerateId(timestamp);
        }

        private long NextSequence()
        {
            _sequence++;

            return _sequence & SequenceMask;
        }

        private long TillNextTimestamp(long lastTimestamp)
        {
            var timestamp = GetTimestamp();

            if (timestamp > lastTimestamp) return timestamp;

            while (timestamp <= lastTimestamp) timestamp = GetTimestamp();

            _sequence = 0;

            return timestamp;
        }
    }
}