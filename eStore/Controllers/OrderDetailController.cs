using BusinessObject.DataAccess;
using DataAccess.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eStore.Controllers
{
    public class OrderDetailController : Controller
    {
        private readonly IOrderDetailRepository _repository;
        public OrderDetailController(IOrderDetailRepository repository)
        {
            _repository = repository;
        }

        // GET: OrderDetailController
        public ActionResult Index()
        {
            var orderDetails = _repository.GetOrderDetails();
            return View(orderDetails);
        }
        public IActionResult OrderDetails(int id)
        {
            var orderDetails = _repository.GetOrderDetails(id);
            return View(orderDetails);
        }
        // GET: OrderDetailController/Details/5
        public ActionResult Details(int orderID = 0, int productId = 0)
        {
            if (orderID == 0 || productId == 0)
                return NotFound();
            var orderDetail = _repository.GetOrderDetailByIDByProductID(orderID, productId);
            if (orderDetail == null)
                return NotFound();
            return View(orderDetail);
        }

        // GET: OrderDetailController/Create
        public ActionResult Create(int id)
        {
            OrderDetail OrderDetail = new OrderDetail { OrderId = id };
            return View(OrderDetail);
        }

        // POST: OrderDetailController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(include: "OrderId, ProductId, UnitPrice, Quantity, Discount")] OrderDetail orderDetail)
        {
            try
            {
                if (ModelState.IsValid)
                    _repository.AddOrderDetails(orderDetail);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View(orderDetail);
            }
        }

        // GET: OrderDetailController/Edit/5
        public ActionResult Edit(int orderId = 0, int productId = 0)
        {
            if (orderId == 0 || productId == 0)
                return NotFound();
            var orderDetail = _repository.GetOrderDetailByIDByProductID(orderId, productId);
            if (orderDetail == null)
                return NotFound();
            return View(orderDetail);

        }

        // POST: OrderDetailController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(OrderDetail orderDetail,int orderId = 0, int productId = 0)
        {
            try
            {
                if (orderId == 0 || productId == 0)
                    return NotFound();
                if (orderId != orderDetail.OrderId || productId != orderDetail.ProductId)
                    return NotFound();
                if (ModelState.IsValid)
                {
                    _repository.Update(orderDetail);
                }
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View(orderDetail);
            }
        }

        // GET: OrderDetailController/Delete/5
        [HttpGet]
        public ActionResult Delete(int id,int productID)
        {
            var OrderDetail = _repository.GetOrderDetailByIDByProductID(id, productID);
            if (OrderDetail != null)
                return View(OrderDetail);
            else return NotFound();
        }

        // POST: OrderDetailController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                var id1 = int.Parse(Request.Form["OrderId"]);
                var productId = int.Parse(Request.Form["ProductId"]);
                _repository.DeleteOrderDetails(_repository.GetOrderDetailByIDByProductID(id1,productId));
                return RedirectToAction(nameof(OrderDetails), new { id = id });
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View(nameof(OrderDetails), new { id = id });
            }
        }
    }
}
