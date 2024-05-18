using CoreApiResponse;
using GaHipHop_Model.DTO.Request;
using GaHipHop_Service.Interfaces;
using GaHipHop_Service.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace GaHipHop_API.Controllers.Product
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : BaseController
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public IActionResult GetAllProduct()
        {
            var product = _productService.GetAllProduct();
            return CustomResult("Get all data Successfully", product);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(long id)
        {
            try
            {
                var product = await _productService.GetProductById(id);

                return CustomResult("Create Product successful", product);
            }
            catch (Exception ex)
            {
                return CustomResult("Product not found.", HttpStatusCode.NotFound);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(long id)
        {
            try
            {
                var result = await _productService.DeleteProduct(id);
                if (result)
                {
                    return CustomResult("Delete Successful.");
                }
                else
                {
                    return CustomResult("Product not found.", HttpStatusCode.NotFound);
                }
            }
            catch (Exception ex)
            {
                return CustomResult("Delete Fail.", HttpStatusCode.BadRequest);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] ProductRequest productRequest)
        {
            if (!ModelState.IsValid)
            {
                //return BadRequest(ModelState);
                return CustomResult(ModelState, HttpStatusCode.BadRequest);
            }

            var result = await _productService.CreateProduct(productRequest);

            if (!result.Status)
            {
                return CustomResult("Create fail.", new { productName = result.ProductName }, HttpStatusCode.Conflict);
            }

            return CustomResult("Create Successful", result);

        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateProduct(long id, [FromBody] ProductRequest productRequest)
        {
            if (!ModelState.IsValid)
            {
                //return BadRequest(ModelState);
                return CustomResult(ModelState, HttpStatusCode.BadRequest);
            }

            try
            {
                var result = await _productService.UpdateProduct(id, productRequest);
                return CustomResult("Update Successful", result);
            }
            catch (Exception ex)
            {
                return CustomResult("Update Product Fail", HttpStatusCode.BadRequest);
            }
        }
    }
}
