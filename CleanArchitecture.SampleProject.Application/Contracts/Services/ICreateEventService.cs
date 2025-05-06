using CleanArchitecture.SampleProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.SampleProject.Application.Contracts.Services
{
    public interface ICreateEventService
    {
        /// <summary>
        /// Create Event
        /// </summary>
        /// <param name="event"></param>
        /// <returns>created event</returns>
        Task<Event> CreateEvent(Event @event);
    }
}
