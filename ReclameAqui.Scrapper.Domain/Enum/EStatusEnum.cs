using System.ComponentModel;

namespace ReclameAqui.Scrapper.Domain.Enum
{
    public enum EStatusEnum
    {
        [Description("")]
        NA = -1,
        NOT_ANSWERED = 0,
        ANSWERED = 1,
        EVALUETED = 2,
        PENDING = 3,
        SOLVED = 4,
    }
}
