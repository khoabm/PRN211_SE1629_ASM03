using BusinessObject.DataAccess;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject;
using System.Data;

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
        public IEnumerable<Order>? GetOrderByMemberId(int id)
        {
            IEnumerable<Order>? order = null;
            try
            {
                using FStoreContext context = new FStoreContext();
                order = context.Orders.Where(o => o.MemberId == id);
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
        public Dictionary<Order, double> GetOrdersByDate(DateTime start, DateTime end)
        {
            Dictionary<Order, double> dict = new Dictionary<Order, double>();
            try
            {
                using FStoreContext context = new FStoreContext();
                SqlConnection cnn = (SqlConnection)context.Database.GetDbConnection();
                string SQL = "SELECT [Orders].*, x.Total FROM\n"
                    + "(SELECT DISTINCT OrderId, SUM(UnitPrice * Quantity * (1 - Discount)) as Total\n"
                    + "FROM OrderDetails WHERE OrderDetails.OrderId IN (\n"
                    + "SELECT OrderId FROM [Orders] WHERE OrderDate Between @StartDate AND @EndDate)\n"
                    + "GROUP BY OrderId) as x, [Orders]\n"
                    + "WHERE [Orders].OrderId = x.OrderId\n" +
                    "ORDER BY x.Total DESC";
                SqlCommand cmd = new SqlCommand(SQL, cnn);
                cmd.Parameters.AddWithValue("@StartDate", start);
                cmd.Parameters.AddWithValue("@EndDate", end);
                if (cnn.State == ConnectionState.Closed)
                    cnn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int _OrderID = reader.GetInt32(0);
                        int _MemberID = reader.GetInt32(1);
                        DateTime _OrderDate = reader.GetDateTime(2);
                        DateTime _RequiredDate = reader.GetDateTime(3);
                        DateTime _ShippedDate = reader.GetDateTime(4);
                        decimal _Freight = reader.GetDecimal(5);
                        double _TotalPrice = reader.GetDouble(6);
                        Order order = new Order
                        {
                            OrderId = _OrderID,
                            MemberId = _MemberID,
                            OrderDate = _OrderDate,
                            RequiredDate = _RequiredDate,
                            ShippedDate = _ShippedDate,
                            Freight = _Freight
                        };
                        dict.Add(order, _TotalPrice);
                    }
                    reader.NextResult();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return dict;
        }

    }
}

