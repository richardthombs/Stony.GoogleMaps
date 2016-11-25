# A .NET wrapper for the Google Maps Geocoding API
![Build status](https://ci.appveyor.com/api/projects/status/at6eyb00ipg1fvm1/branch/master?svg=true)
[![NuGet status](https://img.shields.io/nuget/v/Stony.GoogleMaps.Geocoding.svg)](https://www.nuget.org/packages/Stony.GoogleMaps.Geocoding)

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

## Authorisation

The `GeocodingService` class must be constructed using a `Credentials` instance which represents your Google Maps authorisation credentials.

If you have a Google Maps API key, then construct a `Credentials` instance like this:

```c#
var creds = new Credentials { ApiKey = "YOUR_API_KEY" };
```

If you have a Google Maps Premium account and want to use your client ID and private key:

```c#
var creds = new Credentials
{
	ClientId = "YOUR_CLIENT_ID",
	PrivateKey = "YOUR_PRIVATE_KEY"
};
```

It is possible to use some parts of the Google Maps API without authorisation. If you wish to do so, then you can use the static property `Credentials.None`.

# Credits

This project incorporates code from [Google Maps API for .Net](https://github.com/ericnewton76/gmaps-api-net), by Luis Farzati, Eric Newton et al.

