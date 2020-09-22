using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Text.Json;

namespace Test2
{
	class Vehicle
	{
		public string Name {get;}
		//public string Model {get;set;}
		//public string Manufacturer {get;set;}
		//public int Cost {get;set;}
		//public int Length {get;set;}
		//public int MaxSpeed {get;set;}
		//public int Crew {get;set;}
		//public int Passengers {get;set;}
		//public int CargoCapacty {get;set;}
		//public string Consumables {get;set;}
		//public string Class {get;set;}
		public Person[] Pilots {get;}
		public Vehicle(string name, Person[] pilots)
		{
			Name = name;
			Pilots = pilots;
		}

		public static Vehicle FromJson(JsonElement elem, WebClient client)
		{
			var nameProp = elem.GetProperty("name");
			var name = nameProp.GetString();
			var pilotsProp = elem.GetProperty("pilots");
			var pilots = new List<Person>();
			foreach (var pilotURI in pilotsProp.EnumerateArray())
			{
				pilots.Add(Person.FromJsonURI(pilotURI.GetString(), client));
			}
			return new Vehicle(name, pilots.ToArray());
		}
		public override string ToString()
		{
			return Name;
		}
	}
}
