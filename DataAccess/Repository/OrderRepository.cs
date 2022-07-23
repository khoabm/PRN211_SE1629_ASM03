using BusinessObject.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class OrderRepository : IOrderRepository
    {
        public void AddOrder(Order order) => OrderDAO.Instance.CreateOrder(order);

        public void DeleteOrder(Order order) => OrderDAO.Instance.DeleteOrder(order);

        public List<int> GetMemberIds() => OrderDAO.Instance.GetAllMemberIds();

        public Order GetOrderById(int id) => OrderDAO.Instance.GetOrderById(id);

        public IEnumerable<Order>? GetOrderByMemberId(int id) => OrderDAO.Instance.GetOrderByMemberId(id);

        public IEnumerable<Order> GetOrders() => OrderDAO.Instance.GetOrders();

        public Dictionary<Order, double> GetOrdersByDate(DateTime start, DateTime end) => OrderDAO.Instance.GetOrdersByDate(start, end);
        

        public void UpdateOrder(Order order) => OrderDAO.Instance.UpdateOrder(order);
    }
}
