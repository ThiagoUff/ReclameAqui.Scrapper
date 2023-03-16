using System.ComponentModel;

namespace ReclameAqui.Scrapper.Domain.Enum
{
    public enum StatusEnum
    {
        [Description("")]
        NA = -1,
        NOT_ANSWERED = 0,
        ANSWERED = 1,
        EVALUETED = 2,
        PENDINg = 3,
        //SOLVED = 4,
    }
}
