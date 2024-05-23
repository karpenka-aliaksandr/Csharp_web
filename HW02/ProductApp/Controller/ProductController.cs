﻿using Microsoft.AspNetCore.Mvc;
using ProductApp.DTO;
using ProductApp.Model;
using ProductApp.Repo.Abstraction;
using System.Text;

namespace ProductApp.Controller
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepo _repo;
        public ProductController(IProductRepo repo)
        {
            _repo = repo;
        }

        [HttpPost(template: "addproduct")]
        public ActionResult<int> AddProduct(ProductDTORequest productDTORequest)
        {
            try
            {
                var id = _repo.AddProduct(productDTORequest);
                return id;
            }
            catch
            {
                return StatusCode(409);
            }
        }

        [HttpGet(template: "getproducts")]
        public ActionResult<IEnumerable<ProductDTOResponse>> GetProducts()
        {
            try
            {
                var products = _repo.GetProducts().ToList();
                return products;
            }
            catch
            {
                return StatusCode(523, "База данных недоступна");
            }
        }

        [HttpDelete(template: "deleteproduct")]
        public ActionResult<ProductDTOResponse> DeleteProducts(int id)
        {
            try
            {
                var product = _repo.DeleteProduct(id);
                return product;
            }
            catch
            {
                return BadRequest();
            }
        }

        private string GetCsv(IEnumerable<ProductDTOResponse> products)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("ID;Name;Description");
            foreach (var product in products)
            {
                sb.AppendLine(product.Id + ";" + product.Name + ";" + product.Description);
            }
            return sb.ToString();
        }

        [HttpGet(template: "getproductscsv")]
        public FileContentResult GetProductsCsv()
        {
            var sb = new StringBuilder();
            var products = _repo.GetProducts().ToList();
            var content = GetCsv(products);
            return File(new UTF8Encoding().GetBytes(content), "text/scv", "products.csv");
        }
    }
}
