namespace LyonL
{
    public interface IUniqueIdGenerator
    {
        long WorkerId { get; }

        long[] BlockOfIds(int count);

        long NextId();
    }
}