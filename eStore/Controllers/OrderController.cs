using BusinessObject.DataAccess;
using DataAccess.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eStore.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderRepository _repository;
        public OrderController(IOrderRepository repository)
        {
            _repository = repository;
        }

        // GET: OrderController
        public ActionResult Index()
        {
            if (HttpContext.Session.GetInt32("user") == null)
            {
                return RedirectToAction("Login", "Home");
            }
            int memberID = (int)HttpContext.Session.GetInt32("user");
            if (memberID != 0)
            {
                ViewBag.Error = "You don't have access to this action";
                return View();
            }
            ViewBag.user = HttpContext.Session.GetInt32("user");
            var orders = _repository.GetOrders();
            return View(orders);
        }

        // GET: OrderController/Details/5
        public ActionResult Details(int id )
        {
            try
            {
                Order order = _repository.GetOrderById(id);
                if(HttpContext.Session.GetInt32("user") == null)
                {
                    return RedirectToAction("Login", "Home");
                }
                int memberID = (int)HttpContext.Session.GetInt32("user");
                if(memberID != order.MemberId && memberID != 0)
                {
                    ViewBag.Error = " you dont have permission to view this order";
                    return View();
                }
                return View(order);
            }
            catch 
            {

            }
            return View();
        }

        // GET: OrderController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: OrderController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: OrderController/Edit/5
        public ActionResult Edit(int id = 0)
        {
            if (id == 0)
                return NotFound();

            var order = _repository.GetOrderById(id);

            if (order == null)
                return NotFound();
            return View(order);
        }

        // POST: OrderController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Order order)
        {
            try
            {
                if (id != order.OrderId)
                    return NotFound();
                if (ModelState.IsValid)
                {
                    _repository.UpdateOrder(order);
                }
                    return RedirectToAction(nameof(Index));         
            }
            catch(Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View(order);
            }
        }

        // GET: OrderController/Delete/5
        public ActionResult Delete(int id = 0)
        {
            if (id == 0)
                return NotFound();
            var order = _repository.GetOrderById(id);
            if (order == null)
                return NotFound();

            return View(order);
        }

        // POST: OrderController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public ActionResult DeletePost(int id)
        {
            try
            {
                _repository.DeleteOrder(_repository.GetOrderById(id));
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View();
            }
        }
        [HttpPost]
        public ActionResult Report(DateTime start,DateTime end)
        {
            try
            {
                Dictionary<Order, double> dict = _repository.GetOrdersByDate(start, end);
                ViewData["start"] = start.ToShortDateString();
                ViewData["end"] = end.ToShortDateString();
                return View(dict);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
