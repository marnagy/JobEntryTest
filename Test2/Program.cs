using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Test2
{
	class Program
	{
		static void Main(string[] args)
		{
			using(var client = new WebClient())  
			{
				// GET vehicles
				string nextPageURI = "https://swapi.dev/api/starships/";
				List<Vehicle> vehicles = new List<Vehicle>();

				while (true)
				{
					nextPageURI = LoadVehicles(nextPageURI, client, vehicles);
					//nextPageURI = LoadVehiclesAsync(nextPageURI, client, vehicles).Result;
					if (nextPageURI == null)
					{
						break;
					}
				}

				Console.WriteLine("Vehicles:");
				foreach (var v in vehicles)
				{
					Console.WriteLine(v);
				}

				Console.WriteLine("-------------");

				// filter by planet
				var filteredVehicles = vehicles.Where(v => v.Pilots.Any(p => p.HomePlanet.Name == "Kashyyyk") ).ToList();

				Console.WriteLine("Filtered vehicles:");
				foreach (var v in filteredVehicles)
				{
					Console.WriteLine(v);
				}

				Console.WriteLine("-------------");

				Console.WriteLine($"All vehicles: {vehicles.Count}");
				Console.WriteLine($"Filtered vehicles: {filteredVehicles.Count}");
			}  
		}
		async static Task<string> LoadVehiclesAsync(string URI, WebClient client, List<Vehicle> res)
		{
			var jsonResp = client.DownloadString(URI);
			var options = new JsonSerializerOptions(){
				WriteIndented = true
			};

			var jsonElement = JsonSerializer.Deserialize<JsonElement>(jsonResp);
			var nextPageProp = jsonElement.GetProperty("next");
			//var countProp = jsonElement.GetProperty("count");
			var resultsProp = jsonElement.GetProperty("results");

			string nextPageURI = nextPageProp.GetString();

			foreach (var item in resultsProp.EnumerateArray())
			{
				var vehicle = await Vehicle.FromJsonAsync(item, client);
				res.Add( vehicle );
			}

			return nextPageURI;
		}
		static string LoadVehicles(string URI, WebClient client, List<Vehicle> res)
		{
			var jsonResp = client.DownloadString(URI);
			var options = new JsonSerializerOptions(){
				WriteIndented = true
			};

			var jsonElement = JsonSerializer.Deserialize<JsonElement>(jsonResp);
			var nextPageProp = jsonElement.GetProperty("next");
			//var countProp = jsonElement.GetProperty("count");
			var resultsProp = jsonElement.GetProperty("results");

			string nextPageURI = nextPageProp.GetString();

			foreach (var item in resultsProp.EnumerateArray())
			{
				var vehicle = Vehicle.FromJson(item, client);
				res.Add( vehicle );
			}

			return nextPageURI;
		}
	}
}
