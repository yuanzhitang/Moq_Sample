using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MoqSample;
using System.Collections.Generic;
using System.Linq;

namespace UnitTest
{
	[TestClass]
	public class MockBehaviorTest
	{
		[TestMethod]
		[ExpectedException(typeof(MockException))]
		public void MockBehaviorIsStrict_When_No_Setup_For_Virtual_Method_Then_Throw_MockException()
		{
			var mock = new Mock<ProductRepository>(MockBehavior.Strict);
			mock.Setup(r => r.GetAllProducts()).Returns(new List<Product>()
														{
															new Product(){Name="p1",Type="Type1"},
															new Product(){Name="p2",Type="Type2"},
															new Product(){Name="p3",Type="Type3"}
														});

			//No setup for virtual GetProductsByType method
			//Therefore, throws a MockException
			var result = mock.Object.GetProductsByType("Type1");
		}

		[TestMethod]
		public void MockBehaviorIsStrict_When_Has_Setup_For_All_Virtual_Method_Then_No_MockException()
		{
			var mock = new Mock<ProductRepository>(MockBehavior.Strict);
			mock.Setup(r => r.GetAllProducts()).Returns(new List<Product>()
														{
															new Product(){Name="p1",Type="Type1"},
															new Product(){Name="p2",Type="Type2"},
															new Product(){Name="p3",Type="Type3"}
														});

			mock.Setup(t => t.GetProductsByType(It.IsAny<string>()))
				.Returns(new List<Product>() 
				{ 
					new Product(),
					new Product()
				});

			var result = mock.Object.GetProductsByType("Type1");
			Assert.AreEqual(2, result.Count());
		}

		[TestMethod]
		public void MockBehaviorIsLoose_When_No_Setup_For_Virtual_Method_Then_No_Exception()
		{
			var mock = new Mock<ProductRepository>(MockBehavior.Loose);
			//No setup for virtual GetProductsByType method
			var result = mock.Object.GetProductsByType("Type1");

			Assert.AreEqual(0, result.Count());
		}
	}
}
