using System;
using SendGrid;
using Microsoft.AspNetCore.Identity.UI.Services;
using SendGrid.Helpers.Mail;


namespace COMP2139_labs.Services
{
	public class EmailSender : IEmailSender
	{

		private readonly string _sendGridKey;
		public EmailSender(IConfiguration configuration)
		{
			_sendGridKey = configuration["SendGrid:ApiKey"];
		}

		public async Task SendEmailAsync(string email, string subject, string htmlMessage)
		{
			var client = new SendGridClient(_sendGridKey);
			var from = new EmailAddress("pornpajee.sunkkadithee@georgebrown.ca");
			var to = new EmailAddress(email);
			var msg = MailHelper.CreateSingleEmail(from, to, subject, "", htmlMessage);
			await client.SendEmailAsync(msg);


		}
		
	}
}

