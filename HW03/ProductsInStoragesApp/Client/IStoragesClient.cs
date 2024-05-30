using System;
namespace ProductsInStoragesApp.Client
{
	public interface IStoragesClient
	{
        public Task<bool> Exists(int? id);
    }
}

