using System.ComponentModel;

namespace ReclameAqui.Scrapper.Domain.Enum
{
    public enum ECategoriaTimEnum
    {
        [Description("")]
        NA = 0,

        [Description("0000000000000067")]
        TelefoniaCelular = 1,

        [Description("0000000000000069")]
        ProvedoresEServicosDeInternet = 2,
        
        [Description("0000000000000255")]
        ProblemasComAtendimento = 3,

        [Description("0000000000000000")]
        NaoEncontreiMeuProblema = 4,

        [Description("0000000000000064")]
        CelularESmartphone = 5,

        [Description("0000000000000066")]
        TelefoniaFixa = 6,

        [Description("000000000000001")]
        NaoCategorizado = 7,

        [Description("0000000000000312")]
        Diversos = 8,

        [Description("0000000000000003")]
        CartoesDeCredito = 9,

        [Description("0000000000000259")]
        ProblemasComSite = 10,

        [Description("0000000000000001")]
        Bancos = 11,

        [Description("0000000000000135")]
        MeiosDePagamento = 12,

        [Description("0000000000000302")]
        ConcessionariasDeServico = 13,

        [Description("0000000000000311")]
        Aplicativos = 14,

        [Description("0000000000000112")]
        Softwares = 14,

        [Description("0000000000000260")]
        ProblemasNaLoja = 14,

        [Description("0000000000000004")]
        Financeiras = 14,

        [Description("0000000000000084")]
        RecuperadoraDeCredito = 14,

       //TEM OUTROS
    }
}
