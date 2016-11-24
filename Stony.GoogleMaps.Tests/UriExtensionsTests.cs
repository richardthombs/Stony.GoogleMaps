using System;
using System.Collections.Generic;

using NUnit.Framework;

namespace Stony.GoogleMaps.Tests
{
	[TestFixture]
    public class UriExtensionsTests
    {
		[Test]
		public void ApiKey_And_ClientId_Fails()
		{
			var uri = new Uri("https://dummy.com");

			Assert.Throws<GoogleMapsException>(() =>
			{
				uri.AddCredentials(new Credentials { ApiKey = "foo", ClientId = "bar" });
			});
		}

		[Test]
		public void ApiKey_And_PrivateKey_Fails()
		{
			var uri = new Uri("https://dummy.com");

			Assert.Throws<GoogleMapsException>(() =>
			{
				uri.AddCredentials(new Credentials { ApiKey = "foo", PrivateKey = "bar" });
			});
		}

		[Test]
		public void ClientId_Only_Fails()
		{
			var uri = new Uri("https://dummy.com");

			Assert.Throws<GoogleMapsException>(() =>
			{
				uri.AddCredentials(new Credentials { ClientId = "foo" });
			});
		}

		[Test]
		public void PrivateKey_Only_Fails()
		{
			var uri = new Uri("https://dummy.com");

			Assert.Throws<GoogleMapsException>(() =>
			{
				uri.AddCredentials(new Credentials { PrivateKey = "foo" });
			});
		}

		[Test]
		public void ApiKey_Only_Works()
		{
			var uri = new Uri("https://dummy.com");
			uri = uri.AddCredentials(new Credentials { ApiKey = "foo" });

			Assert.IsTrue(uri.ToString().EndsWith("key=foo"));
		}

		[Test]
		public void ClientId_And_PrivateKey_Works()
		{
			var uri = new Uri("https://dummy.com");
			uri = uri.AddCredentials(new Credentials { ClientId = "foo", PrivateKey = "YmFyCg==" });

			Assert.IsTrue(uri.ToString().Contains("client=foo"));
			Assert.IsTrue(uri.ToString().Contains("signature="));
		}

		[Test]
		public void Add_First_Parameter()
		{
			var uri = new Uri("https://dummy.com/api");
			uri = uri.AddParameter("foo", "bar");

			Assert.AreEqual("https://dummy.com/api?foo=bar", uri.ToString());
		}

		[Test]
		public void Add_Additional_Parameter()
		{
			var uri = new Uri("https://dummy.com/api?foo=bar");
			uri = uri.AddParameter("foo2", "bar2");

			Assert.AreEqual("https://dummy.com/api?foo=bar&foo2=bar2", uri.ToString());
		}
	}
}
