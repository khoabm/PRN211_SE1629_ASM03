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
            var products = _productRepository.GetProducts();
            if (!String.IsNullOrEmpty(searchString))
            {
                products = _productRepository.Search(searchString);
            }
            return View(products);
        }

        // GET: ProductController/Details/5
        public ActionResult Details(int? id)
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

        // GET: ProductController/Create
        public ActionResult Create()
        {
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
    }
}
