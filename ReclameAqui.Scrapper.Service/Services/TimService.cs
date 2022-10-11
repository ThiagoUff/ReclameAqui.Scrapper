using Newtonsoft.Json;
//using OpenQA.Selenium;
//using OpenQA.Selenium.Chrome;
using ReclameAqui.Scrapper.Domain.Entities;
using ReclameAqui.Scrapper.Domain.Interfaces.Repository;
using ReclameAqui.Scrapper.Domain.Interfaces.Services;
using ReclameAqui.Scrapper.Infra.Integration;
using System.Net;
using System.Text;

namespace ReclameAqui.Scrapper.Service.Services
{
    public class TimService : ITimService
    {
        private readonly ITimRepository _timRepository;
        private readonly IReclameAquiIntegration _reclameAquiIntegration;
        public TimService(ITimRepository timRepository,
                          IReclameAquiIntegration reclameAquiIntegration)
        {
            _timRepository = timRepository;
            _reclameAquiIntegration = reclameAquiIntegration;
        }

        public void Scrapper()
        {
            ProcessIntegration();
            //WebDriver driver = new ChromeDriver();

            //for (int i = 1; i < 50000; i++)
            //{
            //    driver.Navigate().GoToUrl($"https://www.reclameaqui.com.br/empresa/live-tim/lista-reclamacoes/?pagina={i}");
            //    var itens = driver.FindElements(By.ClassName("sc-1pe7b5t-0"));
            //    for (int j = 0; j < itens.Count; j++)
            //    {
            //        Console.WriteLine($"Iniciando o processamento da pagina {i}, item {j}");
            //        try
            //        {
            //            itens = driver.FindElements(By.ClassName("sc-1pe7b5t-0"));
            //            var link = itens.ElementAt(j).FindElement(By.CssSelector("a"));
            //            link.Click();
            //            TimEntity entity = ProcessPage(driver);

            //            if (_timRepository.Exists(entity))
            //                _timRepository.ReplaceOne(entity);
            //            else
            //                _timRepository.InsertOne(entity);
            //        }
            //        catch (Exception)
            //        {
            //            Console.WriteLine("Erro ao processar pagina");
            //        }
            //        try
            //        {
            //            driver.Navigate().GoToUrl($"https://www.reclameaqui.com.br/empresa/live-tim/lista-reclamacoes/?pagina={i}");
            //        }
            //        catch (Exception)
            //        {

            //            try
            //            {
            //                driver = new ChromeDriver();
            //                driver.Navigate().GoToUrl($"https://www.reclameaqui.com.br/empresa/live-tim/lista-reclamacoes/?pagina={i}");
            //            }
            //            catch (Exception)
            //            {
            //                driver = new ChromeDriver();
            //                driver.Navigate().GoToUrl($"https://www.reclameaqui.com.br/empresa/live-tim/lista-reclamacoes/?pagina={i}");
            //            }
            //        }

            //    }
            //}

        }


        public async void ProcessIntegration()
        {
            int limit = 5000;
            int total = 0;
            for (int i = 1; i < 50000; i++)
            {
                
                WebClient web1 = new WebClient();
                web1.Headers.Add("Accept", "text/html; charset=utf-8");
                string paginateString = web1.DownloadString($"https://www.reclameaqui.com.br/empresa/live-tim/lista-reclamacoes/?pagina={i}");
                ReclameAquiPagination paginate = ProcessPaginatedPage(paginateString);
                limit = paginate.count;
                web1.Dispose();
                int index = 0;
                foreach (var item in paginate.LAST)
                {
                    
                    web1 = new WebClient();
                    web1.Headers.Add("Accept", "text/html; charset=utf-8");
                    string title = item.title.ToLower().Replace(" ", "-")
                                                       .Replace(".", "")
                                                       .Replace(",", "")
                                                       .Replace(";", "")
                                                       .Replace(":", "")
                                                       .Replace("!", "")
                                                       .Replace("?", "")
                                                       .Replace("ç","c")
                                                       .Replace("ã","a")
                                                       .Replace("õ","o")
                                                       .Replace("á","a")
                                                       .Replace("é","e")
                                                       .Replace("í", "i")
                                                       .Replace("ó", "o")
                                                       .Replace("ú", "u")
                                                       .Replace("à", "a")
                                                       .Replace("è", "e")
                                                       .Replace("ì", "i")
                                                       .Replace("ò", "o")
                                                       .Replace("ù", "u")
                                                       .Replace("â", "a")
                                                       .Replace("ê", "e")
                                                       .Replace("î", "i")
                                                       .Replace("ô", "o")
                                                       .Replace("û", "u");
                    title = title.Last() == '-'? title.Substring(0, title.Length - 1) : title;
                    string url = $"https://www.reclameaqui.com.br/live-tim/{title}_{item.id}/";
                    Console.WriteLine($"Page: {i} - Item:{index++} - Total:{total++} - Url:{url}");
                    Thread.Sleep(1000);
                    string pageString = web1.DownloadString(url);
                    ReclameAquiEntity entity = ProcessPage(pageString);
                    if (_timRepository.Exists(entity))
                        _timRepository.ReplaceOne(entity);
                    else
                        _timRepository.InsertOne(entity);

                    web1.Dispose();
                }
            }
        }



