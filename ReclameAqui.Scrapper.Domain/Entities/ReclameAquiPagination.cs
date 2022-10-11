namespace ReclameAqui.Scrapper.Domain.Entities
{
    public class ReclameAquiPagination
    {
        public IEnumerable<LAST> LAST { get; set; }
        public int count { get; set; }
    }
  
    public class LAST
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
