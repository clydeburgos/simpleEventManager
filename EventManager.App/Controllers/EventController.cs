using AutoMapper;
using EventManager.App.Contracts.Requests;
using EventManager.App.Contracts.Responses;
using EventManager.Application.Interfaces;
using EventManager.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventManager.App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IDataRepository<Event> _dataRepository;
        private const string EVENT_NULL = "Event is null.";
        private const string EVENT_DOES_NOT_EXIST = "The Event doesn't not exist";
        private const string EVENT_DATA_INCORRECT = "Make sure you have filled in all the fields with the correct value and format.";

        public EventController(IMapper mapper, IDataRepository<Event> dataRepository)
        {
            _mapper = mapper;
            _dataRepository = dataRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var eventsData = await _dataRepository.GetAll();
            var eventsResponse = _mapper.Map<List<EventResponse>>(eventsData);
            return Ok(eventsResponse);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var eventData = await _dataRepository.Get(id);
            if (eventData == null)
            {
                return NotFound(EVENT_NULL);
            }

            var eventResponse = _mapper.Map<EventResponse>(eventData);
            return Ok(eventResponse);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] EventRequest eventData)
        {
            var eventPayload = _mapper.Map<Event>(eventData);
            await _dataRepository.Add(eventPayload);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] EventRequest eventData)
        {
            var eventPayload = _mapper.Map<Event>(eventData);
            await _dataRepository.Update(eventPayload);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var eventData = await _dataRepository.Get(id);
            if (eventData == null)
            {
                return NotFound(EVENT_NULL);
            }

            await _dataRepository.Delete(id);
            return Ok();
        }
    }
}
