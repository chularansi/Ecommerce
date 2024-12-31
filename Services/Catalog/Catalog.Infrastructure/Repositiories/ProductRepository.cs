using Catalog.Core.Contracts;
using Catalog.Core.Entities;
using Catalog.Core.Specifications;
using Catalog.Infrastructure.Data;
using MongoDB.Driver;

namespace Catalog.Infrastructure.Repositiories
{
    public class ProductRepository : IProductRepository, IBrandRepository, ITypeRepository
    {
        private readonly ICatalogContext context;

        public ProductRepository(ICatalogContext context)
        {
            this.context = context;
        }

        async Task<Product> IProductRepository.GetProduct(string id)
        {
            return await context
                .Products
                .Find(p => p.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<Pagination<Product>> GetProducts(CatalogSpecificationsParams catalogSpecsParams)
        {
            var builder = Builders<Product>.Filter;
            var filter = builder.Empty;

            if (!string.IsNullOrEmpty(catalogSpecsParams.Search))
            {
                filter = filter & builder.Where(p => p.Name.ToLower().Contains(catalogSpecsParams.Search.ToLower()));
            }
            if (!string.IsNullOrEmpty(catalogSpecsParams.BrandId))
            {
                filter &= builder.Eq(p => p.Brands.Id, catalogSpecsParams.BrandId);
            }
            if (!string.IsNullOrEmpty(catalogSpecsParams.TypeId))
            {
                filter &= builder.Eq(p => p.Types.Id, catalogSpecsParams.TypeId);
            }

            var totalCount = await context.Products.CountDocumentsAsync(filter);
            var data = await DataFilter(catalogSpecsParams, filter);
            
            return new Pagination<Product>(catalogSpecsParams.PageIndex, catalogSpecsParams.PageSize, (int)totalCount, data);
        }

        async Task<IEnumerable<Product>> IProductRepository.GetProductsByName(string productName)
        {
            return await context
                .Products
                .Find(p => p.Name == productName)
                .ToListAsync();
        }

        async Task<IEnumerable<Product>> IProductRepository.GetProductsByBrand(string brandName)
        {
            return await context
                .Products
                .Find(p => p.Brands.Name == brandName)
                .ToListAsync();
        }

        async Task<Product> IProductRepository.CreateProduct(Product product)
        {
            await context
                .Products
                .InsertOneAsync(product);

            return product;
        }

        async Task<bool> IProductRepository.UpdateProduct(Product product)
        {
            var updatedProduct = await context
                .Products
                .ReplaceOneAsync(p => p.Id == product.Id, product);

            return updatedProduct.IsAcknowledged && updatedProduct.ModifiedCount > 0;
        }

        async Task<bool> IProductRepository.DeleteProduct(string id)
        {
            var deletedProduct = await context
                .Products
                .DeleteOneAsync(p => p.Id == id);

            return deletedProduct.IsAcknowledged && deletedProduct.DeletedCount > 0;
        }

        async Task<IEnumerable<ProductBrand>> IBrandRepository.GetBrands()
        {
            return await context
                .Brands
                .Find(b => true)
                .ToListAsync();
        }

        async Task<IEnumerable<ProductType>> ITypeRepository.GetTypes()
        {
            return await context
                .Types
                .Find(t => true)
                .ToListAsync();
        }

        private async Task<IReadOnlyList<Product>> DataFilter(CatalogSpecificationsParams catalogSpecsParams, FilterDefinition<Product> filter)
        {
            var builder = Builders<Product>.Sort;
            var sortDefinition = builder.Ascending("Name");

            if (!string.IsNullOrEmpty(catalogSpecsParams.Sort))
            {
                switch (catalogSpecsParams.Sort)
                {
                    case "priceAsc":
                        sortDefinition = builder.Ascending("Price");
                        break;
                    case "priceDesc":
                        sortDefinition = builder.Descending("Price");
                        break;
                    default:
                        sortDefinition = builder.Ascending("Name");
                        break;
                }
            }

            var data = await context.Products
                .Find(filter)
                .Sort(sortDefinition)
                .Skip((catalogSpecsParams.PageIndex - 1) * catalogSpecsParams.PageSize)
                .Limit(catalogSpecsParams.PageSize)
                .ToListAsync();

            return data;
        }
    }
}
