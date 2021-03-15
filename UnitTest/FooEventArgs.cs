using System;

namespace UnitTest
{
	internal class FooEventArgs : EventArgs
	{
		private object fooValue;

		public FooEventArgs(object fooValue)
		{
			this.fooValue = fooValue;
		}
	}
}