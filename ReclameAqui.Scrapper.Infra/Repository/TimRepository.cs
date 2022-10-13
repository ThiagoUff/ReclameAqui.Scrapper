using MongoDB.Bson;
using MongoDB.Driver;
using ReclameAqui.Scrapper.Domain.Entities;
using ReclameAqui.Scrapper.Domain.Interfaces.Repository;

namespace ReclameAqui.Scrapper.Infra.Repository
{
    public class TimRepository : ITimRepository
    {
        private readonly IMongoCollection<TimEntity> _db;
        public TimRepository()
        {
            MongoClient client = new ("mongodb://localhost:27017");
            IMongoDatabase database = client.GetDatabase("ReclameAqui");
            _db = database.GetCollection<TimEntity>("Tim");
        }

        public async Task InsertOne(TimEntity entity) 
        {
            await _db.InsertOneAsync(entity);
        }
        public async Task<TimEntity> FindOne(int id)
        {
            return await _db.Find(Builders<TimEntity>.Filter.Eq("_id", id)).FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<TimEntity>> GetAll()
        {
            return (await _db.FindAsync(new BsonDocument())).ToEnumerable();
        }
        public bool Exists(TimEntity entity)
        {
            return _db.Find(Builders<TimEntity>.Filter.Eq("_id", entity.Id)).Any();
        }

        public async Task DeleteOne(int id) 
        {
            await _db.DeleteOneAsync(Builders<TimEntity>.Filter.Eq("_id", id));
        }

        public async Task ReplaceOne(TimEntity entity)
        {
            await _db.ReplaceOneAsync(Builders<TimEntity>.Filter.Eq("_id", entity.Id), entity);
        }
    }
}
