namespace EyeSoft.LinqPad.Test;

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Threading.Tasks;
using EyeSoft.LinqPad.Test.Policy;
using LINQPad;

public class TestExecutor
{
	private readonly ITestMethodsDiscoverPolicy testMethodsDiscoverPolicy;
	private readonly ITestClassPolicy testClassPolicy;

	public TestExecutor(ITestClassPolicy? testClassPolicy = null, ITestMethodsDiscoverPolicy? testMethodsDiscoverPolicy = null)
	{
		this.testMethodsDiscoverPolicy = testMethodsDiscoverPolicy ?? new DeclaredOnlyTestMethodsDiscoverPolicy();
		this.testClassPolicy = testClassPolicy ?? new PublicClassTestClassPolicy();
	}

	public void Run<T>()
	{
		Run(typeof(T));
	}

	public void Run(Type type)
	{
		DiscoverAndRun(type).Dump(description: "Test", noTotals: true);
	}

	public IEnumerable<TestResult> DiscoverAndRun<T>()
	{
		return DiscoverAndRun(typeof(T));
	}

	public IEnumerable<TestResult> DiscoverAndRun(object obj)
	{
		return DiscoverAndRun(obj.GetType());
	}
	
	public IEnumerable<TestResult> DiscoverAndRun(Type type)
	{
		var testTypes = type.GetTestTypes(testClassPolicy);

		return DiscoverAndRun(testTypes);
	}

	public IEnumerable<TestResult> DiscoverAndRun(IEnumerable<Type> testTypes)
	{
		var results = new List<TestResult>();

		Parallel.ForEach(testTypes, x => results.AddRange(ExecuteTests(x)));

		return results;
	}

	private IEnumerable<TestResult> ExecuteTests(Type classType)
	{
		var methods = classType.DiscoverTestMethods(testMethodsDiscoverPolicy);

		var results = new ConcurrentBag<TestResult>();

		Parallel.ForEach(methods, x => results.Add(ExecuteTest(classType, x)));

		return results;
	}

	internal static TestResult ExecuteTest(Type classType, MethodBase methodInfo)
	{
		var stopWatch = Stopwatch.StartNew();

		try
		{
			var testInstance = Activator.CreateInstance(classType);
			stopWatch.Start();
			methodInfo.Invoke(testInstance, null);
			stopWatch.Stop();
			return new TestResult(classType.Name, methodInfo.Name, stopWatch.ElapsedMilliseconds);
		}
		catch (Exception exception)
		{
			stopWatch.Stop();

			return new TestResult(classType.Name, methodInfo.Name, exception.InnerException!.Message, stopWatch.ElapsedMilliseconds);
		}
	}
}