using BusinessObject.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class OrderDAO
    {
        private static OrderDAO? instance = null;
        private static readonly object _instanceLock = new object();
        //private OrderDao() { }
        private OrderDAO()
        {

        }
        public static OrderDAO Instance
        {
            get
            {
                lock (_instanceLock)
                {
                    if (instance == null)
                        instance = new OrderDAO();
                }
                return instance;
            }
        }

        //GET LIST OF ORDERS
        public IEnumerable<Order> GetOrders()
        {
            IList<Order> orders = new List<Order>();
            try
            {
                using FStoreContext context = new FStoreContext();
                orders = context.Orders.ToList();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            return orders;
        }

        //GET ORDER BY IT ID
        public Order GetOrderById(int id)
        {
            Order? order = null;
            try
            {
                using FStoreContext context = new FStoreContext();
                order = context.Orders.FirstOrDefault(o => o.OrderId == id);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            return order;

        }

        //CREATE ORDER
        public void CreateOrder(Order order)
        {
            try
            {
                Order o = GetOrderById(order.OrderId);
                if (o == null)
                {
                    using FStoreContext context = new FStoreContext();
                    context.Add(order);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //GET ALL MEMBERS IDS FOR COMBOBOX
        public List<int> GetAllMemberIds()
        {
            List<int> memberIds = new List<int>();
            try
            {
                using FStoreContext context = new FStoreContext();

                memberIds = context.Members.Select(m => m.MemberId).ToList();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            return memberIds;
        }
        public void DeleteOrder(Order order)
        {
            try
            {
                using FStoreContext context = new FStoreContext();
                var foundOrder = context.Orders.SingleOrDefault(o => o.OrderId == order.OrderId);
                context.Orders.Remove(foundOrder);
                context.SaveChanges();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        public void UpdateOrder(Order order)
        {
            try
            {
                using FStoreContext context = new FStoreContext();
                context.Entry<Order>(order).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                //context.Orders.Update(order);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
        }
    }
}

