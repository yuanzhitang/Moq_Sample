using System;

namespace MoqSample
{
	// Raising a custom event which does not adhere to the EventHandler pattern
	public delegate void MyEventHandler(int i, bool b);

	public interface IFoo
	{
		Bar Bar { get; set; }
		string Name { get; set; }
		int Value { get; set; }


		bool DoSomething(string value);
		bool DoSomething(int number, string value);
		string DoSomethingStringy(string value);
		bool TryParse(string value, out string outputValue);
		bool Submit(ref Bar bar);
		int GetCount();
		bool Add(int value);

		event MyEventHandler MyEvent;
		event EventHandler FooEvent;
	}

	public class Bar
	{
		public virtual Baz Baz { get; set; }
		public virtual bool Submit() { return false; }
	}

	public class Baz
	{
		public virtual string Name { get; set; }
	}
}
