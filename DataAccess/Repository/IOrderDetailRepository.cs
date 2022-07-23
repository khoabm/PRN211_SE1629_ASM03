using BusinessObject.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface IOrderDetailRepository

    {
        IEnumerable<OrderDetail> GetOrderDetails(int id);
        IEnumerable<OrderDetail> GetOrderDetails();
        List<int> GetProductIds();
        void AddOrderDetails(OrderDetail orderDetail);
        void DeleteOrderDetails(OrderDetail orderDetail);
        string? GetProductName(int productId);
        OrderDetail GetOrderDetailByIDByProductID(int v1, int v2);
        void Update(OrderDetail orderDetail);
        
    }
}
