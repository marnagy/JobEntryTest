using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

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

		public async static Task<Vehicle> FromJsonAsync(JsonElement elem, WebClient client)
		{
			var nameProp = elem.GetProperty("name");
			var name = nameProp.GetString();
			var pilotsProp = elem.GetProperty("pilots");
			var pilots = new List<Person>();
			foreach (var pilotURI in pilotsProp.EnumerateArray())
			{
				var person = await Person.FromJsonURIAsync(pilotURI.GetString(), client);
				pilots.Add( person );
			}
			return new Vehicle(name, pilots.ToArray());
		}
		public static Vehicle FromJson(JsonElement elem, WebClient client)
		{
			var nameProp = elem.GetProperty("name");
			var name = nameProp.GetString();
			var pilotsProp = elem.GetProperty("pilots");
			var pilots = new List<Person>();
			foreach (var pilotURI in pilotsProp.EnumerateArray())
			{
				var person = Person.FromJsonURI(pilotURI.GetString(), client);
				pilots.Add( person );
			}
			return new Vehicle(name, pilots.ToArray());
		}
		public override string ToString()
		{
			return Name;
		}
	}
}
