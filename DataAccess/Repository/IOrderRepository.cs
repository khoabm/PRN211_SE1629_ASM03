using BusinessObject.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface IOrderRepository
    {
        IEnumerable<Order> GetOrders();
        void AddOrder(Order order);
        List<int> GetMemberIds();
        void DeleteOrder(Order order);
        Order GetOrderById(int id);
        void UpdateOrder(Order order);
        IEnumerable<Order>? GetOrderByMemberId(int id);
        Dictionary<Order, double> GetOrdersByDate(DateTime start, DateTime end);
    }
}
