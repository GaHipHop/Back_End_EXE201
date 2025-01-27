﻿using CoreApiResponse;
using GaHipHop_Model.DTO.Request;
using GaHipHop_Service.Interfaces;
using GaHipHop_Service.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Tools;

namespace GaHipHop_API.Controllers.Order
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : BaseController
    {
        private readonly IOrderService _orderService;
        
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost("createOrder")]
        public async Task<IActionResult> CreateOder([FromBody] OrderRequest orderRequest)
        {
            try
            {
                var result = await _orderService.CreateOrder(orderRequest);

                return CustomResult("Create Successful", result);
            }
            catch (CustomException.DataNotFoundException ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.NotFound);
            }
            catch (CustomException.ForbbidenException ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.Forbidden);
            }
            catch (CustomException.InternalServerErrorException ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.InternalServerError);
            }

        }

        [HttpPatch("updateStatusOrderConfirmed/{id}")]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> UpdateStatusOrderConfirmed(long id)
        {

            try
            {
                var result = await _orderService.UpdateStatusOrderConfirmed(id);
                return CustomResult("Confirmed Order Successful", result);
            }
            catch (CustomException.DataNotFoundException ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.NotFound);
            }
            catch (CustomException.ForbbidenException ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.Forbidden);
            }
            catch (CustomException.InvalidDataException ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.BadRequest);
            }
            catch (CustomException.InternalServerErrorException ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        [HttpPatch("updateStatusOrderReject/{id}")]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> UpdateStatusOrderReject(long id)
        {

            try
            {
                var result = await _orderService.UpdateStatusOrderReject(id);
                return CustomResult("Reject Order Successful", result);
            }
            catch (CustomException.DataNotFoundException ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.NotFound);
            }
            catch (CustomException.ForbbidenException ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.Forbidden);
            }
            catch (CustomException.InvalidDataException ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.BadRequest);
            }
            catch (CustomException.InternalServerErrorException ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.InternalServerError);
            }
        }


        [HttpGet("getOrderByStatusPending")]
        [Authorize(Roles ="Admin,Manager")]
        public IActionResult GetAllOrderByStatusPending(string? keyword, int pageIndex, int pageSize)
        {
            try
            {
                var order = _orderService.GetAllOrderByStatusPending(keyword, pageIndex, pageSize);
                return CustomResult("Data load Successful", order);
            }
            catch (CustomException.ForbbidenException ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.Forbidden);
            }
            catch (CustomException.InternalServerErrorException ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.InternalServerError);
            }

        }

        [HttpGet("getOrderByStatusConfirmed")]
        [Authorize(Roles = "Admin,Manager")]
        public IActionResult GetAllOrderByStatusConfirmed(string? keyword, int pageIndex, int pageSize)
        {
            try
            {
                var order = _orderService.GetAllOrderByStatusConfirmed(keyword, pageIndex, pageSize);
                return CustomResult("Data load Successful", order);
            }
            catch (CustomException.ForbbidenException ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.Forbidden);
            }
            catch (CustomException.InternalServerErrorException ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.InternalServerError);
            }

        }

        [HttpGet("getOrderByStatusRejected")]
        [Authorize(Roles = "Admin,Manager")]
        public IActionResult getAllOrderByStatusReject(string? keyword, int pageIndex, int pageSize)
        {
            try
            {
                var order = _orderService.GetAllOrderByStatusReject(keyword, pageIndex, pageSize);
                return CustomResult("Data load Successful", order);
            }
            catch (CustomException.ForbbidenException ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.Forbidden);
            }
            catch (CustomException.InternalServerErrorException ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.InternalServerError);
            }

        }

        [HttpGet("getOrderById/{id}")]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> GetOrderById(long id)
        {
            try
            {
                var order = await _orderService.GetOrderById(id);

                return CustomResult("Get Order successful.", order);
            }
            catch (CustomException.DataNotFoundException ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.NotFound);
            }
            catch (CustomException.ForbbidenException ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.Forbidden);
            }
            catch (CustomException.InternalServerErrorException ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet("GetOrdersSummaryByMonthYear")]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> GetOrdersSummaryByMonthYear(int month, int year)
        {
            try
            {
                var count = await _orderService.GetOrdersSummaryByMonthYear(month, year);
                return Ok(count);
            }
            catch (CustomException.InternalServerErrorException ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.InternalServerError);
            }
        }
    }
}
