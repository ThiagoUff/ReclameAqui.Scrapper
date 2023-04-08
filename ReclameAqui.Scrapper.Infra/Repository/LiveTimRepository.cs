using MongoDB.Bson;
using MongoDB.Driver;
using ReclameAqui.Scrapper.Domain.Entities;
using ReclameAqui.Scrapper.Domain.Interfaces.Repository;

namespace ReclameAqui.Scrapper.Infra.Repository
{
    public class LiveTimRepository : ILiveTimRepository
    {
        private readonly IMongoCollection<ReclameAquiEntity> _db;
        public LiveTimRepository()
        {
            MongoClient client = new ("mongodb+srv://usr_master:usr_master@cluster0.ay4a5.mongodb.net/test");
            IMongoDatabase database = client.GetDatabase("ReclameAqui");
            _db = database.GetCollection<ReclameAquiEntity>("Tim");
        }

        public async Task InsertOne(ReclameAquiEntity entity) 
        {
            await _db.InsertOneAsync(entity);
        }
        public async Task<ReclameAquiEntity> FindOne(int id)
        {
            return await _db.Find(Builders<ReclameAquiEntity>.Filter.Eq("_id", id)).FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<ReclameAquiEntity>> GetAll()
        {
            return (await _db.FindAsync(new BsonDocument())).ToEnumerable();
        }
        public bool Exists(ReclameAquiEntity entity)
        {
            return _db.Find(Builders<ReclameAquiEntity>.Filter.Eq("_id", entity.Id)).Any();
        }

        public async Task DeleteOne(int id) 
        {
            await _db.DeleteOneAsync(Builders<ReclameAquiEntity>.Filter.Eq("_id", id));
        }

        public async Task ReplaceOne(ReclameAquiEntity entity)
        {
            await _db.ReplaceOneAsync(Builders<ReclameAquiEntity>.Filter.Eq("_id", entity.Id), entity);
        }
    }
}
