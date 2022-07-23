using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BusinessObject.DataAccess;
using DataAccess.Repository;
using System.Data;

namespace eStore.Controllers
{
    public class ProductController : Controller
    {
        IProductRepository _productRepository;

        public ProductController()
        {
            _productRepository = new ProductRepository();
        }

        // GET: ProductController
        public ActionResult Index(string searchString)
        {
            object session = HttpContext.Session.GetInt32("user");
            if (session == null)
                return RedirectToAction("Login", "Home");
            else
            {
                int memberID = (int)session;
                if (memberID != 0)
                {
                    ViewBag.Error = "You don't have access to this action";
                    return View();
                }
            }

            try
            {
                List<Product> lst = (List<Product>)_productRepository.GetProducts();
                return View(lst);
            }
            catch
            {
                ViewBag.Error = "Error loading products";
                return RedirectToAction("Index", "Home");
            }
        }

        // GET: ProductController/Details/5
        public ActionResult Details(int? id)
        {
            int login = CheckLogin();
            if (login == -1)
                return RedirectToAction("Login", "Home");
            if (login == 0)
            {
                ViewBag.Error = "You don't have access to this action";
                return View();
            }
            try
            {
                if (id == null)
                    return NotFound();
                Product pro = _productRepository.GetById(id.Value);
                return View(pro);
            }
            catch
            {
                ViewBag.Error = "Error loading product";
                return View();
            }
        }

        // GET: ProductController/Create
        public ActionResult Create()
        {
            var session = HttpContext.Session.GetInt32("user");
            if (session == null)
                return RedirectToAction("Login", "Home");
            if (session != 0)
                return RedirectToAction("Index", "Home");
            int maxID = _productRepository.GetProducts().Max(pro => pro.ProductId) + 1;
            ViewData["proID"] = maxID;
            return View();
        }

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(include: "CategoryId, ProductName, Weight, UnitPrice, UnitsInStock")]Product product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _productRepository.Create(product);
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return View(product);
        }

        // GET: ProductController/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var product = _productRepository.GetById(id.Value);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Product product)
        {
            if (id != product.ProductId)
            {
                return NotFound();
            }
            try
            {
                if (ModelState.IsValid)
                {
                    _productRepository.Update(product);
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return View(product);
        }

        // GET: ProductController/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var product = _productRepository.GetById(id.Value);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: ProductController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                _productRepository.Delete(id);
                return RedirectToAction("Index");
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return View();
        }
        public int CheckLogin()
        {
            var session = HttpContext.Session.GetInt32("user");
            if (session == null)
                return -1;
            if (session != 0)
                return 0;
            return 1;
        }
    }
}
