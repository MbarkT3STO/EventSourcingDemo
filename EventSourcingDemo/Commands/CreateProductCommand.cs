using System;
using EventSourcingDemo.Data;
using EventSourcingDemo.Domain;
using EventSourcingDemo.Events;
using MediatR;

namespace EventSourcingDemo.Commands
{
	public class CreateProductCommand: IRequest<int>
	{
		public string Name { get; set; }
		public decimal Price { get; set; }
	}

	public class CreateProductCommandHandler: IRequestHandler<CreateProductCommand, int>
	{
		AppWriteDbContext _dbContext;
		IMediator _mediator;

		public CreateProductCommandHandler(AppWriteDbContext appWriteDbContext, IMediator mediator)
		{
			_dbContext = appWriteDbContext;
			_mediator  = mediator;
		}

		public async Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
		{
			var product = new Product
			{
				Name  = request.Name,
				Price = request.Price
			};

			_dbContext.Products.Add(product);
			await _dbContext.SaveChangesAsync(cancellationToken);
			
			// Reload the product to get the Id
			await _dbContext.Entry(product).ReloadAsync(cancellationToken);

			var productCreatedEvent = new ProductCreatedEvent
			{
				ProductId = product.Id,
				Name      = product.Name,
				Price     = product.Price
			};

			await _mediator.Publish(productCreatedEvent, cancellationToken);

			return product.Id;
		}
	}
}