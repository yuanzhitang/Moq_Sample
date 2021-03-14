using System;
using System.Collections.Generic;
using System.Text;

namespace MoqSample
{
	public class Factory
	{
		public virtual IInterface CreateInstance()
		{
			return new Instance();
		}
	}
}
