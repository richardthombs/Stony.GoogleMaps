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
