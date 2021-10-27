using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using HC.LogProxy.Dal.Dto;

namespace HC.LogProxy.Dal
{
    public class LogRepository : ILogRepository
    {
        private readonly HttpClient client;

        public LogRepository(IHttpClientFactory httpClientFactory)
        {
            client = httpClientFactory.CreateClient("AirTableClient");
        }

        public async Task AddLogsAsync(LogFields[] fields, CancellationToken ct)
        {
            var request = new AddLogRequest
            {
                Records = fields
            };
            
            var response = await client
                .PostAsJsonAsync("/messages?maxRecords=3&view=Grid%20view", request, ct);

            response.EnsureSuccessStatusCode();
        }

        public async Task<LogRecord[]> GetAllLogsAsync(CancellationToken ct)
        {
            var response = await client
                .GetFromJsonAsync<GetLogResponse>("/messages?maxRecords=3&view=Grid%20view", ct);

            return response.Records;
        }
    }
}