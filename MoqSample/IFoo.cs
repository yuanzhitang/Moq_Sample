using System;

namespace MoqSample
{
	// Raising a custom event which does not adhere to the EventHandler pattern
	public delegate int MyEventHandler(int i, int b);

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

	public class Foo : IFoo
	{
		public Bar Bar { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
		public string Name { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
		public int Value { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

		public event MyEventHandler MyEvent;
		public event EventHandler FooEvent;

		public bool Add(int value)
		{
			throw new NotImplementedException();
		}

		public bool DoSomething(string value)
		{
			throw new NotImplementedException();
		}

		public bool DoSomething(int number, string value)
		{
			throw new NotImplementedException();
		}

		public string DoSomethingStringy(string value)
		{
			throw new NotImplementedException();
		}

		public int GetCount()
		{
			throw new NotImplementedException();
		}

		public bool Submit(ref Bar bar)
		{
			throw new NotImplementedException();
		}

		public bool TryParse(string value, out string outputValue)
		{
			throw new NotImplementedException();
		}
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
