using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Text.Json;

namespace Test2
{
	class Planet
	{
		public string Name {get;}
		public Planet(string name)
		{
			Name = name;
		}

		internal static Planet FromJsonURI(string URI, WebClient client)
		{
			var jsonResp = client.DownloadString(URI);
			var options = new JsonSerializerOptions(){
				WriteIndented = true
			};

			var jsonPlanetElem = JsonSerializer.Deserialize<JsonElement>(jsonResp);
			var nameProp = jsonPlanetElem.GetProperty("name");
			return new Planet(nameProp.GetString());
		}
	}
}
