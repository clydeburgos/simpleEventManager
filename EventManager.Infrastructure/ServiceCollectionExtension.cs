using EventManager.Application.Interfaces;
using EventManager.Domain.Entities;
using EventManager.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddEventServices(this IServiceCollection services, IConfiguration Configuration)
        {
            //move to Infrastructure
            services.AddDbContext<EventContext>(opts => opts.UseSqlServer(Configuration["ConnectionString:EventDB"]));
            services.AddScoped<IDataRepository<Event>, EventService>();
            return services;
        }
    }
}
