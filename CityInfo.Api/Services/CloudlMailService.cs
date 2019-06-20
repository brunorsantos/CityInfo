using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.Api.Services
{
    public class CloudMailService :IMailService
    {
        private string _mailFrom = Startup.Configuration["mailsettings:mailFromAddress"];
        private string _mailTo = Startup.Configuration["mailsettings:mailToAddress"];

        public void Send(string subject, string message)
        {
            Debug.WriteLine($"Mail from {_mailFrom} to {_mailTo} with CloudMailService");
            Debug.WriteLine($"Subject {subject}");
            Debug.WriteLine($"message {message}");
        }

    }
}
