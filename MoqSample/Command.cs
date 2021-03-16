namespace MoqSample
{
	public abstract class CommandBase
	{
		public string PrintResult()
		{
			return $"{Execute()}:{Execute("")}";
		}

		protected abstract int Execute();

		protected abstract bool Execute(string arg);
	}


	public class Command : CommandBase
	{
		//protected virtual int Execute()
		//{
		//	return 100;
		//}

		//protected virtual bool Execute(string arg)
		//{
		//	return true;
		//}

		protected override int Execute()
		{
			return 100;
		}

		protected override bool Execute(string arg)
		{
			return true;
		}
	}
}
