namespace ActiveDirectory.Common
{
    public class Member
    {
        public string Id { get; set; }
        public string DisplayName { get; set; }
        public DateTimeOffset? CreatedDateTime { get; set; }
        public string OnPremisesSamAccountName { get; set; }
        public DateTimeOffset? OnPremisesLastSyncDateTime { get; set; }
        public string OnPremisesSecurityIdentifier { get; set; }
        public bool? OnPremisesSyncEnabled { get; set; }
        public string OnPremisesDomainName { get; set; }

    }
}
