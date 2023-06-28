using System;
using System.Text.Json;
using EventSourcingDemo.Data.AuditLogContext;
using EventSourcingDemo.Events;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EventSourcingDemo.Services
{
	public class EventReplayService
	{
		readonly AuditLogDbContext _auditLogDbContext;
		readonly IMediator _mediator;

		public EventReplayService(AuditLogDbContext auditLogDbContext, IMediator mediator)
		{
			_auditLogDbContext = auditLogDbContext;
			_mediator = mediator;
		}

		public async Task<IEnumerable<AuditLogEvent>> GetEventsAsync()
		{
			return await _auditLogDbContext.AuditLogEvents.ToListAsync();
		}
		
		public async Task ReplayEventsAsync()
		{
			var events = await _auditLogDbContext.AuditLogEvents.ToListAsync();
			
			foreach (var @event in events)
			{
				
				var eventType = @event.EventType;
				
				switch (eventType)
				{
					case "ProductCreatedEvent":
					{
						var productCreatedEvent = JsonSerializer.Deserialize<ProductCreatedEvent>(@event.EventData);
						
						// Send the event to the ProductCreatedEventHandler and set the IsReplay flag to true
						productCreatedEvent.IsReplay = true;
						await _mediator.Publish(productCreatedEvent);
						
						break;
					}
					default:
						break;
				}
				
				
			}
		}
	}
}