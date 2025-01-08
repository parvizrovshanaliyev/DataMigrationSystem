using System;
using DataMigration.Domain.Common;

namespace DataMigration.UserManagement.Domain.Entities
{
    public class LoginHistory : Entity
    {
        public DateTime LoginTime { get; private set; }
        public string IpAddress { get; private set; }
        public string UserAgent { get; private set; }
        public bool IsSuccessful { get; private set; }
        public string FailureReason { get; private set; }

        private LoginHistory() { } // For EF Core

        public static LoginHistory Create(DateTime loginTime, string ipAddress = null, string userAgent = null)
        {
            return new LoginHistory
            {
                Id = Guid.NewGuid(),
                LoginTime = loginTime,
                IpAddress = ipAddress,
                UserAgent = userAgent,
                IsSuccessful = true
            };
        }

        public void MarkAsFailed(string reason)
        {
            Guard.AgainstEmptyString(reason, nameof(reason));
            
            IsSuccessful = false;
            FailureReason = reason;
        }

        public void SetIpAddress(string ipAddress)
        {
            IpAddress = ipAddress;
        }

        public void SetUserAgent(string userAgent)
        {
            UserAgent = userAgent;
        }
    }
} 