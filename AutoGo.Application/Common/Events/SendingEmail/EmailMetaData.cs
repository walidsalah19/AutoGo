using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoGo.Application.Common.Events.SendingEmail
{
    public class EmailMetaData
    {
        public string ToAddress { get; set; }
        public string Subject { get; set; }
        public string? Body { get; set; }
        public string? AttechmentPath { get; set; }

        public EmailMetaData(string toAddress, string subject, string? body = "", string? attechmentPath = "")
        {
            ToAddress = toAddress;
            Subject = subject;
            Body = body;
            AttechmentPath = attechmentPath;
        }
    }
}
