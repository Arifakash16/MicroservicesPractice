using MongoDB.Bson.Serialization.Attributes;

namespace Catalog.API.Models
{
    public class Product
    {
        [BsonId]
        public string Id { set; get; }
        public string Name { set; get; }
        public string Description { set; get; }
        public string Category { set; get; }
        public string Summary { set; get; }
        public string ImageFile { set; get; }
        public decimal Price { set; get; }
    }
}
