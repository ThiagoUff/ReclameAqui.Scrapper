using Newtonsoft.Json;
using ReclameAqui.Scrapper.Domain.Entities;
using ReclameAqui.Scrapper.Domain.Interfaces.Repository;
using ReclameAqui.Scrapper.Domain.Interfaces.Services;
using System.Net;
using ReclameAqui.Scrapper.Service.Utils;
using System.Diagnostics;
using System.Text;
using ClosedXML.Excel;
using ReclameAqui.Scrapper.Domain.Enum;
using DocumentFormat.OpenXml.Spreadsheet;
using System.Linq;

namespace ReclameAqui.Scrapper.Service.Services
{
    public class TimService : ITimService
    {
        private readonly ITimRepository _timRepository;
        private readonly IReclameAquiRepository _reclameAquiRepository;
        public TimService(ITimRepository timRepository,
                          IReclameAquiRepository reclameAquiRepository)
        {
            _timRepository = timRepository;
            _reclameAquiRepository = reclameAquiRepository;
        }

        public async Task ExtractInfo()
        {
            IEnumerable<TimEntity> listaRA = await _timRepository.GetAll();

            IXLWorkbook workbook = new XLWorkbook();
            IXLWorksheet worksheet = workbook.Worksheets.Add("data");

            worksheet.Cell(1, 1).Value = "Id";
            worksheet.Cell(1, 2).Value = "Titulo";
            worksheet.Cell(1, 3).Value = "Data";
            worksheet.Cell(1, 4).Value = "Localizacao";
            worksheet.Cell(1, 5).Value = "Categoria";
            worksheet.Cell(1, 6).Value = "Problema";
            worksheet.Cell(1, 7).Value = "Produto";
            worksheet.Cell(1, 8).Value = "Descricao";
            worksheet.Cell(1, 9).Value = "Status";
            worksheet.Cell(1, 10).Value = "Resolvido";
            worksheet.Cell(1, 11).Value = "Avaliada";
            worksheet.Cell(1, 12).Value = "VoltariaNegocio";
            worksheet.Cell(1, 13).Value = "Nota";

            var csv = new StringBuilder();
            csv.AppendLine($"Id,Titulo,Data,Localizacao,Categoria,Problema,Produto,Descricao,Status,Resolvido,Avaliada,VoltariaNegocio,Nota");

            int index = 2;
            foreach (TimEntity item in listaRA)
            {
                worksheet.Cell(index, 1).Value = $"{item.Id}";
                worksheet.Cell(index, 2).Value = $"{item.Titulo}";
                worksheet.Cell(index, 3).Value = $"{item.Data}";
                worksheet.Cell(index, 4).Value = $"{item.Localizacao}";
                worksheet.Cell(index, 5).Value = $"{item.Categoria}";
                worksheet.Cell(index, 6).Value = $"{item.Problema}";
                worksheet.Cell(index, 7).Value = $"{item.Produto}";
                worksheet.Cell(index, 8).Value = $"{item.Descricao}";
                worksheet.Cell(index, 9).Value = $"{item.Status}";
                worksheet.Cell(index, 10).Value = $"{item.Resolvido}";
                worksheet.Cell(index, 11).Value = $"{item.Avaliada}";
                worksheet.Cell(index, 12).Value = $"{item.VoltariaNegocio}";
                worksheet.Cell(index, 13).Value = $"{item.Nota}";
                index++;

                csv.AppendLine($"{item.Id},{item.Titulo},{item.Data},{item.Localizacao},{item.Categoria},{item.Problema},{item.Produto},{item.Descricao},{item.Status},{item.Resolvido},{item.Avaliada},{item.VoltariaNegocio},{item.Nota}");
            }
            File.WriteAllText(@"..//dataset.csv", csv.ToString());
            workbook.SaveAs(@"dataset.xlsx");
        }

