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
    public class PhoneController : ControllerBase
    {
        public IMongoCollection<Phone> MongoCollectionPhone { get; }

        public PhoneController(IMongoCollection<Phone> mongoCollectionPhone)
        {
            MongoCollectionPhone = mongoCollectionPhone;
        }

        // GET: api/Phone
        [HttpGet]
        public IEnumerable<Phone> GetPhone()
        {
            return MongoCollectionPhone.Find(new BsonDocument()).ToEnumerable();
        }

        // GET: api/Phone/5
        [HttpGet("{id}", Name = "GetPhone")]
        public Phone GetPhone(string id)
        {
            return MongoCollectionPhone.Find(x => x.Id == id).FirstOrDefault();
        }

        // POST: api/Phone
        [HttpPost]
        public IActionResult PostPhone([FromBody] Phone value)
        {
            MongoCollectionPhone.InsertOne(value);
            return Ok(value);
        }

        // PUT: api/Phone/5
        [HttpPut("{id}")]
        public IActionResult PutPhone(string id, [FromBody] Phone value)
        {
            ReplaceOneResult result = MongoCollectionPhone.ReplaceOne(x => x.Id == id, value);
            return Ok(result);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult DeletePhone(string id)
        {
            DeleteResult result = MongoCollectionPhone.DeleteOne(x => x.Id == id);
            return Ok(result);
        }
    }
}
