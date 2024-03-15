using ActiveDirectory.Common;

namespace ADService.Mapping
{
    public static class GraphUser
    {
        public static User MapGraphUserToUser(Microsoft.Graph.User user)
        {
            return new User
            {
                Id = user.Id,
                DisplayName = user.DisplayName,
                Mail = user.Mail,
                AccountEnabled = user.AccountEnabled,
                BusinessPhones = user.BusinessPhones.ToList(),
                City = user.City,
                CompanyName = user.CompanyName,
                Country = user.Country,
                CreatedDateTime = user.CreatedDateTime,
                DeletedDateTime = user.DeletedDateTime,
                Department = user.Department,
                HireDate = user.HireDate,
                EmployeeId = user.EmployeeId,
                EmployeeType = user.EmployeeType,
                FaxNumber = user.FaxNumber,
                GivenName = user.GivenName,
                JobTitle = user.JobTitle,
                LastPasswordChangeDateTime = user.LastPasswordChangeDateTime,
                MailNickname = user.MailNickname,
                MobilePhone = user.MobilePhone,
                OfficeLocation = user.OfficeLocation,
                OnPremisesDistinguishedName = user.OnPremisesDistinguishedName,
                OnPremisesDomainName = user.OnPremisesDomainName,
                OnPremisesImmutableId = user.OnPremisesImmutableId,
                OnPremisesLastSyncDateTime = user.OnPremisesLastSyncDateTime,
                OnPremisesSamAccountName = user.OnPremisesSamAccountName,
                OnPremisesSecurityIdentifier = user.OnPremisesSecurityIdentifier,
                OnPremisesSyncEnabled = user.OnPremisesSyncEnabled,
                OnPremisesUserPrincipalName = user.OnPremisesUserPrincipalName,
                PostalCode = user.PostalCode,
                PreferredDataLocation = user.PreferredDataLocation,
                ProxyAddresses = user.ProxyAddresses?.ToList(),
                State = user.State,
                StreetAddress = user.StreetAddress,
                Surname = user.Surname,
                UsageLocation = user.UsageLocation
            };
        }
    }
}
