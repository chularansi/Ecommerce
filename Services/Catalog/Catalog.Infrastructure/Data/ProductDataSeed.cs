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
    public static class ProductDataSeed
    {
        public static void SeedData(IMongoCollection<Product> productCollection)
        {
            var checkProducts = productCollection.Find(p => true).Any();
            //var path = Path.Combine("Data", "SeedData", "products.json");

            if (!checkProducts)
            {
                //var productData = File.ReadAllText(path); 
                var productData = File.ReadAllText("../Catalog.Infrastructure/Data/SeedData/products.json");
                var products = JsonSerializer.Deserialize<List<Product>>(productData);

                if (products != null)
                {
                    foreach (var product in products)
                    {
                        productCollection.InsertOneAsync(product);
                    }

                }
            }
        }
    }
}
