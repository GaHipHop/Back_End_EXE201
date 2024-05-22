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
        //CartItem AddItem(long productId, int quantity, long imageId);
        CartItem AddItem(long id, int quantity);
        void RemoveItem(long id);
        void UpdateItemQuantity(long id, int quantity);
        void ClearCart();
        CartResponse GetCartItems();
    }
}
