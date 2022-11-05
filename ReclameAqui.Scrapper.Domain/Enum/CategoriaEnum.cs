using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReclameAqui.Scrapper.Domain.Enum
{
    public enum CategoriaEnum
    {
        [Description("0000000000000069")]
        Prov_serv_internet = 0,

        [Description("0000000000000000")]
        Nao_Encontrei_Meu_Problema = 1,
        
        [Description("0000000000000255")]
        Problemas_Com_Atendimento = 2,

        [Description("0000000000000259")]
        Problemas_Com_Site = 3,

        [Description("000000000000001")]
        Nao_Categorizado = 4,

        [Description("0000000000000066")]
        Telefonia_Fixa = 5,

        [Description("0000000000000067")]
        Telefonia_celular = 6,

        [Description("0000000000000001")]
        Bancos = 7,

        [Description("0000000000000003")]
        Cartoes_De_Credito = 8,

        [Description("0000000000000070")]
        Sites_E_Portais = 9,
    }
}
