using System.Globalization;

namespace CashingWithPaginationDemo.Src
{
    public record RequestResult(string Time, IEnumerable<EntityModel> Data);
    public static class TimeSpanExtensions
    {
        public static string ToSecondsString(this TimeSpan ts)
            => ts.TotalSeconds.ToString("F3", CultureInfo.InvariantCulture) + " s";
    }

}
