using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBackgroundTask.Model
{
    public interface IProductRespository : IRespository<Product>
    {
        Product Add(Product product);
    }
}
