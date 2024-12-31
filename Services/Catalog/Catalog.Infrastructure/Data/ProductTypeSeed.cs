using Catalog.Core.Entities;
using MongoDB.Driver;
using System.Text.Json;

namespace Catalog.Infrastructure.Data
{
    public static class ProductTypeSeed
    {
        public static void SeedData(IMongoCollection<ProductType> typeCollection) 
        {
            var checkTypes = typeCollection.Find(p => true).Any();
            //var path = Path.Combine("Data", "SeedData", "types.json");

            if (!checkTypes)
            {
                //var typeData = File.ReadAllText(path); 
                var typeData = File.ReadAllText("../Catalog.Infrastructure/Data/SeedData/types.json");
                var types = JsonSerializer.Deserialize<List<ProductType>>(typeData);

                if (types != null)
                {
                    foreach (var type in types)
                    {
                        typeCollection.InsertOneAsync(type);
                    }

                }
            }
        }
    }
}
