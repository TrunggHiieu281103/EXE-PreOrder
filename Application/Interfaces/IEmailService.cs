using Application.DTOs.Email;

namespace Application.Interfaces;

public interface IEmailService
{
    Task SendEmailAsync(EmailRequest emailRequest);
}