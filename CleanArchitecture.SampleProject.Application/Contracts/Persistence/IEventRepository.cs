﻿using CleanArchitecture.SampleProject.Domain.Entities;

namespace CleanArchitecture.SampleProject.Application.Contracts.Persistence
{
    public interface IEventRepository : IAsyncRepository<Event>
    {
        Task<bool> IsEventNameAndDateUnique(string name, DateTime eventDate);
    }
}
