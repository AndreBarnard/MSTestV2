using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Test
{
	[TestClass]
	public class Assembly
	{
		[AssemblyInitialize]
		public static void AssemblyInit(TestContext context)
		{
			Console.WriteLine("AssemblyInit");
		}

		[AssemblyCleanup]
		public static void AssemblyClean()
		{
			Console.WriteLine("AssemblyClean");
		}
	}
}
