using System;
using System.Collections.Generic;
using System.Text;

namespace MoqSample
{
	public class Instance : IInterface
	{
		public bool DoSomething(string parameter)
		{
			if(string.IsNullOrEmpty(parameter))
			{
				return false;
			}

			return true;
		}
	}
}
