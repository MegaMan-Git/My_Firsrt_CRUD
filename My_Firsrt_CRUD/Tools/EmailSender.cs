using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;
using FluentEmail.Core;
using FluentEmail.Smtp;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace My_Firsrt_CRUD.Tools
{
    public interface IEmailSender
    {
        public  Task SendEmailAsync(EmailModel model);
    }

    public class EmailSender : IEmailSender
    {
        
        public async Task SendEmailAsync(EmailModel model)
        {
            var sender = new SmtpSender(() => new SmtpClient("smtp.gmail.com")
            {
                UseDefaultCredentials = false,
                Port = 587,
                EnableSsl = true,
                Credentials = new NetworkCredential("your email", "your password")
            });

            Email.DefaultSender = sender;

            var email = await Email
                 .From("your email", "MVC(CRUD) سایت")
                 .To(model.To)
                 .Subject(model.Subject)
                 .Body(model.Body, isHtml: true)
                 .SendAsync();


            if (!email.Successful)
            {
                foreach (var err in email.ErrorMessages)
                    Console.WriteLine(err);
            }
            
        }

    }
    public class EmailModel
    {

        public EmailModel(string to, string subject, string body)
        {
            To = to;
            Subject = subject;
            Body = body;
        }
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }


}