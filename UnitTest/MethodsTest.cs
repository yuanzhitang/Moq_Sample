using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MoqSample;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTest
{
	[TestClass]
	public class MethodsTest
	{
		[TestMethod]
		public void MockMethodWithSpecificArgument()
		{
			var mock = new Mock<IFoo>();
			mock.Setup(foo => foo.DoSomething("ping")).Returns(true);
			Assert.IsTrue(mock.Object.DoSomething("ping"));
			Assert.IsFalse(mock.Object.DoSomething(""));
		}

		[TestMethod]
		public void MockMethodWithOutArgument()
		{
			var mock = new Mock<IFoo>();

			var outString = "ack";
			// TryParse will return true, and the out argument will return "ack", lazy evaluated
			mock.Setup(foo => foo.TryParse("ping", out outString)).Returns(true);

			var outStr = string.Empty;
			Assert.IsTrue(mock.Object.TryParse("ping", out outStr));
			Assert.AreEqual(outString, outStr);
		}

		[TestMethod]
		public void MockMethodWithRefArgument()
		{
			var mock = new Mock<IFoo>();

			var instance = new Bar();
			// Only matches if the ref argument to the invocation is the same instance
			mock.Setup(foo => foo.Submit(ref instance)).Returns(true);

			Assert.IsTrue(mock.Object.Submit(ref instance));
			Bar bar = null;
			Assert.IsFalse(mock.Object.Submit(ref bar));
		}

		[TestMethod]
		public void MockMethod_With_LazyEvaluatingReturnValue()
		{
			var mock = new Mock<IFoo>();

			// access invocation arguments when returning a value
			mock.Setup(x => x.DoSomethingStringy(It.IsAny<string>()))
					.Returns((string s) => s.ToLower());
			var str = "OLDString";
			Assert.AreEqual("oldstring", mock.Object.DoSomethingStringy(str));
			// Multiple parameters overloads available


			// throwing when invoked with specific parameters
			mock.Setup(foo => foo.DoSomething("reset")).Throws<InvalidOperationException>();
			mock.Setup(foo => foo.DoSomething("")).Throws(new ArgumentException("command"));

			try
			{
				mock.Object.DoSomething("reset");
			}
			catch(InvalidOperationException)
			{
				Assert.IsTrue(true);
			}

			try
			{
				mock.Object.DoSomething("");
			}
			catch (ArgumentException)
			{
				Assert.IsTrue(true);
			}

			// lazy evaluating return value
			var count = 1;
			mock.Setup(foo => foo.GetCount()).Returns(() => count);

			// returning different values on each invocation
			mock = new Mock<IFoo>();
			var calls = 0;
			mock.Setup(foo => foo.GetCount())
				.Returns(() => calls)
				.Callback(() => calls++);
			// returns 0 on first invocation, 1 on the next, and so on
			Assert.AreEqual(0, mock.Object.GetCount());
			Assert.AreEqual(1, mock.Object.GetCount());
		}
	}
}
