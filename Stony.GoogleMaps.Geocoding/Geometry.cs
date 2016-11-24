﻿/*
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

using Newtonsoft.Json;

namespace Stony.GoogleMaps.Geocoding
{
	[JsonObject(MemberSerialization.OptIn)]
	public class Geometry
	{
		[JsonProperty("location")]
		public GeographicPosition Location { get; set; }

		[JsonProperty("location_type")]
		public LocationType LocationType { get; set; }

		[JsonProperty("viewport")]
		public GeographicBounds Viewport { get; set; }

		[JsonProperty("bounds")]
		public GeographicBounds Bounds { get; set; }
	}
}
