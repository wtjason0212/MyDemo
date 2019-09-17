using MyBackgroundTask.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBackgroundTask.Repositories
{
    public class ProductRepository : IProductRespository
    {
        private readonly ProductContext _context;

        public ProductRepository(ProductContext context)
        {
            _context = context;
        }

       // public IUnitOfWork UnitOfWork => _context;

        public Product Add(Product product)
        {
            var result = new Product() { Id = 66 };
            _context.Add(result);
            _context.SaveChanges();
            return result;
        }
    }
}
