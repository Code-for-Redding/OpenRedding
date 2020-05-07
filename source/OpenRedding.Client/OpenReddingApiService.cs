namespace OpenRedding.Client
{
    using System;
    using System.Net.Http;

    public class OpenReddingApiService
    {
        private readonly HttpClient _httpClient;
        private readonly Uri _baseUrl;

        public OpenReddingApiService(HttpClient httpClient, Uri baseUrl) =>
            (_httpClient, _baseUrl) = (httpClient, baseUrl);

        /*
        public async Task<EmployeeSearchResultList> GetEmployees()
        {
            await Task.Delay(TimeSpan.FromMilliseconds(500));
        }
         */
    }
}
