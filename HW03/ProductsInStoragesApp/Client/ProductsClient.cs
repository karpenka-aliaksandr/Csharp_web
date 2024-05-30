using System;
namespace ProductsInStoragesApp.Client
{
	public class ProductsClient:IProductsClient
	{   
        readonly HttpClient client = new HttpClient();

        public async Task<bool> Exists(int? id)
        {
            using HttpResponseMessage response = await client.GetAsync($"https://localhost:7132/Product/CheckProduct?id={id.ToString()}");
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

