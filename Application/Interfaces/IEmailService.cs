using Application.DTOs.Email;
using Domain.Settings;

namespace Application.Interfaces;

public interface IEmailService
{
    public MailSettings _mailSettings { get; }
    Task SendEmailAsync(EmailRequest emailRequest);
    string GenerateRandomNumber();
    Task SendOtpMail(string toEmail, string fromEmail, string otp);
    Task SendResetPassOtpMail(string toEmail, string fromEmail, string otp);
}