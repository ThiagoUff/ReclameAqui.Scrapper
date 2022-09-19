namespace ReclameAqui.Scrapper.Domain.Entities
{
    public class TimEntity
    {
        public int Id { get; set; } 
        public string Titulo { get; set; } = null!;
        public string Localizacao { get; set; } = null!;
        public string Data { get; set; } = null!;
        public IEnumerable<string> Categorias { get; set; } = null!;
        public string Descricao { get; set; } = null!;
        public string? Resposta { get; set; }
        public string? DataResposta { get; set; }
        public string? Consideracao { get; set; }
        public string? DataConsideracao { get; set; }
        public bool? Resolvido { get; set; }
        public bool? VoltariaNegocio { get; set; }
        public int? Nota { get; set; }

    }
}
