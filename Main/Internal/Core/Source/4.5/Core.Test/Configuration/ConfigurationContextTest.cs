﻿namespace EyeSoft.Core.Test.Configuration
{
    using System.Collections.Specialized;
    using Extensions;
    using EyeSoft.Collections.Specialized;
    using EyeSoft.Configuration;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using FluentAssertions;

    [TestClass]
	public class ConfigurationContextTest
	{
		[TestMethod]
		public void VerifyExitingKeyInConfiguration()
		{
			var configuration = new Mock<IConfigurationContext>();

			const string Key = "Key1";
			const int ExpectedValue = 903;
			var expectedValueString = ExpectedValue.ToInvariant();

			configuration
				.SetupGet(x => x.AppSettings)
				.Returns(new NameValueCollection { { Key, expectedValueString } });

			ConfigurationContext.Set(configuration.Object);

			ConfigurationContext.AppSettings.Get<int>(Key).Should().Be(ExpectedValue);
		}
	}
}