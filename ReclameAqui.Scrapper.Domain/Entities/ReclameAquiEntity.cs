namespace ReclameAqui.Scrapper.Domain.Entities
{
    public class ReclameAquiEntity
    {
        public int Id { get; set; }
        public Complaint complaint { get; set; }
        public Category category { get; set; }
        public ProductType productType { get; set; }
        public ProblemType problemType { get; set; }

    }
    public class AdditionalField
    {
        public string fieldName { get; set; }
        public bool deleted { get; set; }
        public DateTime created { get; set; }
        public List<Option> options { get; set; }
        public int? legacyId { get; set; }
        public DateTime modified { get; set; }
        public string id { get; set; }
        public string type { get; set; }
        public string value { get; set; }
        public int order { get; set; }
        public string conditionalOptionId { get; set; }
        public string conditionalFieldId { get; set; }
    }

    public class Address
    {
        public bool deleted { get; set; }
        public DateTime created { get; set; }
        public Location location { get; set; }
        public string type { get; set; }
        public string country { get; set; }
        public string zipCode { get; set; }
        public string route { get; set; }
        public string city { get; set; }
        public DateTime modified { get; set; }
        public string id { get; set; }
        public string neighborhood { get; set; }
        public string state { get; set; }
    }

    public class Category
    {
        public bool deleted { get; set; }
        public DateTime created { get; set; }
        public string id { get; set; }
        public string color { get; set; }
        public object ip { get; set; }
        public string name { get; set; }
        public int? legacyId { get; set; }
        public DateTime modified { get; set; }
        public string shortName { get; set; }
    }

    public class Company
    {
        public CompanyPageConfiguration companyPageConfiguration { get; set; }
        public List<object> additionalFields { get; set; }
        public List<object> documents { get; set; }
        public List<object> panels { get; set; }
        public List<object> competitorsIds { get; set; }
        public List<object> phones { get; set; }
        public object hasLocation { get; set; }
        public bool hasRaPhone { get; set; }
        public object fromMothership { get; set; }
        public List<object> emails { get; set; }
        public bool hasRaChat { get; set; }
        public List<object> competitors { get; set; }
        public bool hasSubBrands { get; set; }
        public List<object> presences { get; set; }
        public string id { get; set; }
        public List<object> serviceChannels { get; set; }
        public List<object> categories { get; set; }
        public List<object> companyResponsibles { get; set; }
        public List<object> images { get; set; }
        public Address address { get; set; }
        public bool canBeRA1000 { get; set; }
        public DateTime created { get; set; }
        public List<object> stores { get; set; }
        public string welcomeMessage { get; set; }
        public object hasRAV { get; set; }
        public bool cnpjValidated { get; set; }
        public object users { get; set; }
        public bool underReview { get; set; }
        public bool deleted { get; set; }
        public bool hasPrivateContact { get; set; }
        public List<object> previousNames { get; set; }
        public object embedHash { get; set; }
        public List<object> affiliates { get; set; }
        public List<object> files { get; set; }
        public bool hugmeFreeOn { get; set; }
        public List<object> companyIndexes { get; set; }
        public string status { get; set; }
        public string companyName { get; set; }
        public DateTime firstAccess { get; set; }
        public object registerProgressBarEndDate { get; set; }
        public DateTime modified { get; set; }
        public string ip { get; set; }
        public string metatags { get; set; }
        public string shortname { get; set; }
        public string companyIndex6Months { get; set; }
        public bool createdByUser { get; set; }
        public string description { get; set; }
        public int complainCount { get; set; }
        public int? legacyId { get; set; }
        public object defaultingDate { get; set; }
        public string companyIndex12Months { get; set; }
        public DateTime lastAccess { get; set; }
        public string fantasyName { get; set; }
        public bool hasBrandPage { get; set; }
    }

    public class CompanyPageConfiguration
    {
        public string urlFacebook { get; set; }
        public string urlLinkedin { get; set; }
        public DateTime created { get; set; }
        public string urlTwitter { get; set; }
        public string ip { get; set; }
        public DateTime start { get; set; }
        public List<object> videoUrls { get; set; }
        public string urlInstagram { get; set; }
        public CompanyPageFlags companyPageFlags { get; set; }
        public object emails { get; set; }
        public string urlYoutube { get; set; }
        public string urlAnalyticsImpact { get; set; }
        public List<object> mobileCover { get; set; }
        public string priceAnalyticsImpact { get; set; }
        public bool deleted { get; set; }
        public DateTime modified { get; set; }
        public DateTime end { get; set; }
        public string id { get; set; }
        public string urlGPlus { get; set; }
        public string status { get; set; }
    }

    public class CompanyPageFlags
    {
        public DateTime raFormsEndDate { get; set; }
        public bool hasTrustVoxEcommerce { get; set; }
        public bool hasPrivatePhone { get; set; }
        public bool hasPhones { get; set; }
        public bool hasAnalyticsPerformance { get; set; }
        public bool hasRAForms { get; set; }
        public bool hasPhoneWorkingHour { get; set; }
        public bool hasCoupon { get; set; }
        public bool hasVerificada { get; set; }
        public DateTime raFormsStartDate { get; set; }
        public bool isFreezed { get; set; }
        public bool hasFaq { get; set; }
        public DateTime modified { get; set; }
        public string id { get; set; }
        public string configurationType { get; set; }
        public bool hasMobileCover { get; set; }
        public bool hasPhoneChannel { get; set; }
        public bool hasUrlSite { get; set; }
        public bool hasLogo { get; set; }
        public bool hasComplainPageDetail { get; set; }
        public bool hasLeadButtonContent { get; set; }
        public bool hasCover { get; set; }
        public DateTime created { get; set; }
        public bool hasEmails { get; set; }
        public bool hasSocialNetwork { get; set; }
        public string ip { get; set; }
        public bool hasDescription { get; set; }
        public bool hasCompanyPageAlert { get; set; }
        public bool hasTrustVoxMoments { get; set; }
        public bool hasLeadButtonComplain { get; set; }
        public bool hasRAChat { get; set; }
        public bool hasCompanyShowcase { get; set; }
        public bool hasLeadButtonVideo { get; set; }
        public bool hasNewsAndAlerts { get; set; }
        public bool hasVideoUrls { get; set; }
        public bool deleted { get; set; }
        public bool hasPrivateContact { get; set; }
        public bool hasAnalyticsAdvanced { get; set; }
        public bool hasLeadButton { get; set; }
        public bool hasUrlContact { get; set; }
    }

    public class Complaint
    {
        public string userCity { get; set; }
        public string otherProblemType { get; set; }
        public bool solved { get; set; }
        public string description { get; set; }
        public List<object> phones { get; set; }
        public bool dealAgain { get; set; }
        public string otherProductType { get; set; }
        public string title { get; set; }
        public List<Interaction> interactions { get; set; }
        public string evaluation { get; set; }
        public DateTime firstInteractionDate { get; set; }
        public int score { get; set; }
        public bool userRequestedDelete { get; set; }
        public string userState { get; set; }
        public bool hasReply { get; set; }
        public DateTime modified { get; set; }
        public bool compliment { get; set; }
        public int? legacyId { get; set; }
        public Company company { get; set; }
        public string userEmail { get; set; }
        public bool evaluated { get; set; }
        public string id { get; set; }
        public Presence presence { get; set; }
        public ProductType productType { get; set; }
        public bool read { get; set; }
        public object address { get; set; }
        public object marketplaceComplain { get; set; }
        public DateTime created { get; set; }
        public string ip { get; set; }
        public bool frozen { get; set; }
        public string userName { get; set; }
        public DateTime requestEvaluation { get; set; }
        public bool inModeration { get; set; }
        public bool deleted { get; set; }
        public List<object> files { get; set; }
        public Category category { get; set; }
        public ProblemType problemType { get; set; }
        public User user { get; set; }
        public string deletedIp { get; set; }
        public string status { get; set; }
    }

    public class Document
    {
        public string number { get; set; }
        public bool deleted { get; set; }
        public DateTime created { get; set; }
        public DateTime modified { get; set; }
        public string id { get; set; }
        public string type { get; set; }
    }

    public class Index
    {
        public string averageAnswerTime { get; set; }
        public string finalScore { get; set; }
        public string totalNotAnswered { get; set; }
        public string totalComplains { get; set; }
        public string consumerScore { get; set; }
        public DateTime created { get; set; }
        public string companyName { get; set; }
        public DateTime start { get; set; }
        public string solvedPercentual { get; set; }
        public string type { get; set; }
        public string totalAnswered { get; set; }
        public string totalComplains30 { get; set; }
        public string averageAnswerTime3M { get; set; }
        public string hasStamp { get; set; }
        public string dealAgainPercentual { get; set; }
        public string totalEvaluated { get; set; }
        public string answeredPercentual { get; set; }
        public string company { get; set; }
        public DateTime end { get; set; }
        public string id { get; set; }
        public string status { get; set; }
    }

    public class Interaction
    {
        public bool deleted { get; set; }
        public DateTime created { get; set; }
        public string ip { get; set; }
        public string id { get; set; }
        public string message { get; set; }
        public User user { get; set; }
        public string type { get; set; }
    }

  
    public class Location
    {
        public bool deleted { get; set; }
        public DateTime created { get; set; }
        public DateTime modified { get; set; }
        public string id { get; set; }
    }

   
    public class Option
    {
        public bool deleted { get; set; }
        public DateTime created { get; set; }
        public int? legacyId { get; set; }
        public string id { get; set; }
        public string value { get; set; }
        public int order { get; set; }
        public DateTime? modified { get; set; }
    }



    public class Presence
    {
        public bool deleted { get; set; }
        public DateTime created { get; set; }
        public List<object> categories { get; set; }
        public string id { get; set; }
        public List<object> problemTypes { get; set; }
        public string sector { get; set; }
    }

 

    public class ProblemType
    {
        public bool deleted { get; set; }
        public DateTime created { get; set; }
        public string id { get; set; }
        public bool productSensitive { get; set; }
        public object ip { get; set; }
        public string name { get; set; }
        public int? legacyId { get; set; }
        public DateTime modified { get; set; }
        public string problemOrigin { get; set; }
    }

  

    public class ProductType
    {
        public bool deleted { get; set; }
        public DateTime created { get; set; }
        public string productTypeKind { get; set; }
        public string id { get; set; }
        public bool adult { get; set; }
        public bool female { get; set; }
        public bool male { get; set; }
        public bool child { get; set; }
        public object ip { get; set; }
        public string name { get; set; }
        public int? legacyId { get; set; }
        public DateTime modified { get; set; }
    }

  
  
    public class Role
    {
        public bool deleted { get; set; }
        public DateTime created { get; set; }
    }

   
    public class User
    {
        public List<object> addresses { get; set; }
        public Role role { get; set; }
        public List<object> documents { get; set; }
        public bool cpfValidated { get; set; }
        public List<object> keys { get; set; }
        public DateTime created { get; set; }
        public string ip { get; set; }
        public List<object> phones { get; set; }
        public List<object> userNotifications { get; set; }
        public List<object> notificationsAllowed { get; set; }
        public bool deleted { get; set; }
        public List<object> socialProfiles { get; set; }
        public string cellphone { get; set; }
        public string id { get; set; }
        public string email { get; set; }
        public object cellphoneValidated { get; set; }
        public string status { get; set; }
        public string username { get; set; }
    }
}
