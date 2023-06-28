using System;

namespace EventSourcingDemo.Data.ReadModels
{
	public class ProductReadModel
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public decimal Price { get; set; }
	}
}