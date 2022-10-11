using Refit;

namespace ReclameAqui.Scrapper.Infra.Integration
{
    public interface IReclameAquiIntegration
    {
        [Get("")]
        public Task<string> GetPage(int index);
    }
}
