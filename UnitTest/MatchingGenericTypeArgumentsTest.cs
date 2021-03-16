using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace UnitTest
{
	[TestClass]
	public class MatchingGenericTypeArgumentsTest
	{
		Mock<IFunc> mock = null;
		[TestInitialize]
		public void TestInit()
		{
			mock = new Mock<IFunc>();
		}

		[TestMethod]
		public void MatchingAnyTypes_Using_Object()
		{
			//Generic arguments are matched using the usual type polymorphism rules, so if you want to match any type, you can simply use object as type argument in many cases:
			mock.Setup(m => m.M1<object>()).Returns(true);

			Assert.IsTrue(mock.Object.M1<BaseClass>());
		}

		[TestMethod]
		public void MatchingAnyTypes_Using_IsAnyType()
		{
			// matches any type argument:
			mock.Setup(m => m.M1<It.IsAnyType>()).Returns(true);
			Assert.IsTrue(mock.Object.M1<BaseClass>());
			Assert.IsTrue(mock.Object.M1<SubClass>());
		}

		[TestMethod]
		public void Matches_only_type_arguments_that_are_subtypes()
		{
			// matches only type arguments that are subtypes of / implement T:
			mock.Setup(m => m.M1<It.IsSubtype<BaseClass>>()).Returns(true);

			Assert.IsTrue(mock.Object.M1<BaseClass>());
			Assert.IsTrue(mock.Object.M1<SubClass>());
			Assert.IsFalse(mock.Object.M1<Person>());

			mock = new Mock<IFunc>();
			// use of type matchers is allowed in the argument list:
			mock.Setup(m => m.M2(It.IsAny<It.IsAnyType>())).Returns(true);
			Assert.IsTrue(mock.Object.M2(new SubClass()));
			Assert.IsTrue(mock.Object.M2(new Person()));

			mock = new Mock<IFunc>();
			mock.Setup(m => m.M2(It.IsAny<It.IsSubtype<BaseClass>>())).Returns(true);
			Assert.IsTrue(mock.Object.M2(new SubClass()));
			Assert.IsFalse(mock.Object.M2(new Person()));
		}
	}

	public interface IFunc
	{
		bool M1<T>();
		bool M2<T>(T arg);
	}

	public abstract class BaseClass { }
	public class SubClass : BaseClass { }
	public class Person { }
}
