using ReclameAqui.Scrapper.Domain.Entities;

namespace ReclameAqui.Scrapper.Domain.Interfaces.Repository
{
    public interface IReclameAquiRepository
    {
        Task DeleteOne(int id);
        bool Exists(ReclameAquiEntity entity);
        Task InsertOne(ReclameAquiEntity entity);
        Task ReplaceOne(ReclameAquiEntity entity);
    }
}
