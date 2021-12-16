using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace PlayInventoryServiceApp.Clients
{
    public class CatalogClient
    {
        private readonly HttpClient _httpClient;

        public CatalogClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }


        public async Task<IReadOnlyCollection<CatalogItemDto>> GetCatalogAsync()
        {
            try
            {
                var items = await _httpClient.GetFromJsonAsync<IReadOnlyCollection<CatalogItemDto>>("/api/Items");
                return items;
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}
