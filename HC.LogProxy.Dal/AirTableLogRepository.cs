using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using HC.LogProxy.Dal.Dto;
using Microsoft.Extensions.Logging;

namespace HC.LogProxy.Dal
{
    public class AirTableLogRepository : ILogRepository
    {
        private readonly ILogger<AirTableLogRepository> logger;
        private const int MaxFetchCountThreshold = 5000;
        private readonly HttpClient client;

        public AirTableLogRepository(IHttpClientFactory httpClientFactory, ILogger<AirTableLogRepository> logger)
        {
            this.logger = logger;
            client = httpClientFactory.CreateClient("AirTableClient");
        }

        public async Task AddLogsAsync(LogFields[] fields, CancellationToken ct)
        {
            var request = new AddLogRequest
            {
                Records = fields
            };

            var response = await client.PostAsJsonAsync(
                "/v0/appD1b1YjWoXkUJwR/Messages",
                request,
                ct
            );
            
            response.EnsureSuccessStatusCode();
        }

        public async Task<LogRecord[]> GetAllLogsAsync(CancellationToken ct)
        {
            string? offset = null;
            var logRecords = new List<LogRecord>();

            do
            {
                var response = await client.GetAsync(
                    $"/v0/appD1b1YjWoXkUJwR/Messages?view=Grid%20view&offset={offset}",
                    ct
                );

                response.EnsureSuccessStatusCode();

                var data = await response.Content.ReadFromJsonAsync<GetLogResponse>(null, ct);
                if (data is null) break;

                logRecords.AddRange(data.Records);

                if (logRecords.Count > MaxFetchCountThreshold)
                {
                    logger.LogWarning("Hit MaxFetchCountThreshold={MaxFetchCountThreshold}, refusing to fetch more.", MaxFetchCountThreshold);
                    break;
                }
                
                offset = data.Offset;
            } while (!string.IsNullOrEmpty(offset));

            return logRecords.ToArray();
        }
    }
}