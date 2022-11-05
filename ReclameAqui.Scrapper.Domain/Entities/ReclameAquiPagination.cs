namespace ReclameAqui.Scrapper.Domain.Entities
{
    public class ReclameAquiPagination
    {
        public IEnumerable<PageItens> LAST { get; set; }
        public IEnumerable<PageItens> NOT_ANSWERED { get; set; }
        public IEnumerable<PageItens> ANSWERED { get; set; }
        public IEnumerable<PageItens> EVALUATED { get; set; }
        

        public int count { get; set; }
    }
  
    public class PageItens
    {
        public DateTime created { get; set; }
        public string description { get; set; }
        public bool solved { get; set; }
        public bool evaluated { get; set; }
        public string id { get; set; }
        public string title { get; set; }
        public string userName { get; set; }
        public string status { get; set; }
    }

}
