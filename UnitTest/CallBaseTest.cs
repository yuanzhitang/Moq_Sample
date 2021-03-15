using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MoqSample;
using System.Collections.Generic;
using System.Linq;

namespace UnitTest
{
	[TestClass]
	public class CallBaseTest
	{
		[TestMethod]
		public void InvokeBaseMethodIfCallBaseEqualsToTrue()
		{
			var mock = new Mock<ProductRepository>() { CallBase = true };
			mock.Setup(r => r.GetAllProducts()).Returns(new List<Product>()
														{
															new Product(){Name="p1",Type="Type1"},
															new Product(){Name="p2",Type="Type2"},
															new Product(){Name="p3",Type="Type3"}
														});
			
			var result = mock.Object.GetProductsByType("Type1");
			
			Assert.AreEqual(1, result.Count());
		}
	}
}
