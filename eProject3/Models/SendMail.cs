using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Threading.Tasks;

namespace eProject3.Models
{
    
    public class SendMail
    {
        public string Email { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
    }
}

