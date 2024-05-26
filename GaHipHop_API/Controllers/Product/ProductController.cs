using CoreApiResponse;
using GaHipHop_Model.DTO.Request;
using GaHipHop_Model.DTO.Response;
using GaHipHop_Service.Interfaces;
using GaHipHop_Service.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Tools;

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

        [HttpGet("GetAllProduct")]
        public IActionResult GetAllProduct()
        {
            var product = _productService.GetAllProduct();
            return CustomResult("Get all data Successfully", product);
        }

        [HttpGet("GetProductById/{id}")]
        public async Task<IActionResult> GetProductById(long id)
        {
            try
            {
                var product = await _productService.GetProductById(id);

                return CustomResult("Product is found", product);
            }
            catch (CustomException.DataNotFoundException ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.NotFound);
            }
        }

        [HttpPost("CreateProduct")]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductRequest createProductRequest)
        {
            if (!ModelState.IsValid)
            {
                return CustomResult(ModelState, HttpStatusCode.BadRequest);
            }

            var result = await _productService.CreateProduct(createProductRequest);

            if (!result.Status)
            {
                return CustomResult("Create fail.", new { productName = result.ProductName }, HttpStatusCode.Conflict);
            }

            return CustomResult("Create Successful", result);

        }

        [HttpPatch("UpdateProduct/{id}")]
        public async Task<IActionResult> UpdateProduct(long id, [FromBody] UpdateProductRequest updateProductRequest)
        {
            try
            {
                ProductResponse product = await _productService.UpdateProduct(id, updateProductRequest);
                return CustomResult("updated Successful", product, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        [HttpDelete("DeleteProduct/{id}")]
        public async Task<IActionResult> DeleteProduct(long id)
        {
            try
            {
                var deleteProduct = await _productService.DeleteProduct(id);
                return CustomResult("Delete Prodcut Successfull (Status)", deleteProduct, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.InternalServerError);
            }


        }
    }
}
