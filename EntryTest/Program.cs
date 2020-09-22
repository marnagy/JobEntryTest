using System;
using System.Collections.Generic;

namespace Test1
{
	class Program
	{
		static void Main(string[] args)
		{
			// values generating
			int l = 1_000_000;
			var rand = new Random(64);

			var values = new int[l];
			for (int i = 0; i < l; i++)
			{
				values[i] = rand.Next(int.MinValue, int.MaxValue);
			}

			// using Dictionary/HashTable to remember if number occured
			var table = new Dictionary<int, bool>(l);
			foreach (var num in values)
			{
				// test if number already occured
				if (table.ContainsKey(num))
				{
					table[num] = true;
				}
				else
				{
					table.Add(num, false);
				}
			}

			foreach (var keyValuePair in table)
			{
				if (keyValuePair.Value)
				{
					Console.WriteLine( $"Number {keyValuePair.Key} occured multiple times." );
				}
			}

		}
	}
}
