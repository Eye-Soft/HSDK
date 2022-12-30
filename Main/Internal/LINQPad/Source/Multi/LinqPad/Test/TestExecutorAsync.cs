namespace EyeSoft.LinqPad.Test;

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Threading.Tasks;
using EyeSoft.LinqPad.Test.Policy;
using LINQPad;

public class TestExecutorAsync
{
	private readonly ITestMethodsDiscoverPolicy testMethodPolicy;
	private readonly ITestClassPolicy testClassPolicy;

	public TestExecutorAsync(ITestClassPolicy? testClassPolicy = null, ITestMethodsDiscoverPolicy? testMethodPolicy = null)
	{
		this.testMethodPolicy = testMethodPolicy ?? new DeclaredOnlyTestMethodsDiscoverPolicy();
		this.testClassPolicy = testClassPolicy ?? new PublicClassTestClassPolicy();
	}

	public async Task RunAsync(object obj)
	{
		await RunAsync(obj.GetType());
	}

	public async Task RunAsync(Type type)
	{
		await DiscoverAndRunAsync(type).Dump(description: "Test", noTotals: true);
	}

	public async Task<IEnumerable<TestResult>> DiscoverAndRunAsync(Type type)
	{
		var tests = type.GetTestTypes(testClassPolicy);

		return await DiscoverAndRunAsync(tests);

	}

	public async Task<IEnumerable<TestResult>> DiscoverAndRunAsync(IEnumerable<Type> testTypes)
	{
		var results = new List<TestResult>();

		foreach (var test in testTypes)
		{
			results.AddRange(await ExecuteTestsAsync(test));
		}

		return results;
	}

	private async Task<IEnumerable<TestResult>> ExecuteTestsAsync(Type classType)
	{
		var methods = classType.DiscoverTestMethods(testMethodPolicy);

		var results = new ConcurrentBag<TestResult>();

		foreach (var method in methods)
		{
			results.Add(await ExecuteTestAsync(classType, method));
		}

		return results;
	}

	private static async Task<TestResult> ExecuteTestAsync(Type classType, MethodBase methodInfo)
	{
		var stopWatch = Stopwatch.StartNew();

		try
		{
			var testInstance = Activator.CreateInstance(classType);
			stopWatch.Start();
			var result = methodInfo.Invoke(testInstance, null);

			if (result is Task task)
			{
				await task.ConfigureAwait(false);
			}
			else
			{
				await Task.FromResult(result);
			}

			stopWatch.Stop();
			return new TestResult(classType.Name, methodInfo.Name, stopWatch.ElapsedMilliseconds);
		}
		catch (Exception exception)
		{
			stopWatch.Stop();

			return new TestResult(classType.Name, methodInfo.Name, exception.InnerException == null ? exception.Message : exception.InnerException.Message, stopWatch.ElapsedMilliseconds);
		}
	}
}