using System.Text.Json;

namespace FitbitAuthSample.Services
{
    public class FitbitClient
    {
        private readonly HttpClient httpClient;

        private static readonly JsonSerializerOptions JsonOptions
            = new(JsonSerializerDefaults.Web);

        public FitbitClient(
            HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<ActivityList> GetActivityList(
            CancellationToken cancellationToken)
        {
            var response
                = await httpClient
                    .GetAsync(
                        "/1/user/-/activities/list.json?beforeDate=2023-03-03T00:00:00&sort=desc&limit=10&offset=0",
                        cancellationToken);

            response
                .EnsureSuccessStatusCode();

            return await response
                    .Content
                    .ReadFromJsonAsync<ActivityList>(
                        JsonOptions,
                        cancellationToken)
                    ?? throw new InvalidOperationException("null response");
        }
    }
}
