namespace EyeSoft.LinqPad.Test;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using EyeSoft.LinqPad.Test.Policy;

internal static class TypeExtensions
{
	public static Type[] GetTestTypes(this Type type, ITestClassPolicy testClassPolicy)
	{
		var types = type
			.Assembly
			.GetTypes()
			.Where(testClassPolicy.IsTestClass)
			.ToArray();

		return types;
	}

	public static IEnumerable<MethodBase> DiscoverTestMethods(this Type type, ITestMethodsDiscoverPolicy testMethodsDiscoverPolicy)
	{
		return testMethodsDiscoverPolicy.GetMethods(type);
	}
}