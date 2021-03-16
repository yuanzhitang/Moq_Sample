using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MoqSample;
using System;
using System.Collections.Generic;

namespace UnitTest
{
	[TestClass]
	public class DefaultValueProviderTest
	{
		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void MockWithDefaultValueEmpty()
		{
			var mock = new Mock<IFoo> { DefaultValue = DefaultValue.Empty };
			var name = mock.Object.Name;

			Assert.IsNull(name);

			// this property access would return a new non mock of Bar as it's not "mock-able"
			// Because DefaultValue is Empty
			Bar value = mock.Object.Bar;
			
			// The below code will throw the exception:
			// Test method UnitTest.DefaultValueProviderTest.MockWithDefaultValueEmpty threw exception:
			// System.ArgumentException: Object instance was not created by Moq. (Parameter 'mocked')
			var barMock = Mock.Get(value);
			barMock.Setup(b => b.Submit()).Returns(true);
		}

		[TestMethod]
		public void MockWithDefaultValueMock()
		{
			var mock = new Mock<IFoo> { DefaultValue = DefaultValue.Mock };
			var name = mock.Object.Name;

			Assert.IsNull(name);

			// this property access would return a new mock of Bar as it's "mock-able"
			Bar value = mock.Object.Bar;

			// the returned mock is reused, so further accesses to the property return 
			// the same mock instance. this allows us to also use this instance to 
			// set further expectations on it if we want
			var barMock = Mock.Get(value);
			barMock.Setup(b => b.Submit()).Returns(true);

			Assert.IsTrue(mock.Object.Bar.Submit());
		}

		[TestMethod]
		public void CustomDefaultValueProvider()
		{
			var mock = new Mock<IFoo> { DefaultValueProvider = new MyEmptyDefaultValueProvider() };
			var name = mock.Object.Name;

			Assert.AreEqual("?", name);
		}
	}

	class MyEmptyDefaultValueProvider : LookupOrFallbackDefaultValueProvider
	{
		public MyEmptyDefaultValueProvider()
		{
			base.Register(typeof(string), (type, mock) => "?");
			base.Register(typeof(List<>), (type, mock) => Activator.CreateInstance(type));
		}
	}
}


