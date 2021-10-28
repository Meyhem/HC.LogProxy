using System;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using HC.LogProxy.Core.Dto;
using HC.LogProxy.Dal.Dto;
using Xunit;

namespace HC.LogProxy.Core.Test.LoggingServiceTests
{
    public class TestGetLogsAsync : LoggingServiceTestBase
    {
        [Fact]
        public async Task TestFetchesAndTransformsLogRecords()
        {
            LogRepository.Setup(r => r.GetAllLogsAsync(CancellationToken.None))
                .Returns(Task.FromResult(new LogRecord[]
                {
                    new()
                    {
                        Id = "123",
                        CreatedTime = DateTimeOffset.MinValue,
                        Fields = new Fields
                        {
                            Id = "1",
                            Message = "Text message",
                            Summary = "Text summary",
                            ReceivedAt = DateTimeOffset.MinValue.ToString("o")
                        }
                    }
                }));

            var service = new LoggingService(LogRepository.Object,
                DateTimeOffsetProviderMock.Object,
                IdentifierProvider.Object);

            var logs = await service.GetLogsAsync(CancellationToken.None);

            logs.Should().HaveCount(1);
            logs.Should().BeEquivalentTo(new[]
            {
                new LogRecordDto
                {
                    Id = "1",
                    Text = "Text message",
                    Title = "Text summary",
                    ReceivedAt = DateTimeOffset.MinValue
                }
            });
        }

        [Fact]
        public async Task TestHandlesNullFieldsGracefully()
        {
            LogRepository.Setup(r => r.GetAllLogsAsync(CancellationToken.None))
                .Returns(Task.FromResult(new LogRecord[]
                {
                    new()
                    {
                        Id = null,
                        CreatedTime = DateTimeOffset.MinValue,
                        Fields = null
                    }
                }));
            
            var service = new LoggingService(LogRepository.Object,
                DateTimeOffsetProviderMock.Object,
                IdentifierProvider.Object);

            var logs = await service.GetLogsAsync(CancellationToken.None);
            
            logs.Should().HaveCount(1);
            logs.Should().BeEquivalentTo(new[]
            {
                new LogRecordDto
                {
                    Id = null,
                    Text = null,
                    Title = null,
                    ReceivedAt = null
                }
            });
        }
    }
}