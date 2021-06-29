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
        private readonly IDataRepository<Event> _dataRepository;
        private const string EVENT_NULL = "Event is null.";
        private const string EVENT_DOES_NOT_EXIST = "The Event doesn't not exist";
        private const string EVENT_DATA_INCORRECT = "Make sure you have filled in all the fields with the correct value and format.";

        public EventController(IDataRepository<Event> dataRepository)
        {
            _dataRepository = dataRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            IEnumerable<Event> employees = await _dataRepository.GetAll();
            return Ok(employees);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var eventData = await _dataRepository.Get(id);

            if (eventData == null)
            {
                return NotFound(EVENT_NULL);
            }

            return Ok(eventData);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Event eventData)
        {
            if (eventData == null)
            {
                return BadRequest(EVENT_NULL);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(EVENT_DATA_INCORRECT);
            }

            await _dataRepository.Add(eventData);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] Event eventData)
        {
            if (eventData == null)
            {
                return BadRequest(EVENT_NULL);
            }

            if (!ModelState.IsValid) 
            {
                return BadRequest(EVENT_DATA_INCORRECT);
            }

            var eventToUpdate = await _dataRepository.Get(id);
            if (eventToUpdate == null)
            {
                return NotFound(EVENT_DOES_NOT_EXIST);
            }

            await _dataRepository.Update(eventToUpdate, eventData);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var employee = await _dataRepository.Get(id);
            if (employee == null)
            {
                return NotFound(EVENT_DOES_NOT_EXIST);
            }

            await _dataRepository.Delete(employee);
            return Ok();
        }
    }
}
