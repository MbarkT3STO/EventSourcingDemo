using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventSourcingDemo.Data;
using EventSourcingDemo.Data.ReadModels;
using EventSourcingDemo.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EventSourcingDemo.Queries;

public class GetProductsQuery : IRequest<IEnumerable<ProductReadModel>>
{
	
}


public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, IEnumerable<ProductReadModel>>
{
	readonly AppReadDbContext _dbContext;
	
	public GetProductsQueryHandler(AppReadDbContext dbContext)
	{
		_dbContext = dbContext;
	}
	
	public async Task<IEnumerable<ProductReadModel>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
	{
		return await _dbContext.Products.ToListAsync(cancellationToken);
	}
}
