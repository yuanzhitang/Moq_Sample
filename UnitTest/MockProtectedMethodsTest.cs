using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.Protected;
using MoqSample;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTest
{
	[TestClass]
	public class MockProtectedMethodsTest
	{
		[TestMethod]
		public void MockProtectedMethodInAbstractClass()
		{
			var mock = new Mock<CommandBase>();

			mock.Protected().Setup<int>("Execute").Returns(99);
			mock.Protected().Setup<bool>("Execute", ItExpr.IsAny<string>()).Returns(false);

			Assert.AreEqual("99:False", mock.Object.PrintResult());
		}

		[TestMethod]
		public void MockProtectedMethodInChildClass()
		{
			var mock = new Mock<Command>();

			mock.Protected().Setup<int>("Execute").Returns(88);
			mock.Protected().Setup<bool>("Execute", ItExpr.IsAny<string>()).Returns(false);

			Assert.AreEqual("88:False", mock.Object.PrintResult());
		}
	}
}
