using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MoqSample;
using System.Text.RegularExpressions;

namespace UnitTest
{
	[TestClass]
	public class MatchingArgumentsTest
	{
		[TestMethod]
		public void MockArguments()
		{
			var mock = new Mock<IFoo>();

			// any value
			mock.Setup(foo => foo.DoSomething(It.IsAny<string>())).Returns(true);
			Assert.IsTrue(mock.Object.DoSomething(""));
			Assert.IsTrue(mock.Object.DoSomething("value2"));


			// any value passed in a `ref` parameter (requires Moq 4.8 or later):
			mock.Setup(foo => foo.Submit(ref It.Ref<Bar>.IsAny)).Returns(true);
			Bar bar1 = null;
			Bar bar2 = null;
			Assert.IsTrue(mock.Object.Submit(ref bar1));
			Assert.IsTrue(mock.Object.Submit(ref bar2));

			// matching Func<int>, lazy evaluated
			mock.Setup(foo => foo.Add(It.Is<int>(i => i % 2 == 0))).Returns(true);
			Assert.IsTrue(mock.Object.Add(4));
			Assert.IsFalse(mock.Object.Add(3));


			// matching ranges
			mock.Setup(foo => foo.Add(It.IsInRange<int>(0, 10, Moq.Range.Inclusive))).Returns(true);
			Assert.IsTrue(mock.Object.Add(0));
			Assert.IsTrue(mock.Object.Add(10));
			Assert.IsFalse(mock.Object.Add(-1));
			Assert.IsFalse(mock.Object.Add(11));


			// matching regex
			mock.Setup(x => x.DoSomethingStringy(It.IsRegex("[a-d]+", RegexOptions.IgnoreCase))).Returns("foo");
			Assert.IsTrue("foo".Equals(mock.Object.DoSomethingStringy("abcd")));
			Assert.IsFalse("foo".Equals(mock.Object.DoSomethingStringy("xyz")));

		}
	}
}
