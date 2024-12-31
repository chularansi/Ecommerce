using Catalog.Core.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Catalog.Infrastructure.Data
{
    public static class ProductBrandSeed
    {
        public static void SeedData(IMongoCollection<ProductBrand> brandCollection)
        {
            var checkBrands = brandCollection.Find(p => true).Any();
            //var path = Path.Combine("Data", "SeedData", "brands.json");

            if (!checkBrands)
            {
                //var brandData = File.ReadAllText(path);
                var brandData = File.ReadAllText("../Catalog.Infrastructure/Data/SeedData/brands.json");
                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandData);

                if (brands != null)
                {
                    foreach (var brand in brands)
                    {
                        brandCollection.InsertOneAsync(brand);
                    }
                }
            }
        }
    }
}