        public async Task Scrapper()
        {


            //var xpto = await ProcessPaginatedPage("https://www.reclameaqui.com.br/empresa/live-tim/lista-reclamacoes/?pagina=1&status=NOT_ANSWERED&categoria=000000000000006&produto=0000000000000476&problema=0000000000000078");
            IEnumerable<ProblemaEnum> problemas = Enum.GetValues(typeof(ProblemaEnum)).Cast<ProblemaEnum>();
            IEnumerable<CategoriaEnum> categorias = Enum.GetValues(typeof(CategoriaEnum)).Cast<CategoriaEnum>();
            IEnumerable<ProdutosEnum> produtos = Enum.GetValues(typeof(ProdutosEnum)).Cast<ProdutosEnum>();
            IEnumerable<StatusEnum> statusList = Enum.GetValues(typeof(StatusEnum)).Cast<StatusEnum>();

            //await ProcessLink("EVALUATED", "", "", "");
            foreach (var status in statusList)
            {

                foreach (var problema in problemas)
                {
                    foreach (var categoria in categorias)
                    {
                        //Parallel.ForEach(produtos, new ParallelOptions() { MaxDegreeOfParallelism = 2 }, async produto =>
                        //{
                        //    await ProcessLink(status.GetEnumDescription(), problema.GetEnumDescription(), categoria.GetEnumDescription(), produto.GetEnumDescription());
                        //});
                        foreach (var produto in produtos)
                        {
                            await ProcessLink(status.GetEnumDescription(), problema.GetEnumDescription(), categoria.GetEnumDescription(), produto.GetEnumDescription());
                        }
                    }
                }
            }

        }

