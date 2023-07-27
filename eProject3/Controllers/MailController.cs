using eProject3.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Net;

namespace eProject3.Controllers
{
    public class MailController : Controller
    {
        private IConfiguration Configuration;
        public MailController(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }
        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Contact(SendMail model)
        {
            //Read SMTP settings from AppSettings.json.
            string host = this.Configuration.GetValue<string>("Smtp:smtp.gmail.com");
            int port = this.Configuration.GetValue<int>("Smtp:587");
            string fromAddress = this.Configuration.GetValue<string>("Smtp:FromAddress");
            string userName = this.Configuration.GetValue<string>("Smtp:phanminhduc159@gmail.com");
            string password = this.Configuration.GetValue<string>("Smtp:yrseqepzdlzmljvf");

            using (MailMessage mm = new MailMessage(fromAddress, "phanminhduc159@gmail.com"))
            {
                mm.Subject = model.Subject;
                mm.Body = "Email: " + model.Email + "<br />" + model.Message;
                mm.IsBodyHtml = true;


                using (SmtpClient smtp = new SmtpClient())
                {
                    smtp.Host = "smtp.gmail.com";
                    smtp.EnableSsl = true;
                    NetworkCredential NetworkCred = new NetworkCredential("phanminhduc159@gmail.com", "yrseqepzdlzmljvf");
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = NetworkCred;
                    smtp.Port = 587; ;
                    smtp.Send(mm);
                    ViewBag.msg = "Email sent sucessfully.";
                }
            }

            return View();
        }
    }
}
