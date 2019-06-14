namespace LyonL
{
    public class Singleton<T> where T : class, new()
    {
        protected Singleton()
        {
        }

        public static T Instance => SingletonCreator.CreatorInstance;

        private static class SingletonCreator
        {
            static SingletonCreator()
            {
            }

            // ReSharper disable once StaticMemberInGenericType
            internal static T CreatorInstance { get; } = CreateInstance();

            private static T CreateInstance()
            {
                return new T();
            }
        }
    }
}