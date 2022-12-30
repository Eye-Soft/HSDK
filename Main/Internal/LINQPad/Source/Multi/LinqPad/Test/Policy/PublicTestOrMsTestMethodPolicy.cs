namespace EyeSoft.LinqPad.Test.Policy;

using System.Linq;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

public class PublicTestOrMsTestMethodPolicy : ITestMethodPolicy
{
	public bool IsTestMethod(MethodBase method)
	{
		var isTestMethod =
			method.IsPublic ||
			method.CustomAttributes.Any(y => y.AttributeType == typeof(TestMethodAttribute));

		return isTestMethod;
	}
}