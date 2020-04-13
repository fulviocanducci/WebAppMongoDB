using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
namespace WebAppMongoDB.Models
{
    public class Phone
    {
        [BsonId(IdGenerator = typeof(StringObjectIdGenerator))]
        public string Id { get; set; }
                
        [BsonRequired()]
        [BsonElement("personId")]
        public string PersonId { get; set; }

        [BsonElement("ddd")]
        [BsonRequired()]
        public string Ddd { get; set; }

        [BsonElement("number")]
        [BsonRequired()]
        public string Number { get; set; }
    }
}