using System;

namespace LyonL
{
    public static class Predicate
    {
        public static void TryCache(Action action)
        {
            try
            {
                action.Invoke();
            }
            catch (Exception)
            {
                // ignored
            }
        }

        public static TReturn TryCache<TReturn>(Func<TReturn> func)
        {
            try
            {
                return func();
            }
            catch (Exception)
            {
                return default(TReturn);
            }
        }

        public static TReturn TryCache<TReturn>(Func<TReturn> func, Func<TReturn> onError)
        {
            try
            {
                return func();
            }
            catch (Exception)
            {
                return onError();
            }
        }
    }
}