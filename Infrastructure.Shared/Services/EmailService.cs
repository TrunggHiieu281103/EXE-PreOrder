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

    private string GenerateResetPassEmailBody(string toEmail, string otp)
    {
        return $@"
    <div style='font-family:Arial, sans-serif; background-color:#e3f2fd; padding:20px;'>
        <div style='max-width:600px; margin:auto; background:#ffffff; border-radius:8px; overflow:hidden; box-shadow:0 2px 8px rgba(0,0,0,0.15);'>
            
            <!-- Header với nền xanh + ảnh Gundam -->
            <div style='background-color:#0d47a1; text-align:center; padding:20px;'>
                
                <h2 style='color:white; margin:0;'>Password Reset</h2>
            </div>

            <!-- Banner hình Gundam -->
            <div>
                
            </div>

            <!-- Nội dung -->
            <div style='padding:20px;'>
                <p style='font-size:16px; color:#333;'>Hello <strong>{toEmail}</strong>,</p>
                <p style='font-size:15px; color:#555;'>We received a request to reset your password. Your OTP code is:</p>
                <div style='text-align:center; margin:20px 0;'>
                    <span style='display:inline-block; font-size:22px; font-weight:bold; color:#fff; background:#0d47a1; padding:10px 20px; border-radius:5px; letter-spacing:2px;'>{otp}</span>
                </div>
                <p style='font-size:14px; color:#777;'>⚠ This code will expire in 5 minutes.</p>
            </div>
        </div>
    </div>";
    }

    private string GenerateEmailBody(string toEmail, string otp)
    {
        return $@"
    <div style='font-family:Arial, sans-serif; background-color:#e3f2fd; padding:20px;'>
        <div style='max-width:600px; margin:auto; background:#ffffff; border-radius:8px; overflow:hidden; box-shadow:0 2px 8px rgba(0,0,0,0.15);'>
            
            <!-- Header với nền xanh + ảnh Gundam -->
            <div style='background-color:#0d47a1; text-align:center; padding:20px;'>
               
                <h2 style='color:white; margin:0;'>Welcome to Nhieu Thu Hay</h2>
            </div>

            <!-- Banner hình Gundam -->
            <div>
                
            </div>

            <!-- Nội dung -->
            <div style='padding:20px;'>
                <p style='font-size:16px; color:#333;'>Hello <strong>{toEmail}</strong>,</p>
                <p style='font-size:15px; color:#555;'>Thank you for registering an account. Your OTP code is:</p>
                <div style='text-align:center; margin:20px 0;'>
                    <span style='display:inline-block; font-size:22px; font-weight:bold; color:#fff; background:#0d47a1; padding:10px 20px; border-radius:5px; letter-spacing:2px;'>{otp}</span>
                </div>
                <p style='font-size:14px; color:#777;'>Please use this code to activate your account. The code will expire after 5 minutes.</p>
            </div>
        </div>
    </div>";
    }


}