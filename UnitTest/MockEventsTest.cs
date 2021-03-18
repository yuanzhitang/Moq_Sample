using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MoqSample;
using System;

namespace UnitTest
{
	[TestClass]
	public class MockEventsTest
	{
		[TestMethod]
		public void MockEvents()
		{
			var mock = new Mock<IFoo>();

			int result = 0;
			mock.Object.MyEvent += (a, b) =>
			{
				return result= a + b;
			};

			// Raise passing the custom arguments expected by the event delegate
			mock.Raise(foo => foo.MyEvent += null, 25, 25);

			Assert.AreEqual(50, result);
		}
	}
}
