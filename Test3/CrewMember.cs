using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Test3
{
	enum GenderType
	{
		Male,
		Female
	}
	class CrewMember
	{
		public string Name { get; }
		public GenderType Gender { get; }
		public List<CrewMember> Inferiors { get; }
		public CrewMember(string name, GenderType gender, List<CrewMember> inferiors)
		{
			Name = name;
			Gender = gender;
			Inferiors = inferiors;
		}
		public static bool operator ==(CrewMember mem1, CrewMember mem2)
		{
			return mem1.Equals(mem2);
		}
		public static bool operator !=(CrewMember mem1, CrewMember mem2)
		{
			return !mem1.Equals(mem2);
		}
		public override bool Equals(object obj)
		{
			if (obj is CrewMember cm)
			{
				return this.Name == cm.Name;
			}
			return false;
		}
		public override string ToString()
		{
			return Name;
		}
	}
}
