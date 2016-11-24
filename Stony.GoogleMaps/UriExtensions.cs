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
using System.Web;
using System.Security.Cryptography;
using System.Text;

namespace Stony.GoogleMaps
{
	public static class UriExtensions
	{
		public static Uri AddParameter(this Uri uri, string name, string value)
		{
			if (String.IsNullOrEmpty(value)) return uri;

			var url = uri.ToString();
			url += String.IsNullOrEmpty(uri.Query) ? "?" : "&";
			url += String.Format("{0}={1}", name, HttpUtility.UrlEncode(value));
			return new Uri(url);
		}

		public static Uri AddCredentials(this Uri uri, Credentials credentials)
		{
			if (String.IsNullOrEmpty(credentials.ClientId) ^ String.IsNullOrEmpty(credentials.PrivateKey)) throw new GoogleMapsException("Credentials must specify both ClientId and PrivateKey");
			if (!String.IsNullOrEmpty(credentials.PrivateKey) && !String.IsNullOrEmpty(credentials.ApiKey)) throw new GoogleMapsException("Credentials should be either an ApiKey or a ClientId / PrivateKey pair");

			if (!String.IsNullOrEmpty(credentials.ApiKey)) return uri.AddParameter("key", credentials.ApiKey);
			else if (!String.IsNullOrEmpty(credentials.ClientId)) return uri.AddParameter("client", credentials.ClientId).AddSignature(credentials.PrivateKey);

			// No credentials were provided
			return uri;
		}

		static Uri AddSignature(this Uri uri, string privateKey)
		{
			var encoding = new ASCIIEncoding();

			// converting key to bytes will throw an exception, need to replace '-' and '_' characters first.
			string usablePrivateKey = privateKey.Replace("-", "+").Replace("_", "/");
			var privateKeyBytes = Convert.FromBase64String(usablePrivateKey);

			var encodedPathAndQueryBytes = encoding.GetBytes(uri.LocalPath + uri.Query);

			// compute the hash
			var algorithm = new HMACSHA1(privateKeyBytes);
			var hash = algorithm.ComputeHash(encodedPathAndQueryBytes);

			// convert the bytes to string and make url-safe by replacing '+' and '/' characters
			var signature = Convert.ToBase64String(hash).Replace("+", "-").Replace("/", "_");

			// Add the signature to the existing URI.
			return uri.AddParameter("signature", signature);
		}
	}
}

