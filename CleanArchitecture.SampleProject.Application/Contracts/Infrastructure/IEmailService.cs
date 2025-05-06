using CleanArchitecture.SampleProject.Application.Models.Mail;

namespace CleanArchitecture.SampleProject.Application.Contracts.Infrastructure
{
    public interface IEmailService
    {
        Task<bool> SendEmail(Email email);
    }
}
