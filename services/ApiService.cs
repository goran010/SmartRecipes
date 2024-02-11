using System.Net.Http;


namespace RecipeApp.services
{
    internal class ApiService
    {
        private readonly HttpClient httpClient;

        public ApiService()
        {
            httpClient = new HttpClient();
        }

        public async Task<string> GetMealAsync(string apiEndpoint)
        {
            try
            {
                HttpResponseMessage response = await httpClient.GetAsync(apiEndpoint);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync();
                }
                else
                {
                    throw new Exception($"Error: {response.StatusCode} - {response.ReasonPhrase}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error: {ex.Message}");
            }
        }
    }
}
