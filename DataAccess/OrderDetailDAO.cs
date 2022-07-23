using BusinessObject.DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class OrderDetailDAO
    {
        private static OrderDetailDAO? instance = null;
        private static readonly object _instanceLock = new object();
        //private OrderDao() { }
        private OrderDetailDAO()
        {

        }
        public static OrderDetailDAO Instance
        {
            get
            {
                lock (_instanceLock)
                {
                    if (instance == null)
                        instance = new OrderDetailDAO();
                }
                return instance;
            }
        }
        //GET ALL ORDER DETAILS
        public IEnumerable<OrderDetail> GetOrdersDetail(int id)
        {
            IList<OrderDetail> orders = new List<OrderDetail>();

            try
            {
                using FStoreContext context = new FStoreContext();
                orders = context.OrderDetails.Where(od => od.OrderId == id).Include(od => od.Product).ToList();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            return orders;
        }


        public IEnumerable<OrderDetail> GetOrdersDetail()
        {
            IList<OrderDetail> orders = new List<OrderDetail>();

            try
            {
                using FStoreContext context = new FStoreContext();
                orders = context.OrderDetails.ToList();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            return orders;
        }

        public void Update(OrderDetail orderDetail)
        {
            try
            {
                using FStoreContext _fStoreContext = new FStoreContext();
                _fStoreContext.Entry<OrderDetail>(orderDetail).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _fStoreContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //GET SINGLE DETAIL WITH orderID and productID
        public OrderDetail? GetSingleOrderDetail(int orderId, int productId)
        {
            OrderDetail? orderDetail = new OrderDetail();
            try
            {
                using FStoreContext context = new FStoreContext();
                orderDetail = context.OrderDetails.FirstOrDefault(od => od.OrderId == orderId && od.ProductId == productId);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            return orderDetail;
        }

        //CREATE 1 ORDER DETAIL
        public void InsertOrderDetail(OrderDetail orderDetail)
        {
            try
            {
                OrderDetail? od = GetSingleOrderDetail(orderDetail.OrderId, orderDetail.ProductId);
                if (od != orderDetail)
                {
                    using FStoreContext context = new FStoreContext();
                    context.Add(orderDetail);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {

            }
        }

        //DELETE 1 ORDER DETAIL
        public void DeleteOrderDetail(OrderDetail orderDetail)
        {
            try
            {
                using FStoreContext context = new FStoreContext();
                /*var foundOrder = context.OrderDetails.SingleOrDefault(o=>o.OrderId==orderDetail.OrderId);*/
                context.OrderDetails.Remove(orderDetail);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //GET ALL PRODUCT ID FROM PRODUCTS
        public List<int> GetAllOderDetailsProductId()
        {
            List<int> ids = new List<int>();
            try
            {
                using FStoreContext context = new FStoreContext();
                ids = context.Products.Select(p => p.ProductId).ToList();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            return ids;
        }

        //GET PRODUCT NAME WITH PRODUCT ID
        public string? GetProdctName(int productId)
        {
            string? productName = null;
            try
            {
                using FStoreContext context = new FStoreContext();
                productName = context.Products.SingleOrDefault(p => p.ProductId == productId).ProductName.ToString();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            return productName;
        }
        public IEnumerable<OrderDetail> GetOrderDetailList()
        {
            List<OrderDetail> orderDetails = new List<OrderDetail>();
            try
            {
                using FStoreContext context = new FStoreContext();
                orderDetails = context.OrderDetails.ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return orderDetails;
        }
        //public IEnumerable<SaleReport> CreateSatisticReport(DateTime startDate, DateTime endDate)
        //{
        //    IEnumerable<OrderDetail> orderDetails = OrderDetailDAO.Instance.GetOrderDetailList();
        //    IEnumerable<Order> orders = OrderDAO.Instance.GetOrders();

        //    var report =
        //        from ord in orderDetails
        //        group ord by ord.OrderId into g
        //        from or in orders
        //        where g.All(o => o.OrderId == or.OrderId) && or.OrderDate >= startDate && or.OrderDate <= endDate

        //        select new SaleReport
        //        {
        //            OrderId = or.OrderId,
        //            ProductName = "",
        //            OrderDate = or.OrderDate,
        //            ShippedDate = or.ShippedDate.Value,
        //            TotalSale = g.Sum(o => o.Quantity * o.UnitPrice)

        //        };
        //    return report;
        //}

    }
}

