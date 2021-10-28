using System;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using HC.LogProxy.Core.Dto;
using HC.LogProxy.Dal.Dto;
using Moq;
using Xunit;

namespace HC.LogProxy.Core.Test.LoggingServiceTests
{
    public class TestCreateLogAsync : LoggingServiceTestBase
    {
        [Fact]
        public async Task TestCallsRepositoryWithEnrichedValues()
        {
            LogRepository.Setup(r => r.AddLogsAsync(It.IsAny<LogFields[]>(), CancellationToken.None))
                .Callback((LogFields[] logFields, CancellationToken _) =>
                {
                    logFields.Should().BeEquivalentTo(new[]
                    {
                        new LogFields
                        {
                            Fields = new Fields
                            {
                                Id = "1",
                                Message = "Test text",
                                Summary = "Test title",
                                ReceivedAt = DateTimeOffset.MinValue.ToString("o")
                            }
                        }
                    });
                });

            var service = new LoggingService(LogRepository.Object,
                DateTimeOffsetProviderMock.Object,
                IdentifierProvider.Object);

            await service.CreateLogAsync(new CreateLogRecordDto()
            {
                Title = "Test title",
                Text = "Test text"
            }, CancellationToken.None);
        }
    }
}