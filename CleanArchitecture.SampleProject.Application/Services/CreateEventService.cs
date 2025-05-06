using CleanArchitecture.SampleProject.Application.Contracts.Infrastructure;
using CleanArchitecture.SampleProject.Application.Contracts.Persistence;
using CleanArchitecture.SampleProject.Application.Contracts.Services;
using CleanArchitecture.SampleProject.Application.Exceptions;
using CleanArchitecture.SampleProject.Application.Models.Mail;
using CleanArchitecture.SampleProject.Domain.Entities;
using Microsoft.Extensions.Logging;
using System.Net;

namespace CleanArchitecture.SampleProject.Application.Services
{
    public class CreateEventService : ICreateEventService
    {
        private readonly IAsyncRepository<Event> _eventRepository;
        private readonly IEmailService _emailService;
        private readonly ILogger<CreateEventService> _logger;

        public CreateEventService(IAsyncRepository<Event> eventRepository, IEmailService emailService, ILogger<CreateEventService> logger)
        {
            _eventRepository = eventRepository ?? throw new ApiException($"Value cannot be null for the argument {nameof(eventRepository)}.", (int)HttpStatusCode.InternalServerError);
            _emailService = emailService ?? throw new ApiException($"Value cannot be null for the argument {nameof(emailService)}.", (int)HttpStatusCode.InternalServerError);
            _logger = logger ?? throw new ApiException($"Value cannot be null for the argument {nameof(logger)}.", (int)HttpStatusCode.InternalServerError);
        }
        public async Task<Event> CreateEvent(Event @event)
        {
            
            @event =  await _eventRepository.AddAsync(@event);

            //Sending email notification to admin address
            var email = new Email() { To = "deepak.mittal@hotmail.com", Body = $"A new event was created: {@event.Name}", Subject = "A new event was created" };

            try
            {
                await _emailService.SendEmail(email);
            }
            catch (Exception ex)
            {
                //this shouldn't stop the API from doing else so this can be logged
                _logger.LogError($"Mailing about event {@event.EventId} failed due to an error with the mail service: {ex.Message}");
            }
            return @event;
        }
    }
}
