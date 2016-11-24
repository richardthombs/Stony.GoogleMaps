# A .NET wrapper for the Google Maps API

This project incorporates code from [Google Maps API for .Net](https://github.com/ericnewton76/gmaps-api-net), by Luis Farzati, Eric Newton et al.


## Usage

```c#
using System;

using Stony.GoogleMaps;
using Stony.GoogleMaps.Geocoding;

namespace Geocoding
{
    public class Program
    {
		public static void Main()
		{
			var address = "23411 Summerfield, Aliso Viejo";

			var svc = new GeocodingService(Credentials.None);

			var response = svc.Geocode(address);

			if (response.Status == ServiceResponseStatus.Ok)
			{
				var best = response.Results[0];
				var location = best.Geometry.Location;
				Console.WriteLine("Geocoded \"{3}\" as \"{2}\" at ({1:n4}, {0:n4})", location.Latitude, location.Longitude, best.FormattedAddress, address);
			}
			else
			{
				Console.WriteLine("Service responded with error \"{0}\"", response.Status);
			}
		}
    }
}
```

When run, this will produce:

```txt
Geocoded "23411 Summerfield, Aliso Viejo" as "23411 Summerfield, Aliso Viejo, CA 92656, USA" at (-117.7207, 33.5772)
```
