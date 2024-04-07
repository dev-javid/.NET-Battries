namespace $safeprojectname$.Common.Abstract
{
    public interface IDateTime
    {
        DateTime UtcNow { get; }

        DateOnly UtcToday { get; }
    }

    internal class DateTimeProvider : IDateTime
    {
        public DateTime UtcNow => DateTime.UtcNow;

        public DateOnly UtcToday => DateOnly.FromDateTime(DateTime.UtcNow);
    }
}
