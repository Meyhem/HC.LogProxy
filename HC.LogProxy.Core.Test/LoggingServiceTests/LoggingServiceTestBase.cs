using System;
using HC.LogProxy.Dal;
using HC.LogProxy.Shared;
using Moq;

namespace HC.LogProxy.Core.Test.LoggingServiceTests
{
    public class LoggingServiceTestBase
    {
        public Mock<IDateTimeOffsetProvider> DateTimeOffsetProviderMock { get; }
        public Mock<ILogRepository> LogRepository { get; }
        public Mock<IIdentifierProvider> IdentifierProvider { get; }
        
        public LoggingServiceTestBase()
        {
            DateTimeOffsetProviderMock = new Mock<IDateTimeOffsetProvider>();
            LogRepository = new Mock<ILogRepository>();
            IdentifierProvider = new Mock<IIdentifierProvider>();
            
            DateTimeOffsetProviderMock.Setup(d => d.GetUtcNow())
                .Returns(DateTimeOffset.MinValue);

            IdentifierProvider.Setup(i => i.GetUuid())
                .Returns("1");
        }
    }
}