using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MoqSample
{
	public class ProductRepository
	{
		public virtual IEnumerable<Product> GetAllProducts()
		{
			return new List<Product>()
			{
				new Product(){Name="Product in Redis"}
			};
		}

		public virtual IEnumerable<Product> GetProductsByType(string type)
		{
			return GetAllProducts().Where(p => p.Type == type);
		}
	}

	public class Product
	{
		public string Name { get; set; }

		public string Type { get; set; }
	}
}
