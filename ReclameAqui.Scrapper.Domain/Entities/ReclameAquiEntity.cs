namespace ReclameAqui.Scrapper.Domain.Entities
{
    public class ReclameAquiEntity
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = null!;
        public string Localizacao { get; set; } = null!;
        public DateTime Data { get; set; }
        public string? Categoria { get; set; } = null!;
        public string? Produto { get; set; } = null!;
        public string? Problema { get; set; } = null!;
        public string Descricao { get; set; } = null!;
        public IEnumerable<Interacoes>? Interacoes { get; set; } = null!;
        public string Status { get; set; }
        public bool? Resolvido { get; set; }
        public bool? VoltariaNegocio { get; set; }
        public bool? Avaliada { get { return Resolvido.HasValue; } }
        public int? Nota { get; set; }

    }

    public class Interacoes
    {
        public DateTime? DataInteracao { get; set; }
        public string? TextoInteracao { get; set; }
        public string? TipoInteracao { get; set; }

    }
}
