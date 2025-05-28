using Application.DTOs.Email;
using Application.Exceptions;
using Application.Interfaces;
using AutoMapper.Internal;
using Domain.Settings;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;

namespace Infrastructure.Shared.Services;

public class EmailService : IEmailService
{
    public MailSettings _mailSettings { get; }
    public ILogger<EmailService> _logger { get; }
    
    public EmailService(IOptions<MailSettings> mailSettings,ILogger<EmailService> logger)
    {
        _mailSettings = mailSettings.Value;
        _logger = logger;
    }
    
    
    public async Task  SendEmailAsync(EmailRequest emailRequest)
    {
        try
        {
            // create message
            var email = new MimeMessage();
            email.Sender = new MailboxAddress(_mailSettings.DisplayName, emailRequest.From ?? _mailSettings.EmailFrom);
            email.To.Add(MailboxAddress.Parse(emailRequest.To));
            email.Subject = emailRequest.Subject;
            var builder = new BodyBuilder();
            builder.HtmlBody = emailRequest.Body;
            email.Body = builder.ToMessageBody();
            using var smtp = new SmtpClient();
            smtp.Connect(_mailSettings.SmtpHost, _mailSettings.SmtpPort, SecureSocketOptions.StartTls);
            smtp.Authenticate(_mailSettings.SmtpUser, _mailSettings.SmtpPass);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);

        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex.Message, ex);
            throw new ApiException(ex.Message);
        }
    }

    // 1) Hàm tạo OTP 6 chữ số
    public string GenerateRandomNumber()
    {
        Random random = new Random();
        return random.Next(0, 1000000).ToString("D6");
    }

    // 2) Hàm tạo nội dung email (HTML)
    private string GenerateEmailBody(string toEmail, string otp)
    {
        return $@"
                <div style='font-family:Arial;'>
                    <h3>Hello {toEmail},</h3>
                    <p>Thank you for registering an account. Your OTP code is:<strong>{otp}</strong></p>
                    <p>Please use this code to activate your account. The code will expire after 5 minutes.</p>
                </div>";
    }

    // 3) Hàm gửi OTP qua email
    public async Task SendOtpMail(string toEmail, string fromEmail, string otp)
    {
        var mailRequest = new EmailRequest
        {
            To = toEmail,
            Subject = "Thanks for registering : OTP",
            Body = GenerateEmailBody(toEmail, otp),
            From = fromEmail,
        };

        await SendEmailAsync(mailRequest);
    }
    private string GenerateResetPassEmailBody(string toEmail, string otp)
    {
        return $@"
                <div style='font-family:Arial;'>
                    <h3>Hello {toEmail},</h3>
                    <p>Please confirm to reset your password. Your OTP code is:<strong>{otp}</strong></p>
                    <p>The code will expire after 5 minutes.</p>
                </div>";
    }

    // 3) Hàm gửi OTP qua email
    public async Task SendResetPassOtpMail(string toEmail, string fromEmail, string otp)
    {
        var mailRequest = new EmailRequest
        {
            To = toEmail,
            Subject = "Reset password : OTP",
            Body = GenerateResetPassEmailBody(toEmail, otp),
            From = fromEmail
        };

        await SendEmailAsync(mailRequest);
    }
}