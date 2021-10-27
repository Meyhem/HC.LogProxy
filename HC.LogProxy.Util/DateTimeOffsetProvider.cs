using System;

namespace HC.LogProxy.Util
{
    public interface IDateTimeOffsetProvider
    {
        DateTimeOffset GetUtcNow();
    }
    
    public class DateTimeOffsetProvider : IDateTimeOffsetProvider
    {
        public DateTimeOffset GetUtcNow()
        {
            return DateTimeOffset.UtcNow;
        }
    }
}