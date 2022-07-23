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
            return View();
        }
        public IActionResult OrderDetails(int id)
        {
            var orderDetails = _repository.GetOrderDetails(id);
            return View(orderDetails);
        }
        // GET: OrderDetailController/Details/5
        public ActionResult Details(int id,int productID)
        {
            var orderDetails = _repository.GetOrderDetailByIDByProductID(id, productID);
            if (orderDetails==null)
                return NotFound();
            else return View(orderDetails);
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
        public ActionResult Create(OrderDetail orderDetail)
        {
            try
            {
                int OrderId = int.Parse(Request.Form["OrderId"]);
                orderDetail.OrderId = OrderId;
                _repository.AddOrderDetails(orderDetail);
                return RedirectToAction(nameof(OrderDetails),new {id = orderDetail.OrderId});
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View();
            }
        }

        // GET: OrderDetailController/Edit/5
        public ActionResult Edit(int id,int productID)
        {
            var OrderDetail = _repository.GetOrderDetailByIDByProductID(id, productID);
            if (OrderDetail != null)
                return View(OrderDetail);
            else return NotFound();
        }

        // POST: OrderDetailController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, OrderDetail orderDetails)
        {
            try
            {
                if (id == orderDetails.OrderId)
                    _repository.Update(orderDetails);
                else return NotFound();
                return RedirectToAction(nameof(OrderDetails),new {id=id});
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View();
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
