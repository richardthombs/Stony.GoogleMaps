using System;

namespace Stony.GoogleMaps
{
	public class GoogleMapsException : ApplicationException
	{
		public GoogleMapsException(string message, Exception innerException = null) : base(message, innerException) { }
	}
}
