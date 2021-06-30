using AutoMapper;
using EventManager.App.Contracts.Requests;
using EventManager.App.Contracts.Responses;
using EventManager.Domain.Entities;
using System;


namespace EventManager.App.Configuration
{
    public class MappingConfiguration : Profile
    {
        public MappingConfiguration()
        {
            CreateMap<EventRequest, Event>().ReverseMap();
            CreateMap<EventResponse, Event>().ReverseMap();
        }
    }
}