        private async Task ProcessLink(string status, string problema, string categoria, string produto)
        {
            List<string> idList = new();
            int limit = 100000;
            IEnumerable<ReclameAquiPagination> items;

            for (int i = 1; i < limit; i += 10)
            {
                ReclameAquiPagination paginate;
                try
                {
                    Stopwatch sw = Stopwatch.StartNew();
                    string stat = status == "" ? "" : $"status={status}";
                    string cat = categoria == "" ? "" : $"categoria={categoria}";
                    string prod = produto == "" ? "" : $"produto={produto}";
                    string prob = problema == "" ? "" : $"problema={problema}";
                    //string url = $"https://www.reclameaqui.com.br/empresa/live-tim/lista-reclamacoes/?pagina={i}&status={status}&categoria={categoria}&produto={produto}&problema={problema}";
                    string url = $"https://www.reclameaqui.com.br/empresa/live-tim/lista-reclamacoes/?pagina={i}&{stat}&{cat}&{prod}&{prob}";
                    items = await ProcessPaginatedPage(url);
                    sw.Stop();
                    Console.WriteLine($"{DateTime.Now} - Tempo para obter pagina {url}  de reclamações: {sw.Elapsed}");
                }
                catch (Exception ex)
                {
                    continue;
                }

                if (!items.Any())
                    break;
                //IEnumerable<PageItens> items;
                //if (paginate.LAST is not null)
                //    items = paginate.LAST;
                //else if (paginate.NOT_ANSWERED is not null)
                //    items = paginate.NOT_ANSWERED;
                //else if (paginate.ANSWERED is not null)
                //    items = paginate.ANSWERED;
                //else if (paginate.EVALUATED is not null)
                //    items = paginate.EVALUATED;
                //else
                //    break;
                //;

                int index = 0;
                foreach (var item in items)
                {
                    try
                    {
                        if (idList.Contains(item.Title))
                        {
                            Console.WriteLine($"Titulo: {item.Title} repetido");
                            continue;
                        }
                        else
                        {
                            idList.Add(item.Title);
                            //Thread.Sleep(500);
                        }


                        //string title = item.title.ToLower().Replace(" ", "-")
                        //                                   .Replace(".", "")
                        //                                   .Replace("+", "")
                        //                                   .Replace(",", "")
                        //                                   .Replace(";", "")
                        //                                   .Replace(":", "")
                        //                                   .Replace("!", "")
                        //                                   .Replace("?", "")
                        //                                   .Replace("/", "")
                        //                                   .Replace("ç", "c")
                        //                                   .Replace("ã", "a")
                        //                                   .Replace("õ", "o")
                        //                                   .Replace("á", "a")
                        //                                   .Replace("é", "e")
                        //                                   .Replace("í", "i")
                        //                                   .Replace("ó", "o")
                        //                                   .Replace("ú", "u")
                        //                                   .Replace("à", "a")
                        //                                   .Replace("è", "e")
                        //                                   .Replace("ì", "i")
                        //                                   .Replace("ò", "o")
                        //                                   .Replace("ù", "u")
                        //                                   .Replace("â", "a")
                        //                                   .Replace("ê", "e")
                        //                                   .Replace("î", "i")
                        //                                   .Replace("ô", "o")
                        //                                   .Replace("û", "u")
                        //                                   .Replace("[editado-pelo-reclame-aqui]", "");
                        //title = title.Length == 0 ? title : title.Last() == '-' ? title.Substring(0, title.Length - 1) : title;
                        string url = $"https://www.reclameaqui.com.br{item.Url}";
                        //Console.WriteLine($"{DateTime.Now} - Page: {(int)i / 10} - Item:{index++} - Url:{url}");
                        Stopwatch sw = Stopwatch.StartNew();
                        var entity = await ProcessPage(url);
                        sw.Stop();


                        if (_timRepository.Exists(entity))
                            await _timRepository.ReplaceOne(entity);
                        else
                            await _timRepository.InsertOne(entity);

                        Console.WriteLine($"{DateTime.Now} - Page: {(int)i / 10} - Item:{index++} - Data:{entity.Data} - ID: {entity.Id} - Elapse page: {sw.Elapsed} - Url:{url}");

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                }
            }
        }

        private async Task<TimEntity> ProcessPage(string url)
        {
            try
            {
                Uri baseUri = new Uri(url);
                CookieContainer cookieContainer = new CookieContainer();
                using (HttpClientHandler handler = new HttpClientHandler() { CookieContainer = cookieContainer })
                {
                    using (HttpClient client = new HttpClient(handler) { BaseAddress = baseUri })
                    {
                        client.Timeout = TimeSpan.FromSeconds(30);
                        client.DefaultRequestHeaders.Add("Accept", "*/*");
                        client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:67.0) Gecko/20100101 Firefox/67.0");
                        client.DefaultRequestHeaders.Add("Connection", "keep-alive");
                        client.DefaultRequestHeaders.Add("Cookie", "_abck=B4EF689CE275EB10CF36863FAB53E069~-1~YAAQJW81vc2yypyDAQAAuc95xAiV8kqb8bkG9juvFoyGGPdpZp4BHE69CzfWHSWkoaR7vCXaJPMOPWNhSow+f0UmzH1BKlTjukJJJiu63zHypa/YRRR/zBbztI1jxPzJVJGThLh7c9xp5K92bpg8k7Zfuw90uI9XtHKbItwfJRBxCDk737pSkAgZQMAxq7j2WGBP5v6rpGo7S/nA2EdVIf3R+SS+H4j0hVJLfVDgxt4mkFrztMHRr4rs1z2jUPIPx/HSGW7CdpTuqESaOBZClzi6mKGOOVdtB0VetGfKguJFQ2/DCPWfr8L4nqqO92Itti4nNithpPtbpUmegN2Ro7OW7GT1D81tq3fykMeszSlD9itOfJspi43rRBDRR1wI6Q==~-1~-1~-1");

                        HttpResponseMessage response = await client.GetAsync("");
                        response.EnsureSuccessStatusCode();
                        string paginateString = await response.Content.ReadAsStringAsync();

                        return ParserComplaint(paginateString);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private async Task<IEnumerable<ReclameAquiPagination>> ProcessPaginatedPage(string url, bool redo = true)
        {
            try
            {
                Uri baseUri = new Uri(url);
                CookieContainer cookieContainer = new CookieContainer();
                using (HttpClientHandler handler = new HttpClientHandler() { CookieContainer = cookieContainer })
                {
                    using (HttpClient client = new HttpClient(handler) { BaseAddress = baseUri })
                    {
                        client.Timeout = TimeSpan.FromSeconds(30);
                        client.DefaultRequestHeaders.Add("Accept", "*/*");
                        client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:67.0) Gecko/20100101 Firefox/67.0");
                        client.DefaultRequestHeaders.Add("Connection", "keep-alive");
                        client.DefaultRequestHeaders.Add("Cookie", "_abck=B4EF689CE275EB10CF36863FAB53E069~-1~YAAQJW81vc2yypyDAQAAuc95xAiV8kqb8bkG9juvFoyGGPdpZp4BHE69CzfWHSWkoaR7vCXaJPMOPWNhSow+f0UmzH1BKlTjukJJJiu63zHypa/YRRR/zBbztI1jxPzJVJGThLh7c9xp5K92bpg8k7Zfuw90uI9XtHKbItwfJRBxCDk737pSkAgZQMAxq7j2WGBP5v6rpGo7S/nA2EdVIf3R+SS+H4j0hVJLfVDgxt4mkFrztMHRr4rs1z2jUPIPx/HSGW7CdpTuqESaOBZClzi6mKGOOVdtB0VetGfKguJFQ2/DCPWfr8L4nqqO92Itti4nNithpPtbpUmegN2Ro7OW7GT1D81tq3fykMeszSlD9itOfJspi43rRBDRR1wI6Q==~-1~-1~-1");
                        HttpResponseMessage response = await client.GetAsync("");


                        response.EnsureSuccessStatusCode();
                        string paginateString = await response.Content.ReadAsStringAsync();

                        return ParserPaginated(paginateString);
                    }
                }
            }
            catch (Exception ex)
            {
                if (redo)
                    return await ProcessPaginatedPage(url, false);
                throw ex;
            }
        }



        private IEnumerable<ReclameAquiPagination> ParserPaginated(string webPage)
        {
            int firstIndex = 0;
            int validIndex = 0;
            int lastIndex = 0;
            List<ReclameAquiPagination> listItens = new List<ReclameAquiPagination>();
            while (validIndex != -1)
            {
                firstIndex = webPage.IndexOf("class=\"sc-1pe7b5t-0 bJdtis\"", firstIndex) + 28;
                lastIndex = webPage.IndexOf("</h4>", firstIndex);
                string item = webPage.Substring(firstIndex, lastIndex - firstIndex);

                int startLink = item.IndexOf("href=\"") + 6;
                int lastLink = item.IndexOf("\"", startLink);
                string link = item.Substring(startLink, lastLink - startLink);

                int startTitle = item.IndexOf("class=\"sc-1pe7b5t-1 fTrwHU\">") + 28;
                string title = item.Substring(startTitle);


                listItens.Add(new ReclameAquiPagination() { Title = title, Url = link });
                validIndex = webPage.IndexOf("class=\"sc-1pe7b5t-0 bJdtis\"", firstIndex);
            }

            return listItens;
        }

        private TimEntity ParserComplaint(string webPage)
        {
            string original = webPage;
            try
            {

                int bodyIndex = webPage.IndexOf("<body>");
                webPage = webPage.Substring(bodyIndex);

                int titleStart = webPage.IndexOf("complaint-title") + 41;
                int titleEnd = webPage.IndexOf("</h1>", titleStart);
                string title = webPage.Substring(titleStart, titleEnd - titleStart);

                int statusStart = webPage.IndexOf("sc-1a60wwz-1") + 21;
                int statusEnd = webPage.IndexOf("</span>", statusStart);
                string status = webPage.Substring(statusStart, statusEnd - statusStart);

                int locationStart = webPage.IndexOf("complaint-location") + 20;
                int locationEnd = webPage.IndexOf("</span>", locationStart);
                string location = webPage.Substring(locationStart, locationEnd - locationStart);

                int creationDateStart = webPage.IndexOf("complaint-creation-date") + 25;
                int creationDateEnd = webPage.IndexOf("</span>", creationDateStart);
                string creationDate = webPage.Substring(creationDateStart, creationDateEnd - creationDateStart).Replace(" às", "");

                int idStart = webPage.IndexOf("<b>ID:</b>") + 11;
                int idEnd = webPage.IndexOf("</span>", idStart);
                string id = webPage.Substring(idStart, idEnd - idStart);


                string categoria = string.Empty;
                int categoriaStart = webPage.IndexOf("listitem-categoria");
                if (categoriaStart != -1)
                {
                    int categoriaEnd = webPage.IndexOf("</a></div>", categoriaStart);
                    categoria = webPage.Substring(categoriaStart, categoriaEnd - categoriaStart);
                    categoriaStart = categoria.LastIndexOf(">") + 1;
                    categoria = categoria.Substring(categoriaStart);
                }

                string produto = string.Empty;
                int produtoStart = webPage.IndexOf("listitem-produto");
                if (produtoStart != -1)
                {
                    int produtoEnd = webPage.IndexOf("</a></div>", produtoStart);
                    produto = webPage.Substring(produtoStart, produtoEnd - produtoStart);
                    produtoStart = produto.LastIndexOf(">") + 1;
                    produto = produto.Substring(produtoStart);
                }


                string problema = string.Empty;
                int problemaStart = webPage.IndexOf("listitem-problema");
                if (problemaStart != -1)
                {
                    int problemaEnd = webPage.IndexOf("</a></div>", problemaStart);
                    problema = webPage.Substring(problemaStart, problemaEnd - problemaStart);
                    problemaStart = problema.LastIndexOf(">") + 1;
                    problema = problema.Substring(problemaStart);
                }


                int descricaoStart = webPage.IndexOf("lzlu7c-17 fXwQIB") + 18;
                int descricaoEnd = webPage.IndexOf("</p>", descricaoStart);
                string descricao = webPage.Substring(descricaoStart, descricaoEnd - descricaoStart);

                List<Interacoes> interacoes = new();
                string complaintInteraction = webPage;
                bool willBreak = false;
                while (!willBreak)
                {
                    int answerStart = complaintInteraction.IndexOf("complaint-interaction\"") + 21;
                    if (answerStart == 20)
                    {
                        break;
                    }
                    int nextAnswerStart = complaintInteraction.IndexOf("complaint-interaction\"", answerStart);

                    complaintInteraction = complaintInteraction.Substring(answerStart);

                    int interactionTypeStart = complaintInteraction.IndexOf("div type=\"") + 10;
                    int interactionTypeEnd = complaintInteraction.IndexOf("\"", interactionTypeStart);
                    string interactionType = complaintInteraction.Substring(interactionTypeStart, interactionTypeEnd - interactionTypeStart);

                    int interactionDateStart = complaintInteraction.IndexOf("sc-1o3atjt-3 ipwWvs\">") + 21;
                    int interactionDateEnd = complaintInteraction.IndexOf("</span>", interactionDateStart);
                    string interactionDate = complaintInteraction.Substring(interactionDateStart, interactionDateEnd - interactionDateStart).Replace(" às", "");

                    int interactionStart = complaintInteraction.IndexOf("sc-1o3atjt-4 JkSWX\">") + 20;
                    int interactionEnd = complaintInteraction.IndexOf("</p>", interactionStart);
                    string interaction = complaintInteraction.Substring(interactionStart, interactionEnd - interactionStart);

                    interacoes.Add(new Interacoes()
                    {
                        TipoInteracao = interactionType,
                        DataInteracao = DateTime.ParseExact(interactionDate, "dd/MM/yyyy HH:mm", null),
                        TextoInteracao = interaction
                    });
                    if (nextAnswerStart == -1)
                        willBreak = true;
                    else
                        complaintInteraction = complaintInteraction.Substring(nextAnswerStart - answerStart);

                }

                bool skip = false;
                string resolvido = string.Empty;
                string negocio = string.Empty;
                string nota = string.Empty;
                int resolvidoStart = webPage.IndexOf("sc-1a60wwz-1 gfOjuF\">") + 21;
                if (resolvidoStart == 20) skip = true;
                else
                {
                    int resolvidoEnd = webPage.IndexOf("</span><", resolvidoStart);
                    resolvido = webPage.Substring(resolvidoStart, resolvidoEnd - resolvidoStart);
                }


                if (!skip)
                {
                    int negocioStart = webPage.IndexOf("uh4o7z-3 uh4o7z-4 ceUcTc ezNHIi\">") + 33;
                    int negocioEnd = webPage.IndexOf("</div>", negocioStart);
                    negocio = webPage.Substring(negocioStart, negocioEnd - negocioStart);
                }

                if (!skip)
                {
                    int notaStart = webPage.IndexOf("uh4o7z-3 ceUcTc\">") + 17;
                    int notaEnd = webPage.IndexOf("</div>", notaStart);
                    nota = webPage.Substring(notaStart, notaEnd - notaStart);
                }


                return new TimEntity()
                {
                    Titulo = title,
                    Status = status,
                    Localizacao = location,
                    Data = DateTime.ParseExact(creationDate, "dd/MM/yyyy HH:mm", null),
                    Id = int.Parse(id),
                    Categoria = categoria == string.Empty ? null : categoria,
                    Problema = problema == string.Empty ? null : problema,
                    Produto = produto == string.Empty ? null : produto,
                    Descricao = HtmlUtil.ConvertToPlainText(descricao),
                    Interacoes = interacoes.Any() ? interacoes : null,
                    Resolvido = skip ? null : resolvido == "Resolvido",
                    VoltariaNegocio = skip ? null : negocio == "Sim",
                    Nota = skip ? null : Int32.Parse(nota)
                };
            }
            catch (Exception ex)
            {
                //ParserComplaint(original);
                throw ex;
            }


        }
    }
}