        public ReclameAquiEntity ProcessPage(string page)
        {
            string json = page.Substring(page.LastIndexOf("</div>"));
            json = json.Substring(json.IndexOf("{\"complaint\""));
            json = json.Substring(0, json.IndexOf(",\"__N_SSP\":"));
            ReclameAquiEntity entity = JsonConvert.DeserializeObject<ReclameAquiEntity>(json);
            entity.Id = entity.complaint.id;
            return entity;
        }

        public ReclameAquiPagination ProcessPaginatedPage(string page)
        {
            string json = page.Substring(page.LastIndexOf("</div>"));
            json = json.Substring(json.IndexOf("{\"LAST\":"));
            json = json.Substring(0, json.IndexOf(",\"mediaKit\"")); ;

            return JsonConvert.DeserializeObject<ReclameAquiPagination>(json);
        }

        //public TimEntity ProcessPage(WebDriver driver)
        //{
        //    string titulo = driver.FindElement(By.ClassName("lzlu7c-3")).Text;

        //    string localizacao = driver.FindElement(By.ClassName("lzlu7c-7")).Text;
        //    string data = driver.FindElement(By.ClassName("lzlu7c-8")).Text;
        //    int id = Int32.Parse(driver.FindElement(By.ClassName("lzlu7c-9")).Text.Split("\r\n")[0].Substring(4));
        //    Console.WriteLine($"Realizando salvamento da reclamação - {titulo} - ID: {id}");


        //    string descricao = driver.FindElement(By.ClassName("lzlu7c-17")).Text;

        //    var headerCategories = driver.FindElement(By.ClassName("sc-1dmxdqs-0"));
        //    var categories = headerCategories.FindElements(By.ClassName("eYkobe"));

        //    var follows = driver.FindElements(By.ClassName("sc-1o3atjt-4"));
        //    var dataFollows = driver.FindElements(By.ClassName("sc-1o3atjt-3"));

        //    string? resposta = follows.Count == 0 ? null : follows.ElementAt(0).Text;
        //    string? dataResposta = dataFollows.Count == 0 ? null : dataFollows.ElementAt(0).Text;

        //    string? consideracao = follows.Count > 2 ? follows.ElementAt(1).Text : null;
        //    string? dataConsideracao = dataFollows.Count > 2 ? dataFollows.ElementAt(1).Text : null;

        //    string? isResolvido = driver.FindElements(By.ClassName("gfOjuF")).Count == 0 ? null : driver.FindElement(By.ClassName("gfOjuF")).Text;
        //    string? voltaria = driver.FindElements(By.ClassName("ezNHIi")).Count == 0 ? null : driver.FindElement(By.ClassName("ezNHIi")).Text;
        //    string? nota = driver.FindElements(By.ClassName("ceUcTc")).Count == 0 ? null : driver.FindElement(By.ClassName("ceUcTc")).Text;

        //    return new TimEntity()
        //    {
        //        Titulo = titulo,
        //        Categorias = categories.Select(c => c.Text),
        //        Consideracao = consideracao,
        //        Data = data,
        //        DataConsideracao = dataConsideracao,
        //        DataResposta = dataResposta,
        //        Descricao = descricao,
        //        Id = id,
        //        Localizacao = localizacao,
        //        Nota = nota is null ? null : Int32.Parse(nota),
        //        Resolvido = isResolvido is null ? null : isResolvido == "Resolvido",
        //        Resposta = resposta,
        //        VoltariaNegocio = voltaria is null ? null : voltaria == "Sim",
        //    };
        //}
    }
}
