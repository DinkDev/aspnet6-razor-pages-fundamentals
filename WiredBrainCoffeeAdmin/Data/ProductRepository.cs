namespace WiredBrainCoffeeAdmin.Data;

using Microsoft.EntityFrameworkCore;

public class ProductRepository : IProductRepository
{
    private readonly WiredContext _context;

    public ProductRepository(WiredContext context)
    {
        _context = context;
    }

    public List<Product> GetAll()
    {
        return _context.Products.ToList();
    }

    public Product GetById(int id)
    {
        return _context.Products.FirstOrDefault(x => x.Id == id);
    }

    public void Add(Product product)
    {
        _context.Products.Add(product);
        _context.SaveChanges();
    }

    public void Update(Product product)
    {
        _context.Entry(product).State = EntityState.Modified;
        _context.SaveChanges();
    }

    public void Delete(int id)
    {
        var deletedProduct  = _context.Products.First(x => x.Id == id);
        _context.Products.Remove(deletedProduct);
        _context.SaveChanges();
    }
}