using DapperInCoreLib.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DapperInCoreLib.Repository
{
    public class ProductRepository : IProductRepository
    {
        private IConfiguration _configuration;
        private DapperFactory _factory;
        private IDapper _dapper;
        //public ProductRepository(IConfiguration configuration,IDapper dapper)
        public ProductRepository(IConfiguration configuration )
        {
            _configuration = configuration;
            //_dapper = dapper;
            _factory = DapperFactory.GetInstance(configuration);
            _dapper = _factory.GetDapper();
        }

        public List<Product> GetList()
        {
            string sqlString = "select * from Product";
            return _dapper.GetList<Product>(sqlString);
        }

        public Product GetProduct(int id)
        {
            return GetList().FirstOrDefault(o=>o.Id == id);
        }
    }
}
