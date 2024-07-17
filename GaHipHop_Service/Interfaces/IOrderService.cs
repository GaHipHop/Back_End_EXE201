using GaHipHop_Model.DTO.Request;
using GaHipHop_Model.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GaHipHop_Service.Interfaces
{
    public interface IOrderService
    {
        Task<OrderResponse> CreateOrder(OrderRequest orderRequest);

        Task<bool> UpdateStatusOrderConfirmed(long id);

        Task<bool> UpdateStatusOrderReject(long id);

        Task<OrderResponse> GetOrderById(long id);

        List<OrderResponse> GetAllOrderByStatusPending(string? keyword, int pageIndex, int pageSize);

        List<OrderResponse> GetAllOrderByStatusConfirmed(string? keyword, int pageIndex, int pageSize);

        List<OrderResponse> GetAllOrderByStatusReject(string? keyword, int pageIndex, int pageSize);

        Task<OrderSummaryResponse> GetOrdersSummaryByMonthYear(int month, int year);
    }
}
