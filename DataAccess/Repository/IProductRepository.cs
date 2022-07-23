using BusinessObject.DataAccess;

namespace DataAccess.Repository
{
    public interface IProductRepository
    {
        List<Product> GetProducts();
        Product GetById(int id);
        void Update(Product product);
        void Create(Product product);
        void Delete(int id);
        List<Product> Search(string searchText);
    }
}
