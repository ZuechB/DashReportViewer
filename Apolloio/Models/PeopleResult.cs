namespace Apolloio.Models
{
    public class PeopleResult
    {
        public object[] breadcrumbs { get; set; }
        public PeopleResult_Pagination pagination { get; set; }
        public string[] model_ids { get; set; }
        public object[] contacts { get; set; }
        public PeopleResult_Person[] people { get; set; }
        public object num_fetch_result { get; set; }
    }

    public class PeopleResult_Pagination
    {
        public int page { get; set; }
        public int per_page { get; set; }
        public int total_entries { get; set; }
        public int total_pages { get; set; }
    }

    public class PeopleResult_Person
    {
        public string id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string name { get; set; }
        public string linkedin_url { get; set; }
        public string title { get; set; }
        public string city { get; set; }
        public string email_status { get; set; }
        public string photo_url { get; set; }
        public object twitter_url { get; set; }
        public object github_url { get; set; }
        public object facebook_url { get; set; }
        public object extrapolated_email_confidence { get; set; }
        public string headline { get; set; }
        public string country { get; set; }
        public string email { get; set; }
        public string state { get; set; }
        public bool excluded_for_leadgen { get; set; }
        public object[] starred_by_user_ids { get; set; }
        public string organization_id { get; set; }
        public PeopleResult_Organization organization { get; set; }
    }

    public class PeopleResult_Organization
    {
        public string id { get; set; }
        public string name { get; set; }
        public string website_url { get; set; }
        public object blog_url { get; set; }
        public string angellist_url { get; set; }
        public string linkedin_url { get; set; }
        public string twitter_url { get; set; }
        public string facebook_url { get; set; }
        public PeopleResult_Primary_Phone primary_phone { get; set; }
        public string[] languages { get; set; }
        public int? alexa_ranking { get; set; }
        public string phone { get; set; }
        public string linkedin_uid { get; set; }
        public string publicly_traded_symbol { get; set; }
        public string publicly_traded_exchange { get; set; }
        public string logo_url { get; set; }
        public string crunchbase_url { get; set; }
        public string primary_domain { get; set; }
        public object[] starred_by_user_ids { get; set; }
        public PeopleResult_Persona_Counts persona_counts { get; set; }
        public string market_cap { get; set; }
    }

    public class PeopleResult_Primary_Phone
    {
        public string source { get; set; }
        public string number { get; set; }
    }

    public class PeopleResult_Persona_Counts
    {
    }
}