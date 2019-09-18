using DapperInCoreLib.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DapperInCoreLib.Repository
{
    public interface IProductRepository
    {
        List<Product> GetList();
        Product GetProduct(int id);
            
    }
}
