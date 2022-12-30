namespace EyeSoft.LinqPad.Test;

using System;
using System.Threading.Tasks;
using EyeSoft.LinqPad.Test.Policy;

public static class TestRunner
{
	public static void Run(
		Type type,
		ITestClassPolicy? testClassPolicy = null,
		ITestMethodsDiscoverPolicy? testMethodsDiscoverPolicy = null)
	{
		type.RunTests(testClassPolicy, testMethodsDiscoverPolicy);
	}

	public static async Task RunAsync(
		Type type,
		ITestClassPolicy? testClassPolicy = null,
		ITestMethodsDiscoverPolicy? testMethodsDiscoverPolicy = null)
	{
		await type.RunTestsAsync(testClassPolicy, testMethodsDiscoverPolicy);
	}

	public static void RunTests(
		this object obj,
		ITestClassPolicy? testClassPolicy = null,
		ITestMethodsDiscoverPolicy? testMethodsDiscoverPolicy = null)
	{
		new TestExecutor(testClassPolicy, testMethodsDiscoverPolicy).Run(obj.GetType());
	}

	public static async Task RunTestsAsync(
		this object obj,
		ITestClassPolicy? testClassPolicy = null,
		ITestMethodsDiscoverPolicy? testMethodsDiscoverPolicy = null)
	{
		await obj.GetType().RunTestsAsync(testClassPolicy, testMethodsDiscoverPolicy);
	}

	public static void RunTests(
		this Type type,
		ITestClassPolicy? testClassPolicy = null,
		ITestMethodsDiscoverPolicy? testMethodsDiscoverPolicy = null)
	{
		new TestExecutor(testClassPolicy, testMethodsDiscoverPolicy).Run(type);
	}

	public static async Task RunTestsAsync(
		this Type type,
		ITestClassPolicy? testClassPolicy = null,
		ITestMethodsDiscoverPolicy? testMethodsDiscoverPolicy = null)
	{
		await new TestExecutorAsync(testClassPolicy, testMethodsDiscoverPolicy).RunAsync(type);
	}
}