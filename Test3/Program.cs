using System;
using System.Collections.Generic;

namespace Test3
{
	class Program
	{
		static void Main(string[] args)
		{
			// init spaceship
			var ship = InitShip();

			// all inferiors
			var inferiors = GetInferiors(new CrewMember("Beverley Crusher", GenderType.Female, null), ship);
			foreach (var member in inferiors)
			{
				Console.WriteLine(member);
			}

			Console.WriteLine("---------------");

			// infecting captain
			var posInfections = PossibleInfections(new CrewMember("Julian Bashir", GenderType.Male, null), ship);
			foreach (var member in posInfections)
			{
				Console.WriteLine(member);
			}
		}
		static SpaceShip InitShip()
		{
			var AlexRoz = new CrewMember("Alexander Rozhenko", GenderType.Male, new List<CrewMember>());
			var JulBash = new CrewMember("Julian Bashir", GenderType.Male, new List<CrewMember>());

			var TashYar = new CrewMember("Tasha Yar", GenderType.Female, new List<CrewMember>());
			var KEhley = new CrewMember("K'Ehleyr", GenderType.Female, new List<CrewMember>() { AlexRoz });
			var WesCrus = new CrewMember("Wesley Crusher", GenderType.Male, new List<CrewMember>());
			var AlyOga = new CrewMember("Alyssa Ogawa", GenderType.Female, new List<CrewMember>() { JulBash });

			var Worf = new CrewMember("Worf son of Mog", GenderType.Male, new List<CrewMember>(){ TashYar, KEhley });
			var Guinan = new CrewMember("Guinan", GenderType.Female, new List<CrewMember>());
			var BevCrus = new CrewMember("Beverley Crusher", GenderType.Female, new List<CrewMember>(){ WesCrus, AlyOga });
			var Lwaxa = new CrewMember("Lwaxana Troi", GenderType.Female, new List<CrewMember>());
			var Regi = new CrewMember("Reginald Barkley", GenderType.Male, new List<CrewMember>());
			var MrData = new CrewMember("Mr. Data", GenderType.Male, new List<CrewMember>());
			var Miles = new CrewMember("Miles O'Brien", GenderType.Male, new List<CrewMember>());

			var WilRik = new CrewMember("William Riker", GenderType.Male, new List<CrewMember>(){ Worf, Guinan, BevCrus });
			var Deana = new CrewMember("Deana Troi", GenderType.Female, new List<CrewMember>(){ Lwaxa, Regi });
			var Jordi = new CrewMember("Jordi La Forge", GenderType.Male, new List<CrewMember>(){ MrData, Miles });

			var Picard = new CrewMember("Jean Luc Pickard", GenderType.Male, new List<CrewMember>(){ WilRik, Deana, Jordi });
			return new SpaceShip("Enterprise", Picard);
		}
		static CrewMember[] GetInferiors(CrewMember member, SpaceShip ship)
		{
			var res = new List<CrewMember>();
			inferiors(member, ship.Captain, res);
			return res.ToArray();
		}
		static void inferiors(CrewMember member, CrewMember currentMember, List<CrewMember> result, bool retAll = false)
		{
			if (!retAll)
			{
				if (member == currentMember)
				{
					foreach (var inferior in currentMember.Inferiors)
					{
						inferiors(member, inferior, result, retAll: true);
					}
				}
				else
				{
					foreach (var inferior in currentMember.Inferiors)
					{
						inferiors(member, inferior, result);
					}
					return;
				}
			}
			else
			{
				result.Add(currentMember);
				foreach (var inferior in currentMember.Inferiors)
				{
					inferiors(member, inferior, result, retAll: true);
				}
			}
		}
		static CrewMember[] PossibleInfections(CrewMember member, SpaceShip ship)
		{
			var res = new List<CrewMember>();
			possibleInfections(member, ship.Captain, res);
			return res.ToArray();
		}

		private static bool possibleInfections(CrewMember member, CrewMember currMember, List<CrewMember> res)
		{
			if ( currMember == member)
			{
				return true;
			}

			foreach (var inf in currMember.Inferiors)
			{
				bool addYourself = possibleInfections(member, inf, res);
				if (addYourself)
				{
					res.Add(currMember);
					return addYourself;
				}
			}
			return false;
		}
	}
}
