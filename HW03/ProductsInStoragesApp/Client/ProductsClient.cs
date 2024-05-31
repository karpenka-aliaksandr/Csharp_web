using ProductInStorageApp.DTO;
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

        public async Task<IEnumerable<ProductDTOResponse>> GetProducts()
        {
            IEnumerable<ProductDTOResponse> data = await client.GetFromJsonAsync<IEnumerable<ProductDTOResponse>>($"https://localhost:7132/Product/GetProducts");
            return data;
        }




    }
    
}

