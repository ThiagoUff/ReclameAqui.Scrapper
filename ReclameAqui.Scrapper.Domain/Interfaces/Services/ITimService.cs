namespace ReclameAqui.Scrapper.Domain.Interfaces.Services
{
    public interface ITimService
    {
        Task ExtractInfo();
        Task ScrapperLiveTim();
        Task ScrapperTimCelular();
    }
}
