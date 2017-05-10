using System;
using System.Collections.Generic;

namespace Infrastructure.CrossCutting.Helper
{
    public static class Email
    {
        public class EmailParameter
        {
            public List<string> Emails { get; set; }
            public List<string> Attachments { get; set; }
            public string MailFrom { get; set; }
            public string NameFrom { get; set; }
            public string Body { get; set; }
            public string Title { get; set; }
        }

        public static bool Send(EmailParameter parameter)
        {
            try
            {
                return true;
            }
            catch(Exception)
            {
                return false;
            }
        }
    }
}
