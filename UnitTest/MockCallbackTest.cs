using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MoqSample;
using System;
using System.Collections.Generic;

namespace UnitTest
{
	[TestClass]
	public class MockCallbackTest
	{
		[TestMethod]
		public void MockCallback()
		{
			var mock = new Mock<IFoo>();
			var calls = 0;
			var callArgs = new List<string>();

			mock.Setup(foo => foo.DoSomething("ping"))
				.Returns(true)
				.Callback(() => calls++);

			mock.Object.DoSomething("ping");
			Assert.AreEqual(1, calls);

			// access invocation arguments
			mock.Setup(foo => foo.DoSomething(It.IsAny<string>()))
				.Returns(true)
				.Callback((string s) => callArgs.Add(s));
			mock.Object.DoSomething("args1");
			Assert.IsTrue(callArgs.Contains("args1"));

			// alternate equivalent generic method syntax
			mock.Setup(foo => foo.DoSomething(It.IsAny<string>()))
				.Returns(true)
				.Callback<string>(s => callArgs.Add(s));
			mock.Object.DoSomething("args2");
			Assert.IsTrue(callArgs.Contains("args2"));

			// access arguments for methods with multiple parameters
			mock.Setup(foo => foo.DoSomething(It.IsAny<int>(), It.IsAny<string>()))
				.Returns(true)
				.Callback<int, string>((i, s) => callArgs.Add(s));
			mock.Object.DoSomething(1, "args3");
			Assert.IsTrue(callArgs.Contains("args3"));

			// callbacks can be specified before and after invocation
			mock.Setup(foo => foo.DoSomething("ping"))
				.Callback(() => Console.WriteLine("Before returns"))
				.Returns(true)
				.Callback(() => Console.WriteLine("After returns"));
			
			mock.Object.DoSomething("ping");



			mock.Setup(foo => foo.Submit(ref It.Ref<Bar>.IsAny))
				.Callback(new SubmitCallback((ref Bar bar) => Console.WriteLine("Submitting a Bar!")));

			Bar bar = null;
			mock.Object.Submit(ref bar);

			// returning different values on each invocation
			mock = new Mock<IFoo>();
			calls = 0;
			mock.Setup(foo => foo.GetCount())
				.Callback(() => calls++)
				.Returns(() => calls);
			
			var count = mock.Object.GetCount();
			Assert.AreEqual(1, count);

			// access invocation arguments and set to mock setup property
			mock.SetupProperty(foo => foo.Bar,new Bar());
			mock.SetupProperty(foo => foo.Bar.Baz, new Baz());
			mock.Setup(foo => foo.DoSomething(It.IsAny<string>()))
				.Callback((string s) => mock.Object.Bar.Baz.Name = s)
				.Returns(true);

			mock.Object.DoSomething("BazName");
			Assert.AreEqual("BazName", mock.Object.Bar.Baz.Name);
		}

		// callbacks for methods with `ref` / `out` parameters are possible but require some work (and Moq 4.8 or later):
		delegate void SubmitCallback(ref Bar bar);
	}
}
