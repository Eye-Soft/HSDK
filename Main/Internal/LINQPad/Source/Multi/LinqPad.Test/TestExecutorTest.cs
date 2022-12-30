namespace EyeSoft.LinqPad.Test;

using System;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class TestExecutorTest
{
	[TestMethod]
	public async Task VerifyDiscoverAndRunIsCorrect()
	{
		await VerifyResultAsync<SampleTest>("SampleTest", "Verify");
	}

	[TestMethod]
	public async Task VerifyDiscoverAndRunGiveError()
	{
		await VerifyResultAsync<Sample2Test>("Sample2Test", "Verify", "Cannot execute.");
	}

	private static async Task VerifyResultAsync<T>(string expectedClass, string expectedMethod, string? expectedError = null)
	{
		var testResultsTask = await new TestExecutorAsync().DiscoverAndRunAsync(new[] { typeof(T) });

		var testResults = testResultsTask.ToArray();

		testResults.Should().HaveCount(1);

		var testResult = testResults.Single();

		testResult.Class.Should().BeEquivalentTo(expectedClass);
		testResult.Test.Should().BeEquivalentTo(expectedMethod);
		testResult.Error.Should().BeEquivalentTo(expectedError);
		testResult.Elapsed.Should().BeLessThan(10);
	}


	// ReSharper disable once ClassNeverInstantiated.Local
	private class SampleTest
	{
		// ReSharper disable once UnusedMember.Local
		public void Verify()
		{
		}
	}

	// ReSharper disable once ClassNeverInstantiated.Local
	private class Sample2Test
	{
		// ReSharper disable once UnusedMember.Local
		public void Verify()
		{
			throw new InvalidOperationException("Cannot execute.");
		}
	}
}