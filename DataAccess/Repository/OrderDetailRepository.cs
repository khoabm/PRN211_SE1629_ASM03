using BusinessObject.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class OrderDetailRepository:IOrderDetailRepository
    {
        public void AddOrderDetails(OrderDetail orderDetail) => OrderDetailDAO.Instance.InsertOrderDetail(orderDetail);

        public void DeleteOrderDetails(OrderDetail orderDetail) => OrderDetailDAO.Instance.DeleteOrderDetail(orderDetail);


        public OrderDetail GetOrderDetailByIDByProductID(int v1, int v2) => OrderDetailDAO.Instance.GetSingleOrderDetail(v1, v2);

        public IEnumerable<OrderDetail> GetOrderDetails(int id) => OrderDetailDAO.Instance.GetOrdersDetail(id);

        public IEnumerable<OrderDetail> GetOrderDetails() => OrderDetailDAO.Instance.GetOrdersDetail();

        public List<int> GetProductIds() => OrderDetailDAO.Instance.GetAllOderDetailsProductId();

        public string? GetProductName(int productId) => OrderDetailDAO.Instance.GetProdctName(productId);

        //public IEnumerable<SaleReport> GetSaleReports(DateTime startDate, DateTime EndDate) => OrderDetailDAO.Instance.CreateSatisticReport(startDate, EndDate);


        public void Update(OrderDetail orderDetail) => OrderDetailDAO.Instance.Update(orderDetail);

    }
}
