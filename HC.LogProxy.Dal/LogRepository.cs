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
                .PostAsJsonAsync("/v0/appD1b1YjWoXkUJwR/Messages?maxRecords=3&view=Grid%20view", request, ct);
            var str = await response.Content.ReadAsStringAsync(ct);
            response.EnsureSuccessStatusCode();
        }

        public async Task<LogRecord[]> GetAllLogsAsync(CancellationToken ct)
        {
            var response = await client
                .GetFromJsonAsync<GetLogResponse>("/v0/appD1b1YjWoXkUJwR/Messages?maxRecords=3&view=Grid%20view", ct);

            return response.Records;
        }
    }
}