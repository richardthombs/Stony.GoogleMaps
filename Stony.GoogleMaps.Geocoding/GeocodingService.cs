/*
	Copyright 2016 Richard Thombs

	Licensed under the Apache License, Version 2.0 (the "License");
	you may not use this file except in compliance with the License.
	You may obtain a copy of the License at

		http://www.apache.org/licenses/LICENSE-2.0

	Unless required by applicable law or agreed to in writing, software
	distributed under the License is distributed on an "AS IS" BASIS,
	WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
	See the License for the specific language governing permissions and
	limitations under the License.
*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Web;

using Newtonsoft.Json;

namespace Stony.GoogleMaps.Geocoding
{
	public class GeocodingService
	{
		Credentials credentials;

		public GeocodingService(Credentials credentials)
		{
			this.credentials = credentials;
		}

		public GeocodingResponse Geocode(GeocodingRequest request)
		{
			var query = new List<string>();
			query.AddParameter("address", request.Address);
			query.AddParameter("components", request.Components);
			query.AddParameter("bounds", request.Bounds);
			query.AddParameter("region", request.Region);
			query.AddParameter("language", request.Language);
			if (request.NewForwardGeocoder) query.AddParameter("new_forward_geocoder", "true");

			var uri = new Uri("https://maps.googleapis.com/maps/api/geocode/json?" + String.Join("&", query));

			uri = uri.AddCredentials(credentials);

			var client = new WebClient();
			client.Encoding = System.Text.Encoding.UTF8;
			var json = client.DownloadString(uri);

			using (var stringReader = new StringReader(json))
			{
				var reader = new JsonTextReader(stringReader);
				var serializer = new JsonSerializer();
				serializer.Converters.Add(new JsonEnumTypeConverter());
				return serializer.Deserialize<GeocodingResponse>(reader);
			}
		}
	}

	static class StringListExtensions
	{
		public static void AddParameter(this List<string> list, string name, string value)
		{
			if (String.IsNullOrEmpty(value)) return;

			list.Add(String.Format("{0}={1}", name, HttpUtility.UrlEncode(value)));
		}
	}
}

