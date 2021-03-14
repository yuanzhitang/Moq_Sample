using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MoqSample;
using System;

namespace UnitTest
{
	[TestClass]
	public class BasicTest
	{
		[TestMethod]
		[ExpectedException(typeof(Exception))]
		public void DoSomethingAlwaysThrowException()
		{
			var mockInstance = new Mock<IInterface>();

			mockInstance.Setup(i => i.DoSomething(It.IsAny<string>())).Throws(new Exception());

			mockInstance.Object.DoSomething("");
		}

		[TestMethod]
		public void ReturnMockedInstance()
		{
			var mockFactory = new Mock<Factory>();
			var mockInstance = new Mock<IInterface>();

			mockFactory.Setup(f => f.CreateInstance()).Returns(mockInstance.Object);

			var instance = mockFactory.Object.CreateInstance();

			Assert.AreEqual(mockInstance.Object, instance);
		}

		[TestMethod]
		public void ReturnRealInstance()
		{
			var mockFactory = new Mock<Factory>();
			var realInstance = new Instance();

			mockFactory.Setup(f => f.CreateInstance())
				.Returns(realInstance);

			var instance = mockFactory.Object.CreateInstance();

			Assert.AreEqual(realInstance, instance);
		}
	}
}
