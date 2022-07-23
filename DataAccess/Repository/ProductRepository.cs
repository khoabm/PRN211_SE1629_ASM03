using BusinessObject.DataAccess;

namespace DataAccess.Repository
{
    public class ProductRepository : IProductRepository
    {
        public void Create(Product product) => ProductDAO.Instance.CreateProduct(product);

        public void Delete(int id) => ProductDAO.Instance.DeleteProduct(id);

        public Product GetById(int id) => ProductDAO.Instance.GetById(id);

        public List<Product> GetProducts() => ProductDAO.Instance.GetProducts();

        public List<Product> Search(string searchText) => ProductDAO.Instance.Search(searchText);

        public void Update(Product product) => ProductDAO.Instance.UpdateProduct(product);
    }
}
