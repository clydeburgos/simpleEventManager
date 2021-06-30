using EventManager.App.Contracts.Requests;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventManager.App.Validators
{
    public class CreateEventRequestValidator : AbstractValidator<EventRequest>
    {
        public CreateEventRequestValidator()
        {
            RuleFor(x => x.EventName)
                .NotEmpty();
            RuleFor(x => x.EventDescription)
                .NotEmpty();
            RuleFor(x => x.StartDate)
                .LessThan(x => x.EndDate)
                .NotEmpty();
            RuleFor(x => x.EndDate)
                .GreaterThan(x => x.StartDate)
                .NotEmpty();
        }
    }

    public class UpdateEventRequestValidator : AbstractValidator<UpdateEventRequest>
    {
        public UpdateEventRequestValidator()
        {
            RuleFor(x => x.Identifier)
                .NotEmpty();
            RuleFor(x => x.EventName)
                .NotEmpty();
            RuleFor(x => x.EventDescription)
                .NotEmpty();
            RuleFor(x => x.StartDate)
                .LessThan(x => x.EndDate)
                .NotEmpty();
            RuleFor(x => x.EndDate)
                .GreaterThan(x => x.StartDate)
                .NotEmpty();
        }
    }
}
