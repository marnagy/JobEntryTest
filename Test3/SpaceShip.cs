using System;
using System.Collections.Generic;
using System.Text;

namespace Test3
{
	class SpaceShip
	{
		public string Name { get; }
		public CrewMember Captain { get; }
		public SpaceShip(string name, CrewMember captain)
		{
			Name = name;
			Captain = captain;
		}
	}
}
