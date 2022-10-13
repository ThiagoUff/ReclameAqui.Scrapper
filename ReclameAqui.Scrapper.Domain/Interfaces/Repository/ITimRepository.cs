using ReclameAqui.Scrapper.Domain.Entities;

namespace ReclameAqui.Scrapper.Domain.Interfaces.Repository
{
    public interface ITimRepository
    {
        Task DeleteOne(int id);
        bool Exists(TimEntity entity);
        Task<TimEntity> FindOne(int id);
        Task<IEnumerable<TimEntity>> GetAll();
        Task InsertOne(TimEntity entity);
        Task ReplaceOne(TimEntity entity);
    }
}
