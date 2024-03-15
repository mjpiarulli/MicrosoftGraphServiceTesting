namespace ActiveDirectory.Common
{
    public class User : Member
    {
        public string Mail { get; set; }
        public bool? AccountEnabled { get; set; }
        public List<string> BusinessPhones { get; set; }
        public string City { get; set; }
        public string CompanyName { get; set; }
        public string Country { get; set; }
        public DateTimeOffset? DeletedDateTime { get; set; }
        public string Department { get; set; }
        public DateTimeOffset? HireDate { get; set; }
        public string EmployeeId { get; set; }
        public string EmployeeType { get; set; }
        public string FaxNumber { get; set; }
        public string GivenName { get; set; }
        public string JobTitle { get; set; }
        public DateTimeOffset? LastPasswordChangeDateTime { get; set; }
        public string MailNickname { get; set; }
        public string MobilePhone { get; set; }
        public string OfficeLocation { get; set; }
        public string OnPremisesDistinguishedName { get; set; }        
        public string OnPremisesImmutableId { get; set; }
        public string OnPremisesUserPrincipalName { get; set; }
        public string PostalCode { get; set; }
        public string PreferredDataLocation { get; set; }
        public List<string>? ProxyAddresses { get; set; }
        public string State { get; set; }
        public string StreetAddress { get; set; }
        public string Surname { get; set; }
        public string UsageLocation { get; set; }
    }
}
