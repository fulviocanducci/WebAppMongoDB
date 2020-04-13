using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using WebAppMongoDB.Models;

namespace WebAppMongoDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        public IMongoCollection<Person> MongoCollectionPerson { get; }

        public PersonController(IMongoCollection<Person> mongoCollectionPerson)
        {            
            MongoCollectionPerson = mongoCollectionPerson;            
        }

        // GET: api/Person
        [HttpGet]
        public IEnumerable<Person> GetPerson()
        {
            return MongoCollectionPerson.Find(new BsonDocument()).ToEnumerable();
        }

        [HttpGet("phones")]
        public IEnumerable GetPersonWithPhone([FromServices] IMongoDatabase mongoDatabase)
        {
            BsonElement[] elements = new BsonElement[]
            {
                new BsonElement("from", "phone"),
                new BsonElement("localField", "_id"),
                new BsonElement("foreignField", "personId"),
                new BsonElement("as", "phones")
            };
            var pipeline = new BsonDocument[] {
                new BsonDocument("$lookup", new BsonDocument(elements.AsEnumerable()))
            };
            return mongoDatabase
                .GetCollection<Person>("person")
                .Aggregate<BsonDocument>(pipeline)
                .ToList()
                .Select(c => c.ToDictionary());
        }

        // GET: api/Person/5
        [HttpGet("{id}", Name = "GetPerson")]
        public Person GetPerson(string id)
        {
            return MongoCollectionPerson.Find(x => x.Id == id).FirstOrDefault();
        }

        // POST: api/Person
        [HttpPost]
        public IActionResult PostPerson([FromBody] Person value)
        {
            MongoCollectionPerson.InsertOne(value);
            return Ok(value);
        }

        // PUT: api/Person/5
        [HttpPut("{id}")]
        public IActionResult PutPerson(string id, [FromBody] Person value)
        {            
            ReplaceOneResult result = MongoCollectionPerson.ReplaceOne(x => x.Id == id, value);
            return Ok(result);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult DeletePerson(string id)
        {
            DeleteResult result = MongoCollectionPerson.DeleteOne(x => x.Id == id);
            return Ok(result);
        }
    }
}
