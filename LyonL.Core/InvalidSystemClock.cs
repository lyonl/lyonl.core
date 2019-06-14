using System;

namespace LyonL
{
    [Serializable]
    public class InvalidSystemClock : Exception
    {
        public InvalidSystemClock(string message)
            : base(message)
        {
        }
    }
}