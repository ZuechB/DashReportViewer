using System;

namespace DashReportViewer.Stripe.Models
{
    public class StripeTransaction
    {
        public string id { get; set; }
        //public string _object { get; set; }
        public long amount { get; set; }
        //public int amount_refunded { get; set; }
        //public object application { get; set; }
        //public object application_fee { get; set; }
        //public object application_fee_amount { get; set; }
        //public string balance_transaction { get; set; }
        //public StripeTransaction_Billing_Details billing_details { get; set; }
        //public bool captured { get; set; }
        public DateTime created { get; set; }
        //public string currency { get; set; }
        //public string customer { get; set; }
        //public string description { get; set; }
        //public bool disputed { get; set; }
        public string failure_code { get; set; }
        public string failure_message { get; set; }
        //public StripeTransaction_Fraud_Details fraud_details { get; set; }
        //public string invoice { get; set; }
        //public bool livemode { get; set; }
        //public StripeTransaction_Metadata metadata { get; set; }
        //public object on_behalf_of { get; set; }
        //public object order { get; set; }
        //public StripeTransaction_Outcome outcome { get; set; }
        public bool paid { get; set; }
        //public string payment_intent { get; set; }
        //public string payment_method { get; set; }
        //public StripeTransaction_Payment_Method_Details payment_method_details { get; set; }
        //public object receipt_email { get; set; }
        //public object receipt_number { get; set; }
        //public string receipt_url { get; set; }
        //public bool refunded { get; set; }
        //public StripeTransaction_Refunds refunds { get; set; }
        //public object review { get; set; }
        //public object shipping { get; set; }
        //public object source_transfer { get; set; }
        //public object statement_descriptor { get; set; }
        //public object statement_descriptor_suffix { get; set; }
        //public string status { get; set; }
        //public object transfer_data { get; set; }
        //public object transfer_group { get; set; }
    }

    //public class StripeTransaction_Billing_Details
    //{
    //    public StripeTransaction_Address address { get; set; }
    //    public object email { get; set; }
    //    public object name { get; set; }
    //    public object phone { get; set; }
    //}

    //public class StripeTransaction_Address
    //{
    //    public object city { get; set; }
    //    public object country { get; set; }
    //    public object line1 { get; set; }
    //    public object line2 { get; set; }
    //    public string postal_code { get; set; }
    //    public object state { get; set; }
    //}

    //public class StripeTransaction_Fraud_Details
    //{
    //}

    //public class StripeTransaction_Metadata
    //{
    //}

    //public class StripeTransaction_Outcome
    //{
    //    public string network_status { get; set; }
    //    public object reason { get; set; }
    //    public string risk_level { get; set; }
    //    public int risk_score { get; set; }
    //    public string seller_message { get; set; }
    //    public string type { get; set; }
    //}

    //public class StripeTransaction_Payment_Method_Details
    //{
    //    public StripeTransaction_Card card { get; set; }
    //    public string type { get; set; }
    //}

    //public class StripeTransaction_Card
    //{
    //    public string brand { get; set; }
    //    public StripeTransaction_Checks checks { get; set; }
    //    public string country { get; set; }
    //    public int exp_month { get; set; }
    //    public int exp_year { get; set; }
    //    public string fingerprint { get; set; }
    //    public string funding { get; set; }
    //    public object installments { get; set; }
    //    public string last4 { get; set; }
    //    public string network { get; set; }
    //    public object three_d_secure { get; set; }
    //    public object wallet { get; set; }
    //}

    //public class StripeTransaction_Checks
    //{
    //    public object address_line1_check { get; set; }
    //    public string address_postal_code_check { get; set; }
    //    public string cvc_check { get; set; }
    //}

    //public class StripeTransaction_Refunds
    //{
    //    public string _object { get; set; }
    //    public object[] data { get; set; }
    //    public bool has_more { get; set; }
    //    public string url { get; set; }
    //}

}
