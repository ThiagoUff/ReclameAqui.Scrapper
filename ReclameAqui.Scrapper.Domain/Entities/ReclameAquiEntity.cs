namespace ReclameAqui.Scrapper.Domain.Entities
{
    public class ReclameAquiEntity
    {
        public int Id { get; set; }
        public Complaint? complaint { get; set; } = null;
        public Category? category { get; set; } = null;
        public ProductType? productType { get; set; } = null;
        public ProblemType? problemType { get; set; } = null;

    }
       
    public class Complaint
    {
        public string userCity { get; set; } = null;
        public string? otherProblemType { get; set; } = null;
        public bool? solved { get; set; } = null;
        public string description { get; set; }
        public List<object> phones { get; set; } = null;
        public bool? dealAgain { get; set; } = null;
        public string? otherProductType { get; set; } = null;
        public string title { get; set; } = null;
        public List<Interaction>? interactions { get; set; } = null;
        public string? evaluation { get; set; } = null;
        public DateTime? firstInteractionDate { get; set; } = null;
        public int? score { get; set; } = null;
        public bool? userRequestedDelete { get; set; } = null;
        public string? userState { get; set; } = null;
        public bool? hasReply { get; set; } = null;
        public DateTime? modified { get; set; } = null;
        public bool? compliment { get; set; } = null;
        public int? legacyId { get; set; } = null;
        public string? userEmail { get; set; } = null;
        public bool? evaluated { get; set; } = null;
        public string? id { get; set; } = null;
        public bool? read { get; set; } = null;
        public object? address { get; set; } = null;
        public object? marketplaceComplain { get; set; } = null;
        public DateTime created { get; set; }
        public string? ip { get; set; } = null;
        public bool? frozen { get; set; } = null;
        public string? userName { get; set; } = null;
        public DateTime? requestEvaluation { get; set; } = null;
        public bool? inModeration { get; set; } = null;
        public bool? deleted { get; set; } = null;
        public List<object>? files { get; set; } = null;
        public string? deletedIp { get; set; } = null;
        public string status { get; set; }
    }

  
    public class Interaction
    {
        public bool? deleted { get; set; }  = null;
        public DateTime created { get; set; }
        public string? ip { get; set; }  = null;
        public string? id { get; set; }  = null;
        public string message { get; set; }
        public string type { get; set; }
    }
   

    public class ProblemType
    {
        public bool? deleted { get; set; }  = null;
        public DateTime? created { get; set; }  = null;
        public string? id { get; set; }  = null;
        public bool? productSensitive { get; set; }  = null;
        public object? ip { get; set; }  = null;
        public string? name { get; set; }  = null;
        public int? legacyId { get; set; }  = null;
        public DateTime? modified { get; set; }  = null;
        public string? problemOrigin { get; set; }  = null;
    }

    public class ProductType
    {
        public bool? deleted { get; set; }  = null;
        public DateTime? created { get; set; }  = null;
        public string? productTypeKind { get; set; } = null;
        public string? id { get; set; } = null;
        public bool? adult { get; set; } = null;
        public bool? female { get; set; } = null;
        public bool? male { get; set; } = null;
        public bool? child { get; set; } = null;
        public object? ip { get; set; } = null;
        public string? name { get; set; } = null;
        public int? legacyId { get; set; }  = null;
        public DateTime? modified { get; set; }  = null;
    }

       public class Category
    {
        public bool? deleted { get; set; } = null;
        public DateTime? created { get; set; }  = null;
        public string? id { get; set; } = null;
        public string? color { get; set; } = null;
        public object? ip { get; set; } = null;
        public string? name { get; set; } = null;
        public int? legacyId { get; set; }  = null;
        public DateTime? modified { get; set; }  = null;
        public string? shortName { get; set; } = null;
    }
}
