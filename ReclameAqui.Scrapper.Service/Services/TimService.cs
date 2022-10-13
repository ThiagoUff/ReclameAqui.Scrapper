using Newtonsoft.Json;
using ReclameAqui.Scrapper.Domain.Entities;
using ReclameAqui.Scrapper.Domain.Interfaces.Repository;
using ReclameAqui.Scrapper.Domain.Interfaces.Services;
using System.Net;
using HtmlAgilityPack;
using ReclameAqui.Scrapper.Service.Utils;
using System.Diagnostics;
using System.Text;
using DocumentFormat.OpenXml.Spreadsheet;
using ClosedXML.Excel;

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
            File.WriteAllText(@"..\..\..\..\dataset.csv", csv.ToString());
            workbook.SaveAs(@"..\..\..\..\dataset.xlsx");
        }

        public async Task Scrapper()
        {
            List<Tuple<string, string>> idList = new List<Tuple<string, string>>();
            int limit = 5000;
            int total = 0;
            int i = 1000;

            for (; i < 50000; i = i + 10)
            {
                ReclameAquiPagination paginate;
                try
                {
                    Stopwatch sw = Stopwatch.StartNew();
                    paginate = await ProcessPaginatedPage($"https://www.reclameaqui.com.br/empresa/live-tim/lista-reclamacoes/?pagina={i}");
                    sw.Stop();
                    Console.WriteLine($"Tempo para obter pagina {i/10} de reclamações: {sw.Elapsed}");
                }
                catch (Exception)
                {
                    continue;
                }
                limit = paginate.count;
                int index = 0;
                foreach (var item in paginate.LAST)
                {
                    try
                    {
                        if (idList.Contains(System.Tuple.Create(item.id, item.title)))
                        {
                            Console.WriteLine($"id: {item.id}, Titulo: {item.title} repetido");
                            continue;
                        }
                        else
                        {
                            idList.Add(System.Tuple.Create(item.id, item.title));
                            //Thread.Sleep(500);
                        }


                        string title = item.title.ToLower().Replace(" ", "-")
                                                           .Replace(".", "")
                                                           .Replace("+", "")
                                                           .Replace(",", "")
                                                           .Replace(";", "")
                                                           .Replace(":", "")
                                                           .Replace("!", "")
                                                           .Replace("?", "")
                                                           .Replace("/", "")
                                                           .Replace("ç", "c")
                                                           .Replace("ã", "a")
                                                           .Replace("õ", "o")
                                                           .Replace("á", "a")
                                                           .Replace("é", "e")
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
                                                           .Replace("û", "u")
                                                           .Replace("[editado-pelo-reclame-aqui]", "");
                        title = title.Length == 0 ? title : title.Last() == '-' ? title.Substring(0, title.Length - 1) : title;
                        string url = $"https://www.reclameaqui.com.br/live-tim/{title}_{item.id}/";
                       
                        Stopwatch sw = Stopwatch.StartNew();
                        var entity = await ProcessPage(url);
                        sw.Stop();


                        //if (_reclameAquiRepository.Exists(entity.Item1))
                        //    _reclameAquiRepository.ReplaceOne(entity.Item1);
                        //else
                        //    _reclameAquiRepository.InsertOne(entity.Item1);

                        Stopwatch sw2 = Stopwatch.StartNew();
                        if (_timRepository.Exists(entity.Item2))
                        {
                            TimEntity tim = await _timRepository.FindOne(entity.Item1.Id);
                            if (entity.Item2.Equals(tim))
                            {
                                sw2.Stop();

                                Console.WriteLine($"Page: {(int)i / 10} - Item:{index++} - Total:{total++} - ID: {item.id} - Elapse page: {sw.Elapsed} - Elapse Mongo: {sw2.Elapsed} - Url:{url}");
                                break;
                            }
                            else
                                _timRepository.ReplaceOne(entity.Item2);
                        }
                        else
                            _timRepository.InsertOne(entity.Item2);
                        sw2.Stop();

                        Console.WriteLine($"Page: {(int)i / 10} - Item:{index++} - Total:{total++} - ID: {item.id} - Elapse page: {sw.Elapsed} - Elapse Mongo: {sw2.Elapsed} - Url:{url}");

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                }
            }
        }

        private async Task<Tuple<ReclameAquiEntity, TimEntity>> ProcessPage(string url)
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

                        string json = paginateString.Substring(paginateString.LastIndexOf("</div>"));
                        json = json.Substring(json.IndexOf("{\"complaint\""));
                        json = json.Substring(0, json.IndexOf(",\"__N_SSP\":"));
                        ReclameAquiEntity entity = JsonConvert.DeserializeObject<ReclameAquiEntity>(json);
                        entity.Id = entity.complaint.legacyId.Value;

                        TimEntity tim = new TimEntity()
                        {
                            Categoria = entity.category is null ? null : entity.category.name,
                            Data = entity.complaint.created,
                            Descricao = HtmlUtil.ConvertToPlainText(entity.complaint.description),
                            Id = entity.complaint.legacyId.Value,
                            Interacoes = entity.complaint.interactions.Any() ? entity.complaint.interactions.Select(x => new Interacoes()
                            {
                                DataInteracao = x.created,
                                TextoInteracao = HtmlUtil.ConvertToPlainText(x.message),
                                TipoInteracao = x.type,
                            }) : null,
                            Localizacao = entity.complaint.userCity,
                            Nota = entity.complaint.evaluated ? entity.complaint.score : null,
                            Problema = entity.problemType is null ? null : entity.problemType.name,
                            Produto = entity.productType is null ? null : entity.productType.name,
                            Resolvido = entity.complaint.evaluated ? entity.complaint.solved : null,
                            Titulo = entity.complaint.title,
                            VoltariaNegocio = entity.complaint.evaluated ? entity.complaint.dealAgain : null,
                            Avaliada = entity.complaint.evaluated,
                            Status = entity.complaint.status,

                        };
                        return System.Tuple.Create(entity, tim);
                    }
                }

                 
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private async Task<ReclameAquiPagination> ProcessPaginatedPage(string url, bool redo = true)
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
                        string json = paginateString.Substring(paginateString.LastIndexOf("</div>"));
                        json = json.Substring(json.IndexOf("{\"LAST\":"));
                        json = json.Substring(0, json.IndexOf(",\"mediaKit\""));
                        return JsonConvert.DeserializeObject<ReclameAquiPagination>(json);
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
    }
}
