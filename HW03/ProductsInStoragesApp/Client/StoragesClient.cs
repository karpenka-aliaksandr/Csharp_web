using System;
namespace ProductsInStoragesApp.Client
{
	public class StoragesClient: IStoragesClient
    {
        readonly HttpClient client = new HttpClient();

        public async Task<bool> Exists(int? id)
        {
            using HttpResponseMessage response = await client.GetAsync($"https://localhost:7163/Storage/CheckStorage?id={id.ToString()}");
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            if (responseBody == "true")
            {
                return true;
            }

            if (responseBody == "false")
            {
                return false;
            }

            throw new Exception("Unknow response");
        }
    }
}

