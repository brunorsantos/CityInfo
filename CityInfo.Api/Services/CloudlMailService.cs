using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.Api.Services
{
    public class CloudMailService :IMailService
    {
        private string _mailFrom = "mailfrom@mail.com";
        private string _mailTo = "mailto@mail.com";

        public void Send(string subject, string message)
        {
            Debug.WriteLine($"Mail from {_mailFrom} to {_mailTo} with CloudMailService");
            Debug.WriteLine($"Subject {subject}");
            Debug.WriteLine($"message {message}");
        }

    }
}
