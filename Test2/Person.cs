using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Test2
{
	class Person
	{
		public string Name {get;}
		//public int Height {get; set;}
		//public int Mass {get; set;}
		//public string HairColor {get; set;}
		//public string SkinColor {get; set;}
		//public string EyeColor {get; set;}
		//public string BirthYear {get; set;}
		//public string Gender {get; set;}
		public Planet HomePlanet {get;}
		public Person(string name, Planet homePlanet)
		{
			Name = name;
			HomePlanet = homePlanet;
		}

		internal async static Task<Person> FromJsonURIAsync(string URI, WebClient client)
		{
			var jsonResp = client.DownloadString(URI);
			var options = new JsonSerializerOptions(){
				WriteIndented = true
			};

			var jsonPersonElem = JsonSerializer.Deserialize<JsonElement>(jsonResp);
			var nameProp = jsonPersonElem.GetProperty("name");
			string name = nameProp.GetString();
			var planetProp = jsonPersonElem.GetProperty("homeworld");
			var homePlanet = await Planet.FromJsonURIAsync(planetProp.GetString(), client);
			return new Person(name, homePlanet);
		}
		internal static Person FromJsonURI(string URI, WebClient client)
		{
			var jsonResp = client.DownloadString(URI);
			var options = new JsonSerializerOptions(){
				WriteIndented = true
			};

			var jsonPersonElem = JsonSerializer.Deserialize<JsonElement>(jsonResp);
			var nameProp = jsonPersonElem.GetProperty("name");
			string name = nameProp.GetString();
			var planetProp = jsonPersonElem.GetProperty("homeworld");
			var homePlanet = Planet.FromJsonURI(planetProp.GetString(), client);
			return new Person(name, homePlanet);
		}
	}
}
