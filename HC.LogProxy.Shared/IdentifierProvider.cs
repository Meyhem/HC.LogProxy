using System;

namespace HC.LogProxy.Shared
{
    public interface IIdentifierProvider
    {
        string GetUuid();
    }

    public class IdentifierProvider : IIdentifierProvider
    {
        public string GetUuid()
        {
            return Guid.NewGuid().ToString();
        }
    }
}