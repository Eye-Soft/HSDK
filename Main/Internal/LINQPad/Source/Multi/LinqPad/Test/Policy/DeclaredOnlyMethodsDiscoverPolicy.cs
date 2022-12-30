namespace EyeSoft.LinqPad.Test.Policy;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

public class DeclaredOnlyTestMethodsDiscoverPolicy : ITestMethodsDiscoverPolicy
{
	private readonly ITestMethodPolicy testMethodPolicy;

	public DeclaredOnlyTestMethodsDiscoverPolicy(ITestMethodPolicy? testMethodPolicy = null)
	{
		this.testMethodPolicy = testMethodPolicy ?? new PublicTestOrMsTestMethodPolicy();
	}
	public IEnumerable<MethodBase> GetMethods(Type type)
	{
		var methods = type
			.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public)
			.Where(x => x.IsTestMethod(testMethodPolicy));

		return methods;
	}
}