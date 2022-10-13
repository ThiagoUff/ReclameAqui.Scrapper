namespace ReclameAqui.Scrapper.Domain.Entities
{
    public class TimEntity : IEquatable<TimEntity>
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
        public bool Avaliada { get; set; }
        public int? Nota { get; set; }

        public bool Equals(TimEntity? other)
        {
            if (other is null) return false;

            if(this.Interacoes is not null){
                if (this.Interacoes.Count() != other.Interacoes.Count()) return false;

                for (int i = 0; i < this.Interacoes.Count(); i++)
                {
                    if(!this.Interacoes.ElementAt(i).Equals(other.Interacoes.ElementAt(i))) return false;
                }
            }

            return this.Id == other.Id &&
                   this.Titulo == other.Titulo &&
                   this.Localizacao == other.Localizacao &&
                   //this.Data == other.Data &&
                   this.Produto == other.Produto &&
                   this.Problema == other.Problema &&
                   this.Descricao == other.Descricao &&
                   this.Categoria == other.Categoria &&
                   this.Status == other.Status &&
                   this.Resolvido == other.Resolvido &&
                   this.VoltariaNegocio == other.VoltariaNegocio &&
                   this.Avaliada == other.Avaliada &&
                   this.Nota == other.Nota;
        }
    }

    public class Interacoes : IEquatable<Interacoes>
    {
        public DateTime? DataInteracao { get; set; }
        public string? TextoInteracao { get; set; }
        public string? TipoInteracao { get; set; }

        public bool Equals(Interacoes? other)
        {
            if (this is null && other is null) return true;

            return this.DataInteracao == other.DataInteracao.Value.AddHours(-3) &&
                   this.TextoInteracao == other.TextoInteracao &&
                   this.TipoInteracao == other.TipoInteracao;
        }
        
    }
}
