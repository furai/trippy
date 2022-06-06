using Mailjet.Client;
using Mailjet.Client.TransactionalEmails;
using Mailjet.Client.TransactionalEmails.Response;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace TrippyWeb.Services;

public class EmailSender : IEmailSender
{
    private readonly ILogger _logger;
    private readonly IWebHostEnvironment _env;
    private string? _apiKey;
    private string? _secretKey;

    public EmailSender(ILogger<EmailSender> logger, IWebHostEnvironment env)
    {
        _logger = logger;
        _env = env;
        _apiKey = Environment.GetEnvironmentVariable("MAILJET_API_KEY");
        _secretKey = Environment.GetEnvironmentVariable("MAILJET_SECRET_KEY");
    }

    public async Task SendEmailAsync(string toEmail, string subject, string message)
    {
        if (string.IsNullOrEmpty(_apiKey) || string.IsNullOrEmpty(_secretKey))
        {
            throw new Exception("Missing credentials for MailJet API. Please edit .env file and rebuild the application.");
        }

        await Execute(subject, message, toEmail);
    }

    public async Task Execute(string subject, string message, string toEmail)
    {
        var client = new MailjetClient(_apiKey, _secretKey) { };
        var email = new TransactionalEmailBuilder()
                .WithFrom(new SendContact("furai@thd.vg"))
                .WithSubject(subject)
                .WithHtmlPart(message)
                .WithTo(new SendContact(toEmail))
                .Build();


        if (!_env.IsDevelopment())
        {
            await client.SendTransactionalEmailAsync(email);
        }
    }
}
