using BusinessObject.DataAccess;

namespace DataAccess
{
    public sealed class ProductDAO
    {
        private static ProductDAO? instance = null;
        private static readonly object instanceLock = new object();
        private ProductDAO() { }
        public static ProductDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new ProductDAO();
                    }
                    return instance;
                }
            }
        }
        public List<Product> GetProducts()
        {
            List<Product> products = new List<Product>();
            try
            {
                using FStoreContext _fStoreContext = new FStoreContext();
                products = _fStoreContext.Products.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return products;
        }
        public Product GetById(int id)
        {
            var product = new Product();
            try
            {
                using FStoreContext _fStoreContext = new FStoreContext();
                product = _fStoreContext.Products.FirstOrDefault(p => p.ProductId == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return product;
        }
        public void CreateProduct(Product product)
        {
            try
            {
                using FStoreContext _fStoreContext = new FStoreContext();
                _fStoreContext.Products.Add(product);
                _fStoreContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void UpdateProduct(Product product)
        {
            try
            {
                using FStoreContext _fStoreContext = new FStoreContext();
                _fStoreContext.Entry<Product>(product).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _fStoreContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void DeleteProduct(int id)
        {
            try
            {
                using FStoreContext _fStoreContext = new FStoreContext();
                var searchProduct = _fStoreContext.Products.FirstOrDefault(p => p.ProductId == id);
                _fStoreContext.Products.Remove(searchProduct);
                _fStoreContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<Product> Search(string searchText)
        {
            try
            {
                using FStoreContext _fStoreContext = new FStoreContext();
                var searchedProducts = _fStoreContext.Products.Where(
                    x => x.ProductName.Contains(searchText) ||
                    x.UnitPrice.ToString().Contains(searchText));
                return searchedProducts.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
