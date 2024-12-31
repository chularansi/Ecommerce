using Catalog.Core.Entities;
using Catalog.Core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Core.Contracts
{
    public interface IProductRepository
    {
        Task<Pagination<Product>> GetProducts(CatalogSpecificationsParams catalogSpecParams);
        //Task<IEnumerable<Product>> GetProducts();
        Task<Product> GetProduct(string id);
        Task<IEnumerable<Product>> GetProductsByName(string productName);
        Task<IEnumerable<Product>> GetProductsByBrand(string brandName);
        Task<Product> CreateProduct(Product product);
        Task<bool> UpdateProduct(Product product);
        Task<bool> DeleteProduct(string id);
    }
}
