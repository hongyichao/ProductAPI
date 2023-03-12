using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ProductRepositories.MongoDB.DataModels
{
    public class Product
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("NameId")]
        public string NameId { get; set; }

        [BsonElement("Name")]
        public string Name { get; set; }

        [BsonElement("Group")]
        public string Group { get; set; }

        [BsonElement("Price")]
        public decimal Price { get; set; }

        [BsonElement("Description")]
        public String Description { get; set; }

        [BsonElement("CreatedTimestamp")]
        public DateTimeOffset CreatedTimestamp { get; set; }

        [BsonElement("UpdatedTimestamp")]
        public DateTimeOffset UpdatedTimestamp { get; set; }
    }
}
