using System;
using System.Text.Json;
using EventSourcingDemo.Data;
using EventSourcingDemo.Data.AuditLogContext;
using EventSourcingDemo.Data.ReadModels;
using MediatR;

namespace EventSourcingDemo.Events
{
	public class ProductCreatedEvent: INotification
	{
		public int ProductId { get; set; }
		public string Name { get; set; }
		public decimal Price { get; set; }
		public bool IsReplay { get; set; }
	}

	public class ProductCreatedEventHandler: INotificationHandler<ProductCreatedEvent>
	{
		AppReadDbContext _dbContext;
		AuditLogDbContext _auditLogDbContext;

		public ProductCreatedEventHandler(AppReadDbContext appReadDbContext, AuditLogDbContext auditLogDbContext)
		{
			_dbContext         = appReadDbContext;
			_auditLogDbContext = auditLogDbContext;
		}

		public async Task Handle(ProductCreatedEvent notification, CancellationToken cancellationToken)
		{
			// Create a new Product Read Model for the Query side
			var productReadModel = new ProductReadModel
			{
				Name  = notification.Name,
				Price = notification.Price
			};

			_dbContext.Products.Add(productReadModel);
			await _dbContext.SaveChangesAsync(cancellationToken);
			
			// If this is a replay, we don't want to add another Audit Log Event
			if (notification.IsReplay)
				return;
			
			// Audit Log Event for the Audit Log side
			var auditLogEvent = new AuditLogEvent
			{
				EventType = nameof(ProductCreatedEvent),
				Timestamp = DateTime.UtcNow,
				User      = "System",
				EventData = JsonSerializer.Serialize(notification)
			};
			
			await _auditLogDbContext.AuditLogEvents.AddAsync(auditLogEvent, cancellationToken);
			await _auditLogDbContext.SaveChangesAsync(cancellationToken);

			// You can also perform other actions here, like sending an email, updating a cache, logging, etc.
		}
	}
}