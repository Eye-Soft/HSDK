namespace EyeSoft.LinqPad.Test;

using System.Reflection;
using Policy;

internal static class MethodBaseExtensions
{
	public static bool IsTestMethod(this MethodBase method, ITestMethodPolicy? testMethodPolicy = null)
	{
		testMethodPolicy ??= new PublicTestOrMsTestMethodPolicy();

		var isTestMethod = testMethodPolicy.IsTestMethod(method);

		return isTestMethod;
	}
}