using CoreApiResponse;
using GaHipHop_Model.DTO.Request;
using GaHipHop_Model.DTO.Response;
using GaHipHop_Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Tools;

namespace GaHipHop_API.Controllers.cart
{
    [ApiController]
    [Route("api/cart")]
    public class CartController : BaseController
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpPost("add")]
        /*public IActionResult AddItemToCart([FromBody] AddItemToCartRequest request)
        {
            try
            {
                var cartItemDTO = _cartService.AddItem(request.ProductId, request.Quantity);
                return CustomResult("Item added to cart.", cartItemDTO);
            }
            catch (Exception ex)
            {
                return CustomResult("Not found product.", HttpStatusCode.NotFound);
            }

        }*/
        public IActionResult AddItemToCart([FromBody] AddItemToCartRequest request)
        {
            try
            {
                var cartItemDTO = _cartService.AddItem(request.ProductId, request.Quantity, request.imageId);
                return CustomResult("Item added to cart.", cartItemDTO);
            }
            catch (CustomException.DataNotFoundException ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.NotFound);
            }
            catch (CustomException.InvalidDataException ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                return CustomResult("An error occurred while adding the item to the cart.", HttpStatusCode.InternalServerError);
            }
        }


        [HttpDelete("remove/{productId}")]
        public IActionResult RemoveItemFromCart(long productId)
        {
            _cartService.RemoveItem(productId);
            return CustomResult("Item removed from cart.");
        }

        [HttpPut("update")]
        /*public IActionResult UpdateItemQuantity([FromBody] UpdateItemQuantityRequest request)
        {
            _cartService.UpdateItemQuantity(request.ProductId, request.Quantity);
            return CustomResult("Item quantity updated.");
        }*/
        public IActionResult UpdateItemQuantityInCart([FromBody] UpdateItemQuantityRequest request)
        {
            try
            {
                _cartService.UpdateItemQuantity(request.ProductId, request.Quantity, request.imgId);
                return CustomResult("Item quantity updated successfully.");
            }
            catch (CustomException.DataNotFoundException ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.NotFound);
            }
            catch (CustomException.InvalidDataException ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                return CustomResult("An error occurred while updating the item quantity.", HttpStatusCode.InternalServerError);
            }
        }


        [HttpDelete("clear")]
        public IActionResult ClearCart()
        {
            _cartService.ClearCart();
            return CustomResult("Cart cleared.");
        }

        [HttpGet("items")]
        public IActionResult GetCartItems()
        {
            var items = _cartService.GetCartItems();
            return CustomResult("View item from cart.", items);
        }
    }
}
