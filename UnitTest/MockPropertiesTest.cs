using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MoqSample;

namespace UnitTest
{
	[TestClass]
	public class MockPropertiesTest
	{
		[TestMethod]
		public void MockProperties()
		{
			var mock = new Mock<IFoo>();
			mock.Setup(foo => foo.Name).Returns("bar");
			Assert.AreEqual("bar", mock.Object.Name);

			// auto-mocking hierarchies (a.k.a. recursive mocks)
			mock.Setup(foo => foo.Bar.Baz.Name).Returns("baz");
			Assert.AreEqual("baz", mock.Object.Bar.Baz.Name);

			// expects an invocation to set the value to "foo"
			mock.SetupSet(foo => foo.Name = "foo");

			mock.Object.Name = "foo";

			// or verify the setter directly
			mock.VerifySet(foo => foo.Name = "foo");

			// start "tracking" sets/gets to this property
			mock.SetupProperty(f => f.Name);

			// alternatively, provide a default value for the stubbed property
			mock.SetupProperty(f => f.Name, "foo");

			// Now you can do:
			IFoo foo = mock.Object;
			// Initial value was stored
			Assert.AreEqual("foo", foo.Name);

			// New value set which changes the initial value
			foo.Name = "bar";
			Assert.AreEqual("bar", foo.Name);
			
			// Stub all properties on a mock(not available on Silverlight)
			mock.SetupAllProperties();

			Assert.AreEqual(null, foo.Name);
		}
	}
}
