using GaHipHop_Model.DTO.Response;
using GaHipHop_Repository.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GaHipHop_Service.Interfaces
{
    public interface ICartService
    {
        CartItem AddItem(long productId, int quantity, long imageId);
        void RemoveItem(long productId);
        void UpdateItemQuantity(long productId, int quantity, long imageId);
        void ClearCart();
        CartResponse GetCartItems();
    }
}
