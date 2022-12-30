namespace EyeSoft.LinqPad.Test;

using System;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class TestExecutorAsyncTest
{
	[TestMethod]
	public async Task VerifyDiscoverAndRunIsCorrectAsync()
	{
		await VerifyResult<SampleAsyncTest>("SampleAsyncTest", "VerifyAsync");
	}

	[TestMethod]
	public async Task VerifyDiscoverAndRunGiveErrorAsync()
	{
		await VerifyResult<Sample2AsyncTest>("Sample2AsyncTest", "VerifyAsync", "Cannot execute.");
	}

	private static async Task VerifyResult<T>(string expectedClass, string expectedMethod, string? expectedError = null)
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
	private class SampleAsyncTest
	{
		// ReSharper disable once UnusedMember.Local
		public Task<object?> VerifyAsync()
		{
			return Task.FromResult((object?)null);
		}
	}

	// ReSharper disable once ClassNeverInstantiated.Local
	private class Sample2AsyncTest
	{
		// ReSharper disable once UnusedMember.Local
		public async Task VerifyAsync()
		{
			await Task.Run(() =>throw new InvalidOperationException("Cannot execute."));
		}
	}
}