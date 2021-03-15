using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MoqSample;
using System;
using System.Collections.Generic;
using System.Text;
using Range = Moq.Range;

namespace UnitTest
{
	[TestClass]
	public class VerifyTest
	{
		Mock<IFoo> mock = null;

		[TestInitialize]
		public void TestInit()
		{
			mock = new Mock<IFoo>();
		}

		[TestMethod]
		public void Verify()
		{
			mock.Object.DoSomething("ping");
			mock.Verify(foo => foo.DoSomething("ping"));

			// Verify with custom error message for failure
			mock.Verify(foo => foo.DoSomething("ping"), "When doing operation X, the service should be pinged always");
		}

		[TestMethod]
		public void MethodNeverBeCalled()
		{
			// Method should never be called
			mock.Verify(foo => foo.DoSomething("ping"), Times.Never());
		}

		[TestMethod]
		public void MethodCalledAtLeastOnceBeCalled()
		{
			mock.Object.DoSomething("ping");
			// Called at least once
			mock.Verify(foo => foo.DoSomething("ping"), Times.AtLeastOnce());
		}

		[TestMethod]
		public void PropertyGet()
		{
			var name = mock.Object.Name;
			// Verify getter invocation, regardless of value.
			mock.VerifyGet(foo => foo.Name);
		}

		[TestMethod]
		public void PropertySet()
		{
			mock.Object.Name = "dummy";
			// Verify setter invocation, regardless of value.
			mock.VerifySet(foo => foo.Name);

			mock.Object.Name = "foo";
			// Verify setter called with specific value
			mock.VerifySet(foo => foo.Name = "foo");

			mock.Object.Value = 4;
			// Verify setter with an argument matcher
			mock.VerifySet(foo => foo.Value = It.IsInRange(1, 5, Range.Inclusive));
		}

		[TestMethod]
		public void VerifyEventAddOrRemove()
		{
			mock.Object.FooEvent += null;
			// Verify event accessors (requires Moq 4.13 or later):
			mock.VerifyAdd(foo => foo.FooEvent += It.IsAny<EventHandler>());

			mock.Object.FooEvent -= null;
			mock.VerifyRemove(foo => foo.FooEvent -= It.IsAny<EventHandler>());

			// Verify that no other invocations were made other than those already verified (requires Moq 4.8 or later)
			mock.VerifyNoOtherCalls();
		}
	}
}
