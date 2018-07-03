using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Test
{
	public class ExternalHealthDamageTestData
	{

		public static IEnumerable<object[]> TestData
		{

			get
			{
				string[] csvlines = File.ReadAllLines("Damage.csv");

				var testCases = new List<object[]>();

				foreach (var csvLine in csvlines)
				{
					IEnumerable<int> values = csvLine.Split(',').Select(int.Parse);

					object[] testCase = values.Cast<object>().ToArray();

					testCases.Add(testCase);
				}

				return testCases;
			}
		}
	}
}
